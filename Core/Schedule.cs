using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Core
{
    public class Schedule
    {
        public int RedniBroj { get; set; }
        public DateTime VrijemeKreiranja { get; set; }
        public List<Game> Utakmice { get; set; }
        public bool VazeciRaspored { get; set; }

        public Schedule(int redniBroj, DateTime vrijemeKreiranja, List<Game> utakmice, bool vazeciRaspored)
        {
            RedniBroj = redniBroj;
            VrijemeKreiranja = vrijemeKreiranja;
            Utakmice = utakmice;
            VazeciRaspored = vazeciRaspored;
        }
    }
}
