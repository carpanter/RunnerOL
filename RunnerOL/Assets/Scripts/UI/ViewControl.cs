using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour {

	[SerializeField]
	GameObject accView;
	[SerializeField]
	GameObject CreateView;
	[SerializeField]
	GameObject MatchView;
	
	// Update is called once per frame
	void Update () {
		if(string.IsNullOrEmpty(PlayerCahe.Instance.PlayerName) && string.IsNullOrEmpty(PlayerCahe.Instance.ID))
		{
			accView.SetActive(true);
			CreateView.SetActive(false);
			MatchView.SetActive(false);
		}
		else if (string.IsNullOrEmpty(PlayerCahe.Instance.PlayerName) && !string.IsNullOrEmpty(PlayerCahe.Instance.ID))
		{
			accView.SetActive(false);
			CreateView.SetActive(true);
			MatchView.SetActive(false);
		}
		else if (!string.IsNullOrEmpty(PlayerCahe.Instance.PlayerName) && !string.IsNullOrEmpty(PlayerCahe.Instance.ID))
		{
			accView.SetActive(false);
			CreateView.SetActive(false);
			MatchView.SetActive(true);
		}

	}
}
