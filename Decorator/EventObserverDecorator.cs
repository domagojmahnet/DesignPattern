using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Decorator
{
    public class EventObserverDecorator : EventObserver
    {
        protected EventObserver eventObserver;
        public EventObserverDecorator(EventObserver eo)
        {
            eventObserver = eo;
        }
        public override string GolString(Event e)
        {
            int brojGolovaUSezoni = Storage.listaDogadaja.Count(x => x.Igrač == e.Igrač
                && (x.Vrsta == Enums.EventType.Gol || x.Vrsta == Enums.EventType.KazneniUdarac)
                && x.Broj.Kolo < e.Broj.Kolo);

            int eMin = e.Min.Contains("+") ? int.Parse(e.Min.Split('+')[0]) +
                    int.Parse(e.Min.Split('+')[1]) : int.Parse(e.Min);

            brojGolovaUSezoni += Storage.listaDogadaja.Count(x => x.Igrač == e.Igrač
                && (x.Vrsta == Enums.EventType.Gol || x.Vrsta == Enums.EventType.KazneniUdarac)
                && x.Broj == e.Broj && (x.Min.Contains("+") ? int.Parse(x.Min.Split('+')[0]) +
                int.Parse(x.Min.Split('+')[1]) : int.Parse(x.Min)) <= eMin);

            return eventObserver.GolString(e) + " (" + brojGolovaUSezoni + ")";
        }
    }
}
