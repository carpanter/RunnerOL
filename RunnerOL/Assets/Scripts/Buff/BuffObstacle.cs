using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffObstacle : Obstacle 
{
	RaycastHit hit;
	// Use this for initialization
	new void Start () {
		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		bound = GetComponent<MeshRenderer>().bounds;
		offset = bound.center - transform.position;
	}
	
	// Update is called once per frame
	new void Update () {
		if (pc.gameObject.transform.position.z - 10 > transform.position.z)
		{
			ResetPos();
		}
		if (!isBusContact && Physics.Raycast(transform.position, Vector3.down, out hit))
		{
			if (hit.collider.gameObject.tag == "Player")
				return;
			if (hit.collider.isTrigger && hit.collider.gameObject.tag == "Vehicle")
				transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
			else
				transform.position = new Vector3(transform.position.x, hit.point.y + 0.5f, transform.position.z);
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

	 void ResetPos()
    {
		int minZ = GameController.Instance.area * 320 + 640;
		int MaxZ = GameController.Instance.area * 320 + 960;
		float z = ObstacleInitialize.Instance.seed.Next(minZ, MaxZ);
		float x = arryX[ObstacleInitialize.Instance.seed.Next(0, 3)];
		transform.position = new Vector3(x, 20, z);
		SetCanSee();
		SetBusContactFalse();
	}

	void SetCanSee()
	{
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		if(gameObject.tag == "Coin")
			gameObject.GetComponent<SphereCollider>().enabled = true;
		else
			gameObject.GetComponent<BoxCollider>().enabled = true;
	}

	void SetBusContactFalse()
	{
		isBusContact = false;
	}

	IEnumerator SetColliderFalse()
    {
		yield return new WaitForSeconds(0.1f);
		gameObject.GetComponent<BoxCollider>().enabled = false;
	}

	protected void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "OtherPlayer") && !col.isTrigger)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			StartCoroutine("SetColliderFalse");
		}
	}
}
