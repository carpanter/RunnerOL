using ExitGames.Client.Photon;
using RunnerCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccoutRecevier : IRecevier {

    public void OnResponse(OperationResponse operationResponse)
    {
        byte subCode = (byte)operationResponse[80];
        switch(subCode)
        {
            case AccoutCode.Register:
                OnRegister(operationResponse);
                break;
            case AccoutCode.Sigin:
                OnSigin(operationResponse);
                break;
            case AccoutCode.CreateName:
                OnCreateName(operationResponse);
                break;
        }
    }

    void OnRegister(OperationResponse operationResponse)
    {
        string msg = operationResponse.DebugMessage;
        EventCenter.Instance.PostEvent(EventType.RegisterAccount, msg);
    }

    void OnSigin(OperationResponse operationResponse)
    {
        byte retCode = (byte)operationResponse.ReturnCode;
        string msg = operationResponse.DebugMessage;
        if (retCode == 0)
        {
            //EventCenter.Instance.PostEvent(EventType.SiginSucessWithName);
            PlayerCahe.Instance.PlayerName = operationResponse[0].ToString();
            PlayerCahe.Instance.ID = operationResponse[1].ToString();
        }
        else if (retCode == 1)
        {
            //EventCenter.Instance.PostEvent(EventType.SiginSucessWithouName);
            PlayerCahe.Instance.ID = operationResponse[0].ToString();
        }
        else if (retCode == 2)
            EventCenter.Instance.PostEvent(EventType.SiginFalse, msg);
    }

    void OnCreateName(OperationResponse operationResponse)
    {
        byte retCode = (byte)operationResponse.ReturnCode;
        string msg = operationResponse.DebugMessage;
        if (retCode == 0)
            EventCenter.Instance.PostEvent(EventType.CreateSucces);
        else
            EventCenter.Instance.PostEvent(EventType.CreateFalse, msg);
    }
}
