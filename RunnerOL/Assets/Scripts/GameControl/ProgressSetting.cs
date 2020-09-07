using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSetting : Singleton<ProgressSetting> {

	public bool gameStart = false;
	public bool gameOver = false;
	// Use this for initialization
	

	public void OnGameStartButton()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		PhotonManager.Instance.Request(OpCode.GameCode, GameCode.PlayerReady);
	}
}
