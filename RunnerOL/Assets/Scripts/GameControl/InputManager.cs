using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {

    public bool TrunLeftSend;
    public bool TrunRightSend;
    public bool RollSend;
    public bool JumpSend;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ProgressSetting.Instance.gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.D) && TrunRightSend)
        {
            PhotonManager.Instance.Request(OpCode.GameCode, GameCode.PlayerControl, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && TrunLeftSend)
        {
            PhotonManager.Instance.Request(OpCode.GameCode, GameCode.PlayerControl, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && RollSend)
        {
            PhotonManager.Instance.Request(OpCode.GameCode, GameCode.PlayerControl, 2);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && JumpSend)
        {
            PhotonManager.Instance.Request(OpCode.GameCode, GameCode.PlayerControl, 3);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            EventCenter.Instance.PostEvent(EventType.InPasue);
        }
    }
}
