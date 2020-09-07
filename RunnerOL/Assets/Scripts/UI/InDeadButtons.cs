using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDeadButtons : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (ProgressSetting.Instance.gameOver)
		{
			anim.SetBool("ShowButton", true);
		}
		else
		{
			anim.SetBool("ShowButton", false);
		}
	}
}
