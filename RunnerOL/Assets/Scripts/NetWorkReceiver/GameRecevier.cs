using ExitGames.Client.Photon;
using LitJsonEx;
using RunnerCommon;
using RunnerCommon.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRecevier : IRecevier {

    //bool hasInit = false;
    Dictionary<int, PlayerControl> dicIDCon = new Dictionary<int, PlayerControl>();

    public void OnResponse(OperationResponse operationResponse)
    {
        byte subCode = (byte)operationResponse[80];
        switch (subCode)
        {
            case GameCode.GameInit:
                OnGmaeInit(operationResponse);
                break;
            case GameCode.PlayerReady:
                OnPlayerReady(operationResponse);
                break;
            case GameCode.PlayerControl:
                OnPlayerControl(operationResponse);
                break;
            case GameCode.GetBuff:
                OnGetBuff(operationResponse);
                break;
            case GameCode.GetCoin:
                OnGetCoin(operationResponse);
                break;
            case GameCode.GetDamage:
                OnGetDamage(operationResponse);
                break;
            case GameCode.LeaveGame:
                OnLeaveGame(operationResponse);
                break;
        }
    }

    void OnGmaeInit(OperationResponse operationResponse)
    {
        //if (hasInit)
        //    return;
        //hasInit = true;
        dicIDCon = new Dictionary<int, PlayerControl>();
        
        AccoutDto[] dtos = JsonMapper.ToObject<AccoutDto[]>(operationResponse[1].ToString());
        if (dtos == null)
            return;
        int[] playerIDs = dtos.Select(q => q.ID).ToArray();
        if (playerIDs == null)
            return;
        dicIDCon = GameController.Instance.InitPlayer(playerIDs);
        EventCenter.Instance.PostEvent<AccoutDto[]>(EventType.InitOtherScore, dtos);
        ObstacleInitialize.Instance.Init((int)operationResponse[0]); 
    }

    void OnPlayerReady(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == 0)
            ProgressSetting.Instance.gameStart = true;
    }

    void OnPlayerControl(OperationResponse operationResponse)
    {
        int id = (int)operationResponse[0];
        if (!dicIDCon.ContainsKey(id))
            return;
        switch(operationResponse.ReturnCode)
        {
            case 0:
                dicIDCon[id].OnTrunRight();
                break;
            case 1:
                dicIDCon[id].OnTrunLeft();
                break;
            case 2:
                if (id == int.Parse(PlayerCahe.Instance.ID))
                    dicIDCon[id].OnRoll(true);
                else
                    dicIDCon[id].OnRoll(false);
                break;
            case 3:
                dicIDCon[id].OnJump();
                break;

        }
    }

    private void OnGetBuff(OperationResponse operationResponse)
    {
        int id = (int)operationResponse[0];
        int RetCode = operationResponse.ReturnCode;
        if (!dicIDCon.ContainsKey(id))
            return;
        dicIDCon[id].OnGetBuff(RetCode);
    }


    void OnGetCoin(OperationResponse operationResponse)
    {
        int id = (int)operationResponse[0];
        int score = (int)operationResponse[1];
        if (id == int.Parse(PlayerCahe.Instance.ID))
            EventCenter.Instance.PostEvent<int>(EventType.UpdatePlayerScore, score);
        else
            EventCenter.Instance.PostEvent<int, int>(EventType.UpdateOtherScore, id, score);
    }

    void OnGetDamage(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == 0)
        {
            int id = (int)operationResponse[0];
            int lifeLeft = (int)operationResponse[1];
            if (!dicIDCon.ContainsKey(id))
                return;
            if (id == int.Parse(PlayerCahe.Instance.ID))
                EventCenter.Instance.PostEvent<int>(EventType.UpdatePlayerLife, lifeLeft);
            else
                EventCenter.Instance.PostEvent<int, int>(EventType.UpdateOtherLife, id, lifeLeft);
            if (lifeLeft > 0)
                dicIDCon[id].OnDamage();
            else
                dicIDCon[id].OnDead();
        }
        else if (operationResponse.ReturnCode == 1)
        {
            int liveID = (int)operationResponse[0];
            ProgressSetting.Instance.gameOver = true;
            if (liveID == int.Parse(PlayerCahe.Instance.ID))
                EventCenter.Instance.PostEvent<bool>(EventType.ShowSettleMent, true);
            else
                EventCenter.Instance.PostEvent<bool>(EventType.ShowSettleMent, false);
        }
    }

    void OnLeaveGame(OperationResponse operationResponse)
    {
        int leaveID = (int)operationResponse[0];
        if (!dicIDCon.ContainsKey(leaveID))
            return;
        dicIDCon[leaveID].OnDestroy();
        EventCenter.Instance.PostEvent<int>(EventType.RemoveScore, leaveID);
    }
}
