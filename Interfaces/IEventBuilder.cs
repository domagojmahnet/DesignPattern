using dmahnet_zadaca_3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Interfaces
{
    public interface IEventBuilder
    {
        EventBuilder SetKlub(Club klub);
        EventBuilder SetIgrač(Player igrač);
        EventBuilder SetZamjena(Player zamjena);
    }
}
