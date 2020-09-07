using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class Caches
    {
        public static AccoutCache accoutCache = new AccoutCache();
        public static MatchRoomCache matchCache = new MatchRoomCache();
        public static GameRoomCache gameCache = new GameRoomCache();
    }
}
