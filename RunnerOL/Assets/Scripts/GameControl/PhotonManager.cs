using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System.Runtime.InteropServices;
using RunnerCommon;

public class PhotonManager : Singleton<PhotonManager> , IPhotonPeerListener
{

    PhotonPeer peer;
    ConnectionProtocol protocol = ConnectionProtocol.Udp;
    string serverAddress = "192.168.1.3:5055";
    string applicationName = "RunnerServer";
    bool connected = false;

    AccoutRecevier accRecevier;
    MatchRecevier matchRecevier;
    GameRecevier gameRecevier;
    // Use this for initialization
    override protected void Awake() 
    {
        base.Awake();
        peer = new PhotonPeer(this, protocol);
        accRecevier = new AccoutRecevier();
        matchRecevier = new MatchRecevier();
        gameRecevier = new GameRecevier();
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () 
    {
		if(!connected)
            peer.Connect(serverAddress, applicationName);
        peer.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnEvent(EventData eventData)
    {
        
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log(operationResponse.DebugMessage);
        byte code = operationResponse.OperationCode;
        switch(code)
        {
            case OpCode.AccCode:
                accRecevier.OnResponse(operationResponse);
                break;
            case OpCode.MatchCode:
                matchRecevier.OnResponse(operationResponse);
                break;
            case OpCode.GameCode:
                gameRecevier.OnResponse(operationResponse);
                break;
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch(statusCode)
        {
            case StatusCode.Connect:
                connected = true;
                break;
            case StatusCode.Disconnect:
                connected = false;
                break;
        }
    }

    public void Request(byte code, byte subCode, params object[] parameters)
    {
        Dictionary<byte, object> dict = new Dictionary<byte, object>();
        dict[80] = subCode;
        for (int i = 0; i < parameters.Length; i++)
            dict[(byte)i] = parameters[i];
        peer.OpCustom(code, dict, true);
    }

    void OnApplicationQuit()
    {
        peer.Disconnect();
    }
}
