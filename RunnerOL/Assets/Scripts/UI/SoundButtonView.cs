using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonView : MonoBehaviour {

	[SerializeField]
	GameObject ButtonOn;
	[SerializeField]
	GameObject ButtonOff;

	public void OnSoundsButtonOn()
	{
		SoundsManager.Instance.SoundsOn = true;
		BGMControl.Instance.TrunOnBGM(true);
	}

	public void OnSoundsButtonOff()
	{
		SoundsManager.Instance.SoundsOn = false;
		BGMControl.Instance.TrunOnBGM(false);
	}

	void Update()
    {
		if (SoundsManager.Instance.SoundsOn && ButtonOn.activeSelf)
        {
			ButtonOn.SetActive(false);
			ButtonOff.SetActive(true);
		}
		if (!SoundsManager.Instance.SoundsOn && ButtonOff.activeSelf)
		{
			ButtonOn.SetActive(true);
			ButtonOff.SetActive(false);
		}

	}
}
