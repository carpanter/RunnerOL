using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

	[SerializeField]
	Text nameText;
	[SerializeField]
	Text scoreText;
	[SerializeField]
	Sprite lifeOn;
	[SerializeField]
	Sprite lifeOff;
	[SerializeField]
	Image[] lifes;
	
	// Use this for initialization
	void Awake () 
	{
		nameText.text = PlayerCahe.Instance.PlayerName;
		scoreText.text = "0";
		foreach(var life in lifes)
        {
			life.sprite = lifeOn;
        }

		EventCenter.Instance.AddListen<int>(EventType.UpdatePlayerScore, UpdateScore);
		EventCenter.Instance.AddListen<int>(EventType.UpdatePlayerLife, UpdateLife);
	}

	void UpdateScore(int score)
    {
		scoreText.text = score.ToString();
    }

	void UpdateLife(int lifeLeft)
    {
		for(int i=0;i<lifes.Length;i++)
        {
			if (i < lifeLeft)
				lifes[i].sprite = lifeOn;
			else
				lifes[i].sprite = lifeOff;
        }
    }

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen<int>(EventType.UpdatePlayerScore, UpdateScore);
		EventCenter.Instance.RemoveListen<int>(EventType.UpdatePlayerLife, UpdateLife);
	}

}
