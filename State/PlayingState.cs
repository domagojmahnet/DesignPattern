using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.State
{
    class PlayingState : PlayerState
    {
        private static PlayingState instance = new PlayingState();
        private static readonly List<EventType> dopusteneAkcije = new List<EventType>
            { EventType.Gol, EventType.KazneniUdarac, EventType.Autogol, EventType.ZutiKarton,
            EventType.CrveniKarton, EventType.Zamjena};

        private PlayingState() { }

        public static PlayingState Instance()
        {
            return instance;
        }

        public override void UpdateState(Player p, Event e)
        {
            if(e.Vrsta == EventType.ZutiKarton)
            {
                int eMin = e.Min.Contains("+") ? int.Parse(e.Min.Split('+')[0]) + 
                    int.Parse(e.Min.Split('+')[1]) : int.Parse(e.Min);
                var igraceviKartoniUtakmica = Storage.listaDogadaja
                        .Count(x => x.Vrsta == EventType.ZutiKarton
                            && x.Igrač.Ime == e.Igrač.Ime
                            && x.Igrač.Klub.Klub == e.Igrač.Klub.Klub
                            && x.Broj.Broj == e.Broj.Broj
                            && (x.Min.Contains("+") ? int.Parse(x.Min.Split('+')[0]) +
                            int.Parse(x.Min.Split('+')[1]) : int.Parse(x.Min)) <= eMin) ;
                if (igraceviKartoniUtakmica == 1)
                {
                    p.playerState = SentOffState.Instance();

                }
            }
            if (e.Vrsta == EventType.CrveniKarton)
            {
                p.playerState = SentOffState.Instance();
            }
            if (e.Vrsta == EventType.Zamjena)
            {
                p.playerState = SubstitutedState.Instance();
            }
        }

        public override bool CheckValidity(Player p, Event e)
        {
            if (dopusteneAkcije.Contains(e.Vrsta))
            {
                return true;
            }
            return false;
        }
    }
}
