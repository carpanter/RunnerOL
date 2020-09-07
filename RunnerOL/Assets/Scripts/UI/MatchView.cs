using RunnerCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchView : MonoBehaviour {

	[SerializeField]
	Text nameText;
	[SerializeField]
	Animation ModelAnim;
	[SerializeField]
	Text timeShow;

	double Matchtime = 0;
	bool isMatching = false;
	DateTime timeStart;
	// Use this for initialization
	void Awake () {
		EventCenter.Instance.AddListen(EventType.LoadGame, LoadGame);
		nameText.text = PlayerCahe.Instance.PlayerName;
		ModelAnim.Stop();
	}

	void Update()
    {
		if (isMatching)
			Matchtime = (DateTime.Now - timeStart).TotalSeconds;
		timeShow.text = string.Format("{0:d2}:{1:d2}", (int)(Matchtime / 60), (int)Matchtime % 60);
    }

	public void OnMatchStartClick()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		ModelAnim.Play();
		isMatching = true;
		timeStart = DateTime.Now;
		PhotonManager.Instance.Request(OpCode.MatchCode, MatchCode.MatchStar);
    }

	public void OnCancelClick()
    {
		if (SoundsManager.Instance.SoundsOn)
			SoundsManager.Instance.PlayAudioByName("button");
		ModelAnim.Stop();
		isMatching = false;
		Matchtime = 0;
		PhotonManager.Instance.Request(OpCode.MatchCode, MatchCode.MatchCancel);
	}
	
	void LoadGame()
    {
		StartCoroutine("LoadGameDelate");
    }

	IEnumerator LoadGameDelate()
    {
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(2);
		BGMControl.Instance.ChangeBGM(2);
    }

    private void OnDestroy()
    {
		EventCenter.Instance.RemoveListen(EventType.LoadGame, LoadGame);
	}

}
