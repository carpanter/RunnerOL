using LitJsonEx;
using Photon.SocketServer;
using RunnerCommon;
using RunnerServer.Cache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Logic
{
    public class MatchHandler : SingleSend, IHandler
    {
        public void OnDisConnect(RunnerClient client)
        {
            OnMatchCancel(client);
        }

        public void OnRequest(RunnerClient client, byte subCode, OperationRequest operationRequest)
        {
            switch(subCode)
            {
                case MatchCode.MatchStar:
                    OnMatchStart(client);
                    break;
                case MatchCode.MatchCancel:
                    OnMatchCancel(client);
                    break;
            }
        }

        void OnMatchStart(RunnerClient client)
        {
            int playerID = Caches.accoutCache.GetIDbyClient(client);
            if (playerID == -1)
                return;
            bool isFull = Caches.matchCache.EnterRoom(client, playerID);
            if(isFull)
            {
                RoomBase matchRoom = Caches.matchCache.GetRoomByClient(client);
                if (matchRoom != null)
                {
                    foreach(var roomClient in matchRoom.dicIDClient)
                    {
                        Caches.gameCache.EnterRoom(roomClient.Value, roomClient.Key);
                    }
                    Caches.matchCache.DestroyRoom(matchRoom);
                    GameRoom gameRoom = Caches.gameCache.GetRoomByClient(client);
                    gameRoom.seed = (int)DateTime.Now.Ticks;
                    gameRoom.Brocast(OpCode.MatchCode, MatchCode.MatchStar, 0, "匹配完成，开始游戏");
                }
            }
        }

        void OnMatchCancel(RunnerClient client)
        {
            int playerID = Caches.accoutCache.GetIDbyClient(client);
            if (playerID == -1)
                return;
            Caches.matchCache.LeaveRoom(client, playerID);
        }
    }
}
