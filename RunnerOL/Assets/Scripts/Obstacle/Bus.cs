using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : Obstacle
{
	List<GameObject> listPlayer;
	public float busSpeed;
	public bool busMoving = false;
	// Use this for initialization
	new void Start()
	{
		base.Start();
		listPlayer = new List<GameObject>();
		listPlayer.Add(pc.gameObject);
		GameObject[] gos = GameObject.FindGameObjectsWithTag("OtherPlayer");
		listPlayer.AddRange(gos);
	}

	// Update is called once per frame
	new void Update()
	{
		//if (pc.playerState == PlayerControl.PlayerState.Dead)
  //      {
		//	busMoving = false;
		//	return;
  //      }
		base.Update();
		busMoving = false;
		foreach (var go in listPlayer)
        {
			if (go == null)
				continue;
			if (go.transform.position.z + 30 > transform.position.z)
            {
				busMoving = true;
				break;
			}				
		}
		if(busMoving)
        {
			transform.Translate(Vector3.back * busSpeed * Time.deltaTime);
			if(this.isBusContact == false)
				this.isBusContact = true;
		}
		
	}
	private void OnTriggerStay(Collider col)
	{
		if (!busMoving)
			return;
		if (col.gameObject.tag == "Env" || col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Vehicle" ||
			col.gameObject.tag == "BigEnv" || col.gameObject.tag == "BigObstacle" || col.gameObject.tag == "Buff" || col.gameObject.tag == "Coin")
		{
			var ob = col.gameObject.GetComponent<Obstacle>();
			if (ob != null)
				ob.isBusContact = true;
		}
	}

}
