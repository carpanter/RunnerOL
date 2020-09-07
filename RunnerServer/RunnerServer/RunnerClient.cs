using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnerCommon;
using RunnerServer.Logic;

namespace RunnerServer
{
    public class RunnerClient : ClientPeer
    {
        AccHandler accHandler;
        MatchHandler matchHandler;
        GameHandler GameHandler;
        public RunnerClient(InitRequest initRequest) : base(initRequest)
        {
            accHandler = new AccHandler();
            matchHandler = new MatchHandler();
            GameHandler = new GameHandler();
            RunnerAppliacation.Log("一个玩家上线了");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            GameHandler.OnDisConnect(this);
            matchHandler.OnDisConnect(this);
            accHandler.OnDisConnect(this);
            
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            byte subCode = (byte)operationRequest[80];
            switch(operationRequest.OperationCode)
            {
                case OpCode.AccCode:
                    accHandler.OnRequest(this, subCode, operationRequest);
                    break;
                case OpCode.MatchCode:
                    matchHandler.OnRequest(this, subCode, operationRequest);
                    break;
                case OpCode.GameCode:
                    GameHandler.OnRequest(this, subCode, operationRequest);
                    break;
            }
        }
    }
}
