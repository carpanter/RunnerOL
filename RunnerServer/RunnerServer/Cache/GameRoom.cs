using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class GameRoom : RoomBase
    {
        public Dictionary<int ,PlayerAttribute> dicIDAtt;
        public int seed;

        public GameRoom(int id, int count) : base(id, count)
        {
            dicIDAtt = new Dictionary<int, PlayerAttribute>();
        }

        public new bool EnterRoom(RunnerClient client, int playerID)
        {
            if (!base.EnterRoom(client, playerID))
                return false;
            if (dicIDAtt.ContainsKey(playerID))
                return false;
            dicIDAtt.Add(playerID, new PlayerAttribute());
            return true;
        }

        public new bool LeaveRoom(RunnerClient client, int playerID)
        {
            if (!base.LeaveRoom(client, playerID))
                return false;
            if (!dicIDAtt.ContainsKey(playerID))
                return false;
            dicIDAtt.Remove(playerID);
            return true;
        }

        public bool PlayerReady(int id)
        {
            if (!dicIDAtt.ContainsKey(id))
                return false;
            dicIDAtt[id].isReady = true;
            foreach(var player in dicIDAtt.Values)
            {
                if (player.isReady == false)
                    return false;
            }
            return true;
        }

        public int AddCoin(int id,bool multiply)
        {
            if(!dicIDAtt.ContainsKey(id))
                return -1;
            dicIDAtt[id].score += (multiply == true ? 2 : 1) * 10;
            return dicIDAtt[id].score;
        }

        public int GetDamage(int id,int damages)
        {
            if (!dicIDAtt.ContainsKey(id))
                return -1;
            dicIDAtt[id].life -= damages;
            if (dicIDAtt[id].life < 0)
                dicIDAtt[id].life = 0;
            return dicIDAtt[id].life;
        }

        public int CheckOver()
        {
            int liveCout = 0;
            int liveID = -1;
            foreach(var item in dicIDAtt)
            {
                if(item.Value.life > 0)
                {
                    liveCout++;
                    liveID = item.Key;
                }
            }
            if (liveCout > 1)
                return -1;
            else
                return liveID;
        }
    }
}
