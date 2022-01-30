using dmahnet_zadaca_3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Core
{
    public class Coach : Person
    {
        public Coach(string trener)
        {
            Ime = trener;
        }
    }
}
