using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer.Cache
{
    public class PlayerAttribute
    {
        public int score;
        public int life;
        public bool isReady;

        public PlayerAttribute()
        {
            this.score = 0;
            this.life = 3;
            this.isReady = false;
        }
    }
}
