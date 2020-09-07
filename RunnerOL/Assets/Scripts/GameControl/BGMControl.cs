using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl : Singleton<BGMControl> {

	[SerializeField]
	AudioClip GameBGM;
	[SerializeField]
	AudioClip UIBGM;

	AudioSource audio;
	new void Awake () {
		base.Awake();
		audio = gameObject.GetComponent<AudioSource>();
		DontDestroyOnLoad(this);
	}

	public void ChangeBGM(int sceneNum)
    {
		if (audio == null)
			return;
		if (sceneNum == 1)
			audio.clip = UIBGM;
		else if (sceneNum == 2)
			audio.clip = GameBGM;
		audio.Play();
	}

	public void TrunOnBGM(bool turnOn)
    {
		if (turnOn)
			audio.volume = 0.6f;
		else
			audio.volume = 0;
	}
}
