using dmahnet_zadaca_3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.State
{
    public abstract class PlayerState
    {
        public abstract void UpdateState(Player player, Event e);
        public abstract bool CheckValidity(Player player, Event e);
    }
}
