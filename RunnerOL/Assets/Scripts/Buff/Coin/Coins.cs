using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coins : MonoBehaviour
{
	float[] arryX = { -1.5f, 0, 1.5f };
	PlayerControl pc;
	Transform[] transforms;
	void Start()
    {
		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		transforms = gameObject.GetComponentsInChildren<Transform>();
	}
	 void Update () 
	{
		if (pc.gameObject.transform.position.z - 10 > transform.position.z)
		{
			ResetPos();
		}
	}

	void ResetPos()
	{
		int minZ = GameController.Instance.area * 320 + 640;
		int MaxZ = GameController.Instance.area * 320 + 960;
		float z = ObstacleInitialize.Instance.seed.Next(minZ, MaxZ);
		float x = arryX[ObstacleInitialize.Instance.seed.Next(0, 3)];	
		transform.position = new Vector3(x, 20, z);
		BroadcastMessage("SetCanSee");
		BroadcastMessage("SetBusContactFalse");
		for (int i=1;i<transforms.Length;i++)
        {
			transforms[i].localPosition = new Vector3(0, 20, i * 0.5f - 0.5f);
        }
	}
}
