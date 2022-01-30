
using dmahnet_zadaca_3.State;

namespace dmahnet_zadaca_3.Core
{
    public class Team
    {
        public Game Broj { get; set; }
        public Club Klub { get; set; }
        public string Vrsta { get; set; }
        public Player Igrač { get; set; }
        public string Pozicija { get; set; }


        public Team(Game broj, Club klub, string vrsta, Player igrač, string pozicija)
        {
            Broj = broj;
            Klub = klub;
            Vrsta = vrsta;
            Igrač = igrač;
            Pozicija = pozicija;

            if(vrsta == "S") 
            {
                Igrač.playerState = PlayingState.Instance();
            }
            else
            {
                Igrač.playerState = SubstituteState.Instance();
            }
        }

    }
}
