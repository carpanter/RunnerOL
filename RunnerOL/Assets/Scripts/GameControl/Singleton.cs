using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
	private static T _instance;
	public static T Instance { get { return _instance; } }

	// Use this for initialization
	virtual protected void Awake () {
		if (_instance == null)
			_instance = this as T;
	}

}
