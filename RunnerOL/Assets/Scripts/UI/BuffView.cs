using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class BuffView : MonoBehaviour {

	[SerializeField]
	private Text shoeTime;
	[SerializeField]
	private Text starTime;
	[SerializeField]
	private Text multiplyTime;
	[SerializeField]
	private Image shoeMask;
	[SerializeField]
	private Image starMask;
	[SerializeField]
	private Image multiplyMask;

	// Use this for initialization
	void Start () {
		EventCenter.Instance.AddListen<string, float>(EventType.UpdateMultiplyView, UpdateMultiplyView);
		EventCenter.Instance.AddListen<string, float>(EventType.UpdateShoeView, UpdateShoeView);
		EventCenter.Instance.AddListen<string, float>(EventType.UpdaeStarView, UpdateStarView);
	}
	
	void UpdateMultiplyView(string text,float fillAmount)
    {
		multiplyTime.text = text;
		multiplyMask.fillAmount = fillAmount;
	}

	void UpdateShoeView(string text, float fillAmount)
	{
		shoeTime.text = text;
		shoeMask.fillAmount = fillAmount;
	}

	void UpdateStarView(string text, float fillAmount)
	{
		starTime.text = text;
		starMask.fillAmount = fillAmount;
	}

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen<string, float>(EventType.UpdateMultiplyView, UpdateMultiplyView);
		EventCenter.Instance.RemoveListen<string, float>(EventType.UpdateShoeView, UpdateShoeView);
		EventCenter.Instance.RemoveListen<string, float>(EventType.UpdaeStarView, UpdateStarView);
	}
}
