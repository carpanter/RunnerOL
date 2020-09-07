using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Obstacle
{

	MeshCollider boardcollider;
	Collider playerCollider;
	// Use this for initialization
	new void Start () {
		base.Start();
		boardcollider = gameObject.GetComponent<MeshCollider>();
		playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
		EventCenter.Instance.AddListen(EventType.RollingStart, SetBoradColliderFalse);
		EventCenter.Instance.AddListen(EventType.RollingEnd, SetBoradColliderTure);
	}
	

	public void SetBoradColliderFalse()
    {
		Physics.IgnoreCollision(playerCollider, boardcollider, true);
	}

	public void SetBoradColliderTure()
    {
		Physics.IgnoreCollision(playerCollider, boardcollider, false);
	}

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen(EventType.RollingStart, SetBoradColliderFalse);
		EventCenter.Instance.RemoveListen(EventType.RollingEnd, SetBoradColliderTure);
	}


}
