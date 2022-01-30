using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace dmahnet_zadaca_3.TableFactory
{
    class ScorerTable : ITable
    {
        public List<string> Header { get; set; }
        public List<string[]> Data { get; set; }

        public ScorerTable(int? kolo = null, string klub = null)
        {
            Header = Zaglavlje();
            Data = Podaci(kolo, klub);
        }

        public List<string[]> Podaci(int? kolo = null, string klub = null)
        {
            List<string[]> data = new List<string[]>();
            foreach(Player p in Storage.listaIgrača)
            {
                var brojGolova = Storage.listaDogadaja
                    .Count(x =>(x.Vrsta == EventType.Gol || x.Vrsta == EventType.KazneniUdarac) 
                        && x.Igrač.Ime == p.Ime 
                        && (kolo == null ?  x.Broj.Kolo > 0 : x.Broj.Kolo <= kolo));

                if (brojGolova > 0)
                {
                    string[] podaciIgrac = { p.Ime, p.Klub.Naziv, brojGolova.ToString() };
                    data.Add(podaciIgrac);
                }
            }
            data = data.OrderByDescending(o => int.Parse(o[2])).ToList();
            string[] sumarni = { "", "", data.Sum(o => int.Parse(o[2])).ToString() };
            data.Add(sumarni);
            return data;
        }

        public List<string> Zaglavlje()
        {
            return new List<string> { "Igrač", "Klub", "Golovi"};
        }
    }
}
