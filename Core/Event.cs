using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Core
{
    public class Event
    {
        public Game Broj { get; set; }
        public string Min { get; set; }
        public EventType Vrsta { get; set; }
        public Club Klub { get; set; }
        public Player Igrač { get; set; }
        public Player Zamjena { get; set; }

        public Event() { }

    }

    public class EventBuilder : IEventBuilder
    {
        private Event _event = new Event();
        public EventBuilder(Game broj, string min, EventType vrsta)
        {
            _event.Broj = broj;
            _event.Min = min;
            _event.Vrsta = vrsta;
        }

        public static EventBuilder Init(Game broj, string min, EventType vrsta)
        {
            return new EventBuilder(broj, min, vrsta);
        }

        public Event Build() => _event;

        public EventBuilder SetIgrač(Player igrač)
        {
            _event.Igrač = igrač;
            return this;
        }

        public EventBuilder SetKlub(Club klub)
        {
            _event.Klub = klub;
            return this;
        }

        public EventBuilder SetZamjena(Player zamjena)
        {
            _event.Zamjena = zamjena;
            return this;
        }
    }
}
