using dmahnet_zadaca_3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmahnet_zadaca_3.State;

namespace dmahnet_zadaca_3.Core
{
    public class Player : Person, ICloneable
    {
        public Club Klub { get; set; }
        public List<string> Pozicije { get; set; }
        public DateTime Rođen { get; set; }

        public PlayerState playerState { get; set; }

        public Player()
        {

        }
        public Player(Club klub, string igrač, List<string> pozicije, DateTime rođen)
        {
            Klub = klub;
            Ime = igrač;
            Pozicije = pozicije;
            Rođen = rođen;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Update(Event e)
        {
            playerState.UpdateState(this, e);
        }

        public bool Validity(Event e)
        {
            return playerState.CheckValidity(this, e);
        }
    }
}
