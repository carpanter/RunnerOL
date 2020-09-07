using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCahe : Singleton<PlayerCahe> {

    private string id;
    private string playerName;

    public string ID { get { return id; } set { id = value;}  }
    public string PlayerName { get { return playerName; } set { playerName = value; } }

    new void Awake () {
        base.Awake();
		DontDestroyOnLoad(this);
	}
	
}
