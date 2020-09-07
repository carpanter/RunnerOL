using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{

	[SerializeField]
	Text nameText;
	[SerializeField]
	Text scoreText;
	[SerializeField]
	Text LifeText;

	public void Init(string name)
	{
		nameText.text = name;
		scoreText.text = "X 0";
		LifeText.text = "X 3";
	}

	public void UpdateScore(int score)
	{
		scoreText.text = string.Format("X {0}", score);
	}

	public void UpdateLife(int lifeLeft)
	{
		LifeText.text = string.Format("X {0}", lifeLeft);
	}
}
