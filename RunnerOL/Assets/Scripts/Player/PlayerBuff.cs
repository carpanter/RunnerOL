using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuff : MonoBehaviour {


	PlayerControl pc;
	bool isUpadteView;

	bool getMultiply = false;
	bool getShoe = false;
	bool getStar = false;

	float multiplyTimer = 0;
	float shoeTimer = 0;
	float starTimer = 0;
	
	// Use this for initialization
	void Start () {
		pc = gameObject.GetComponent<PlayerControl>();
		if (gameObject.tag == "Player")
			isUpadteView = true;
		else
			isUpadteView = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (getMultiply)
        {
			string text = ((int)(15 - multiplyTimer)).ToString() + "s";
			float fillAmount = multiplyTimer / 15;
			if (NormalTimer(ref multiplyTimer))
            {
				text = string.Empty;
				fillAmount = 1;
				pc.multiplyCoin = false;
				getMultiply = false;
			}
			if (isUpadteView)
				EventCenter.Instance.PostEvent<string, float>(EventType.UpdateMultiplyView, text, fillAmount);
		}
		if (getShoe)
        {
			string text = ((int)(15 - shoeTimer)).ToString() + "s";
			float fillAmount = shoeTimer / 15;
			if (NormalTimer(ref shoeTimer))
            {
				text = string.Empty;
				fillAmount = 1;
				pc.doubleJump = false;
				getShoe = false;
			}
			if (isUpadteView)
				EventCenter.Instance.PostEvent<string, float>(EventType.UpdateShoeView, text, fillAmount);
		}
		if (getStar)
		{
			string text = ((int)(2 - starTimer)).ToString() + "s";
			float fillAmount = starTimer / 2;
			if (StarTimer(ref starTimer))
            {
				text = string.Empty;
				fillAmount = 1;
				pc.sprint = false;
				getStar = false;
			}
			if (isUpadteView)
				EventCenter.Instance.PostEvent<string, float>(EventType.UpdaeStarView, text, fillAmount);
		}
	}

	public void GetMultiply()
    {
		getMultiply = true;
		multiplyTimer = 0;
	}

	public void GetShoe()
    {
		getShoe = true;
		shoeTimer = 0;
	}

	public void GetStar()
    {
		getStar = true;
		starTimer = 0;
	}


	bool NormalTimer(ref float timer)
    {
		timer += Time.deltaTime;
		if(timer > 15.0f)
        {
			timer = 0;
			return true;
		}
		else
        {
			return false;
        }		
	}

	bool StarTimer(ref float timer)
    {
		{
			timer += Time.deltaTime;
			if (timer > 0.5f)
			{
				timer = 0;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
