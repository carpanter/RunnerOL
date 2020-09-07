using LitJsonEx;
using Photon.SocketServer;
using RunnerCommon;
using RunnerCommon.Dto;
using RunnerServer.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Logic
{
    public class GameHandler : SingleSend, IHandler
    {
        public void OnDisConnect(RunnerClient client)
        {
            GameRoom room = Caches.gameCache.GetRoomByClient(client);
            OnLeaveGame(client, room);   
        }

        public void OnRequest(RunnerClient client, byte subCode, OperationRequest operationRequest)
        {
            GameRoom room = Caches.gameCache.GetRoomByClient(client);
            if (room == null)
                return;

            switch (subCode)
            {
                case GameCode.GameInit:
                    OnGameInit(client, room);
                    break;
                case GameCode.PlayerReady:
                    OnPlayerReady(client, room);
                    break;
                case GameCode.PlayerControl:
                    OnPlayerControl(client, room, operationRequest);
                    break;
                case GameCode.GetBuff:
                    OnGetBuff(client, room, operationRequest);
                    break;
                case GameCode.GetCoin:
                    OnGetCoin(client, room, operationRequest);
                    break;
                case GameCode.GetDamage:
                    OnGetDamage(client, room, operationRequest);
                    break;
                case GameCode.LeaveGame:
                    OnLeaveGame(client, room);
                    break;
            }
        }

        void OnGameInit(RunnerClient client , GameRoom room)
        {
            List<AccoutDto> listDto = new List<AccoutDto>();
            foreach(var RoomClient in room.dicIDClient.Values)
            {
                AccoutDto dto = Caches.accoutCache.GetDtoByClient(RoomClient);
                listDto.Add(dto);
            }
            send(client, OpCode.GameCode, GameCode.GameInit, 0, "游戏初始化完成", room.seed, JsonMapper.ToJson(listDto.ToArray()));
            RunnerAppliacation.Log(room.seed.ToString());
        }

        void OnPlayerReady(RunnerClient client, GameRoom room)
        {
            int id = GetID(client, room);
            if (room.PlayerReady(id))
                room.Brocast(OpCode.GameCode, GameCode.PlayerReady, 0, "所有玩家已准备，游戏开始");
        }

        void OnPlayerControl(RunnerClient client, GameRoom room, OperationRequest operationRequest)
        {
            int id = GetID(client, room);   
            byte retCode = byte.Parse(operationRequest[0].ToString());
            room.Brocast(OpCode.GameCode, GameCode.PlayerControl, retCode, string.Empty, id);
        }

        void OnGetBuff(RunnerClient client, GameRoom room, OperationRequest operationRequest)
        {
            int id = GetID(client, room);
            byte retCode = byte.Parse(operationRequest[0].ToString());
            room.Brocast(OpCode.GameCode, GameCode.GetBuff, retCode, string.Empty, id);
        }


        void OnGetCoin(RunnerClient client, GameRoom room, OperationRequest operationRequest)
        {
            int id = GetID(client, room);
            bool multiply = bool.Parse(operationRequest[0].ToString());
            int score = room.AddCoin(id, multiply);
            if (score == -1)
                return;
            room.Brocast(OpCode.GameCode, GameCode.GetCoin, 0, string.Empty, id, score);
        }


        void OnGetDamage(RunnerClient client, GameRoom room, OperationRequest operationRequest)
        {
            int id = GetID(client, room);
            int damages = (int)operationRequest[0];
            int lifeLeft = room.GetDamage(id, damages);
            room.Brocast(OpCode.GameCode, GameCode.GetDamage, 0, string.Empty, id, lifeLeft);
            int liveID = room.CheckOver();
            if (liveID != -1)
            {
                room.Brocast(OpCode.GameCode, GameCode.GetDamage, 1, string.Empty, liveID);
                Caches.gameCache.DestroyRoom(room);
            }
        }

        void OnLeaveGame(RunnerClient client, GameRoom room)
        {
            int playerID = Caches.accoutCache.GetIDbyClient(client);
            if (playerID == -1)
                return;
            Caches.gameCache.LeaveRoom(client, playerID);
            room.Brocast(OpCode.GameCode, GameCode.LeaveGame, 0, string.Empty, playerID);
            int liveID = room.CheckOver();
            if (liveID != -1)
            {
                room.Brocast(OpCode.GameCode, GameCode.GetDamage, 1, string.Empty, liveID);
                Caches.gameCache.DestroyRoom(room);
            }
        }


        int GetID(RunnerClient client, GameRoom room)
        {
            var keyValue = room.dicIDClient.Where(q => q.Value == client);
            if (keyValue == null)
                return -1;
            int id = keyValue.Select(t => t.Key).FirstOrDefault();
            return id;
        }

    }
}
