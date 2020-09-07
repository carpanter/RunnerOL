using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScreen : MonoBehaviour {

	public float alpha = 0.25f;
	public float fadeSpeed = 1.0f;
	CanvasGroup canvasGroup;
	// Use this for initialization
	void Start () {
		canvasGroup = gameObject.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		if(alpha != canvasGroup.alpha)
        {
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, alpha, fadeSpeed * Time.deltaTime);
        }
		if(Mathf.Abs(canvasGroup.alpha - alpha) < 0.05f)
        {
			this.gameObject.SetActive(false);
        }
	}
}
