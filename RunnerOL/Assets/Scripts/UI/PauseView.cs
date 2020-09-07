using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {

	[SerializeField]
	Button exitButton;
	// Use this for initialization
	void Awake () {
		EventCenter.Instance.AddListen(EventType.InPasue, ShowButton);
	}
	

	void ShowButton()
    {
		exitButton.gameObject.SetActive(true);
	}

	public void OnExit()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		PhotonManager.Instance.Request(OpCode.GameCode, GameCode.LeaveGame);
		SceneManager.LoadScene(1);
		BGMControl.Instance.ChangeBGM(1);
    }

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen(EventType.InPasue, ShowButton);
	}
}
