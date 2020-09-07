using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	protected PlayerControl pc;
	public bool isBusContact = false;
	protected Bounds bound;
	protected Vector3 offset = Vector3.zero;
	protected List<Collider> ListCollider = new List<Collider>();
	int myArea;
	protected float[] arryX = { -1.5f, 0, 1.5f };
	
	// Use this for initialization
	protected void Start () 
	{
		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		bound = GetComponent<MeshCollider>().bounds;
		offset = bound.center - transform.position;
		if (transform.position.z < 320)
			myArea = 0;
		else
			myArea = 1;
	}

	// Update is called once per frame
	protected void Update () 
	{
		if (pc.gameObject.transform.position.z - 10 > transform.position.z)
		{
			ResetPos(true);
		}
		ListCollider = Physics.OverlapBox(transform.position + offset, bound.extents).ToList<Collider>();
		foreach(var col in ListCollider)
        {
			if (col.gameObject == gameObject || isBusContact || col.isTrigger)
				continue;
			if(col.gameObject.tag == "Env" || col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Vehicle" ||
				col.gameObject.tag == "BigEnv" || col.gameObject.tag == "BigObstacle")
            {
				ResetPos(false);
			}
        }
		ListCollider.Clear();
	}

	void ResetPos(bool nextArea)
	{
		int minZ = 0;
		int MaxZ = 0;
		if (nextArea)
        {
			minZ = GameController.Instance.area * 320 + 640;
			MaxZ = GameController.Instance.area * 320 + 960;
			if(nextArea)
				myArea = GameController.Instance.area + 2;
		}
		else
        {
			minZ = myArea * 320;
			MaxZ = myArea * 320 + 320;
			if (myArea == 0)
				minZ = 30;
		}
		float z = ObstacleInitialize.Instance.seed.Next(minZ, MaxZ);
		float x = 0;
		if (gameObject.tag != "BigEnv" && gameObject.tag != "BigObstacle")
			x = arryX[ObstacleInitialize.Instance.seed.Next(0, 3)];
		transform.position = new Vector3(x, transform.position.y, z);
		isBusContact = false;
	}
}
