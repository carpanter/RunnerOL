using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : Singleton<SoundsManager> {

	public AudioClip[] audioClips;
	public bool SoundsOn = true;
	
	new void Awake()
    {
		base.Awake();
		DontDestroyOnLoad(this);
    }

	public void PlayAudioByName(string name)
    {
		foreach(var audio in audioClips)
        {
			if (audio.name == name)
				AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
        }
    }
}
