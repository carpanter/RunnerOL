using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coin : BuffObstacle
{
	RaycastHit hit;
	// Use this for initialization
	new void Start()
	{
		bound = GetComponent<MeshRenderer>().bounds;
		offset = bound.center - transform.position;
	}

	// Update is called once per frame
	new void Update()
	{
		transform.Rotate(Vector3.up * 180 * Time.deltaTime);
		if (!isBusContact && Physics.Raycast(transform.position,Vector3.down,out hit))
        {
			if (hit.collider.gameObject.tag == "Player")
				return;
			if (hit.collider.isTrigger && hit.collider.gameObject.tag == "Vehicle")
				transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
			else
				transform.position = new Vector3(transform.position.x, hit.point.y + 0.25f, transform.position.z);
		}
		ListCollider = Physics.OverlapBox(transform.position + offset, bound.extents).ToList<Collider>();
		foreach (var col in ListCollider)
		{
			if (col.gameObject == gameObject || isBusContact || col.isTrigger)
				continue;
			if (col.gameObject.tag == "Env" || col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Vehicle" ||
				col.gameObject.tag == "BigEnv" || col.gameObject.tag == "BigObstacle")
			{
				transform.position = new Vector3(transform.position.x, 20, transform.position.z);
			}
		}
		ListCollider.Clear();
	}

	IEnumerator SetColliderFalse()
	{
		yield return new WaitForSeconds(0.1f);
		gameObject.GetComponent<SphereCollider>().enabled = false;
	}

	new void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "OtherPlayer") && !col.isTrigger)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			StartCoroutine("SetColliderFalse");
			if (col.gameObject.tag == "Player")
            {
				if (SoundsManager.Instance.SoundsOn)
					SoundsManager.Instance.PlayAudioByName("coin");
				PlayerControl con = col.gameObject.GetComponent<PlayerControl>();
				if (con == null)
					return;
				PhotonManager.Instance.Request(OpCode.GameCode, GameCode.GetCoin, con.multiplyCoin);
			}
		}
	}
}
