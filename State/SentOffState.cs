using dmahnet_zadaca_3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.State
{
    class SentOffState : PlayerState
    {
        private static SentOffState instance = new SentOffState();

        private SentOffState() { }

        public static SentOffState Instance()
        {
            return instance;
        }

        public override bool CheckValidity(Player p, Event e)
        {
            return false;
        }

        public override void UpdateState(Player p, Event e)
        {

        }
    }
}
