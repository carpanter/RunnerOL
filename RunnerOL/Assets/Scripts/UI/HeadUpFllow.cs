using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpFllow : MonoBehaviour {

	[SerializeField]
	Text nameText;
	[SerializeField]
	Transform Head;

	Vector3 screenPos;
	Vector3 offset = new Vector3(0, 0.55f, 0);
	float speed = 5.0f;
	Mesh mesh;
	PlayerControl pc;

	// Use this for initialization
	void Start () {
		pc = gameObject.GetComponent<PlayerControl>();
		if (!string.IsNullOrEmpty(PlayerCahe.Instance.PlayerName))
			nameText.text = PlayerCahe.Instance.PlayerName;
	}
	
	// Update is called once per frame
	void Update () {
		screenPos = Camera.main.WorldToScreenPoint(Head.position + offset);
		if (pc.playerState == PlayerControl.PlayerState.Jump || pc.playerState == PlayerControl.PlayerState.JumpDown || pc.playerState == PlayerControl.PlayerState.DoubleJump
			|| pc.playerState == PlayerControl.PlayerState.InFlaot)
			nameText.rectTransform.position = Vector3.Lerp(nameText.rectTransform.position, screenPos, speed * Time.deltaTime);
		else
			nameText.rectTransform.position = screenPos;
	}
}
