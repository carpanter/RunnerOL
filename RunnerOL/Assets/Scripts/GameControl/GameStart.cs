using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		SceneManager.LoadScene(1);
	}
	
}
