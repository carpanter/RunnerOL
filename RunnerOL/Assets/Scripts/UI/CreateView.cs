using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RunnerCommon;

public class CreateView : MonoBehaviour {

	[SerializeField]
	private InputField nameField;
	[SerializeField]
	private GameObject tips;
	[SerializeField]
	private Button btnConfrim;
	[SerializeField]
	private GameObject matchView;

	// Use this for initialization
	void Start () {
		EventCenter.Instance.AddListen(EventType.CreateSucces, CreateNameSucces);
		EventCenter.Instance.AddListen<string>(EventType.CreateFalse, ShowTips);
	}
	
	public void OnConfrimClick()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		if (string.IsNullOrEmpty(nameField.text))
			return;
		PhotonManager.Instance.Request(OpCode.AccCode, AccoutCode.CreateName, nameField.text);
	}

	void ShowTips(string msg)
    {
		tips.SetActive(true);
		tips.GetComponentInChildren<Text>().text = msg;
		nameField.text = string.Empty;
		btnConfrim.interactable = true;
	}

	void CreateNameSucces()
    {
		//gameObject.SetActive(false);
		//matchView.SetActive(true);
		PlayerCahe.Instance.PlayerName = nameField.text;
	}

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen(EventType.CreateSucces, CreateNameSucces);
		EventCenter.Instance.RemoveListen<string>(EventType.CreateFalse, ShowTips);
	}
}
