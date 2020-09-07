using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class RoomCacheBase<T> where T:RoomBase
    {
        protected Dictionary<int, T> dicIDRomm = new Dictionary<int, T>();
        protected Dictionary<RunnerClient, T> dicPlayerRoom = new Dictionary<RunnerClient, T>();
        protected Queue<T> QueueRoom = new Queue<T>();
        protected int id = 0;

        public T GetRoomByClient(RunnerClient client)
        {
            T room;
            dicPlayerRoom.TryGetValue(client, out room);
            return room;
        }
    }
}
