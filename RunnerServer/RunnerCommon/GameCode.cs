using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerCommon
{
    public class GameCode
    {
        public const byte GameInit = 0;
        public const byte PlayerReady = 1;
        public const byte PlayerControl = 2;
        public const byte GetBuff = 3;
        public const byte GetCoin = 4;
        public const byte GetDamage = 5;
        public const byte LeaveGame = 6;
    }
}
