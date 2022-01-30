using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.State
{
    class SubstituteState : PlayerState
    {
        private static SubstituteState instance = new SubstituteState();

        private SubstituteState() { }

        public static SubstituteState Instance()
        {
            return instance;
        }

        public override bool CheckValidity(Player p, Event e)
        {
            if (e.Vrsta == EventType.Zamjena)
            {
                return true;
            }
            return false;
        }

        public override void UpdateState(Player p, Event e)
        {
            p.playerState = PlayingState.Instance();
        }
    }
}
