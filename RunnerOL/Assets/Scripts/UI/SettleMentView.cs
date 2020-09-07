using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettleMentView : MonoBehaviour {

	[SerializeField]
	Text resulText;
	[SerializeField]
	GameObject bg;
	[SerializeField]
	GameObject exitButton;
	// Use this for initialization
	void Awake () {
		EventCenter.Instance.AddListen<bool>(EventType.ShowSettleMent, ShowSettleMent);
	}
	
	void ShowSettleMent(bool isWin)
    {
		resulText.gameObject.SetActive(true);
		resulText.text = isWin ? "You Win" : "You Lost";
		bg.SetActive(true);
		exitButton.SetActive(true);
    }

	public void OnExitClick()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		SceneManager.LoadScene(1);
		BGMControl.Instance.ChangeBGM(1);
    }

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen<bool>(EventType.ShowSettleMent, ShowSettleMent);
	}
}
