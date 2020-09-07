using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class GameRoomCache : RoomCacheBase<GameRoom>
    {
        public bool EnterRoom(RunnerClient client, int plyerID)
        {
            GameRoom room;
            foreach (var item in dicIDRomm.Values)
            {
                room = item;
                if (room.isFull())
                    continue;
                room.EnterRoom(client, plyerID);
                dicPlayerRoom.Add(client, room);
                return room.isFull();
            }
            if (QueueRoom.Count > 0)
            {
                room = QueueRoom.Dequeue();
                dicIDRomm.Add(room.ID, room);
                room.EnterRoom(client, plyerID);
                dicPlayerRoom.Add(client, room);
                return room.isFull();
            }
            room = new GameRoom(id++, 2);
            dicIDRomm.Add(room.ID, room);
            room.EnterRoom(client, plyerID);
            dicPlayerRoom.Add(client, room);
            return room.isFull();

        }

        public bool LeaveRoom(RunnerClient client, int plyerID)
        {
            GameRoom room;
            if (!dicPlayerRoom.TryGetValue(client, out room))
                return false;
            if (!room.LeaveRoom(client, plyerID))
                return false;
            dicPlayerRoom.Remove(client);
            if (room.isEmpty())
            {
                dicIDRomm.Remove(room.ID);
                QueueRoom.Enqueue(room);
            }
            return true;
        }

        public void DestroyRoom(GameRoom room)
        {
            if (dicIDRomm.ContainsKey(room.ID))
                dicIDRomm.Remove(room.ID);
            foreach (var client in room.dicIDClient.Values)
            {
                if (dicPlayerRoom.ContainsKey(client))
                    dicPlayerRoom.Remove(client);
            }
            room.dicIDClient = new Dictionary<int, RunnerClient>();
            room.dicIDAtt = new Dictionary<int, PlayerAttribute>();
            room.seed = 0;
            QueueRoom.Enqueue(room);
        }
    }
}
