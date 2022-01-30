using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.TableFactory
{
    class ResultTable : ITable
    {
        public List<string> Header { get; set; }
        public List<string[]> Data { get; set; }

        public ResultTable(int? kolo = null, string klub = null)
        {
            Header = Zaglavlje();
            Data = Podaci(kolo, klub);
        }

        public List<string[]> Podaci(int? kolo = null, string klub = null)
        {
            List<string[]> data = new List<string[]>();
            foreach (Game g in Storage.listaUtakmica.Where(x => x.Domaćin.Klub == klub 
                && (kolo == null ? x.Kolo > 0 : x.Kolo <= kolo)))
            {
                if (!Storage.listaDogadaja.Any(y => y.Broj == g)) continue;
                data.Add(rezultatDomacin(klub, g));

            }
            foreach (Game g in Storage.listaUtakmica.Where(x => x.Gost.Klub == klub 
                && (kolo == null ? x.Kolo > 0 : x.Kolo <= kolo)))
            {
                if (!Storage.listaDogadaja.Any(y => y.Broj == g)) continue;
                data.Add(rezultatGost(klub, g));
            }
            return data.OrderBy(o => int.Parse(o[0])).ToList();
        }

        public List<string> Zaglavlje()
        {
            return new List<string> { "Kolo", "Datum", "Domaćin", "Gost", "Rezultat" };
        }

        public string[] rezultatDomacin(string klub, Game g)
        {
            var zabijeniGolovi = Storage.listaDogadaja.Where(x => x.Broj == g)
                    .Count(x => (((x.Vrsta == EventType.Gol || x.Vrsta == EventType.KazneniUdarac)
                        && (x.Klub.Klub == klub && x.Igrač.Klub.Klub == g.Domaćin.Klub))
                        || (x.Vrsta == EventType.Autogol && x.Igrač.Klub.Klub == g.Gost.Klub 
                        && x.Broj.Domaćin.Klub == g.Domaćin.Klub)));

            var primljeniGolovi = Storage.listaDogadaja.Where(x => x.Broj == g)
                .Count(x => (((x.Vrsta == EventType.Gol || x.Vrsta == EventType.KazneniUdarac)
                    && g.Domaćin.Klub == klub && x.Broj.Gost.Klub == g.Gost.Klub 
                    && x.Igrač.Klub.Klub == g.Gost.Klub)
                    || (x.Vrsta == EventType.Autogol && x.Igrač.Klub.Klub == g.Domaćin.Klub 
                    && x.Broj.Gost.Klub == g.Gost.Klub)));

            string[] rezultatDomacin = { g.Kolo.ToString(), g.Početak.ToString(), g.Domaćin.Naziv, 
                g.Gost.Naziv, zabijeniGolovi + ":" + primljeniGolovi };

            return rezultatDomacin;
        }

        public string[] rezultatGost(string klub, Game g)
        {
            var zabijeniGolovi = Storage.listaDogadaja.Where(x => x.Broj == g)
                     .Count(x => (((x.Vrsta == EventType.Gol || x.Vrsta == EventType.KazneniUdarac)
                         && g.Gost.Klub == klub && x.Broj.Gost.Klub == g.Gost.Klub 
                         && x.Igrač.Klub.Klub == g.Gost.Klub)
                         || (x.Vrsta == EventType.Autogol && x.Igrač.Klub.Klub == g.Domaćin.Klub 
                         && x.Broj.Gost.Klub == g.Gost.Klub)));

            var primljeniGolovi = Storage.listaDogadaja.Where(x => x.Broj == g)
                .Count(x => (((x.Vrsta == EventType.Gol || x.Vrsta == EventType.KazneniUdarac)
                    && g.Gost.Klub == klub && x.Broj.Gost.Klub == g.Gost.Klub 
                    && x.Igrač.Klub.Klub == g.Domaćin.Klub)
                    || (x.Vrsta == EventType.Autogol && x.Igrač.Klub.Klub == g.Gost.Klub 
                    && x.Broj.Gost.Klub == g.Gost.Klub)));

            string[] rezultatDomacin = { g.Kolo.ToString(), g.Početak.ToString(), g.Domaćin.Naziv, 
                g.Gost.Naziv, primljeniGolovi + ":" + zabijeniGolovi };

            return rezultatDomacin;
        }
    }
}
