using ExitGames.Client.Photon;
using RunnerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchRecevier : MonoBehaviour ,IRecevier {
    public void OnResponse(OperationResponse operationResponse)
    {
        byte subCode = (byte)operationResponse[80];
        if(subCode == MatchCode.MatchStar && operationResponse.ReturnCode == 0)
        {
            EventCenter.Instance.PostEvent(EventType.LoadGame);
        }
    }
}
