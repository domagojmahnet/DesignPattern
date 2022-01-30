using dmahnet_zadaca_3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Core
{
    public class Club
    {
        public string Klub { get; set; }
        public string Naziv { get; set; }
        public Coach Trener { get; set; }

        public Club(string klub, string naziv, Coach trener)
        {
            Klub = klub;
            Naziv = naziv;
            Trener = trener;
        }

        public Club(string klub, string naziv)
        {
            Klub = klub;
            Naziv = naziv;
        }
    }
}
