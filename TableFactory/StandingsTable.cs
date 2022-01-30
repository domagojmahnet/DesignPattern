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
    class StandingsTable : ITable
    {
        public List<string> Header { get; set; }
        public List<string[]> Data { get; set; }

        public StandingsTable(int? kolo = null, string klub = null)
        {
            Header = Zaglavlje();
            Data = Podaci(kolo, klub);
        }

        public List<string[]> Podaci(int? kolo = null, string klub = null)
        {
            List<string[]> data = new List<string[]>();
            foreach (Club k in Storage.listaKlubova)
            {
                int pobjede = 0, nerijeseno = 0, porazi = 0, zabijeni = 0, primljeni = 0, 
                    odigrane = 0, brojbodova = 0;
                foreach (Game g in Storage.listaUtakmica
                    .Where(x => (x.Domaćin.Klub == k.Klub || x.Gost.Klub == k.Klub) 
                    && (kolo == null ? x.Kolo > 0 : x.Kolo <= kolo)))
                {
                    if (!Storage.listaDogadaja.Any(y => y.Broj == g)) continue;
                    string[] rezultat = izracunajRezultat(k.Klub, g);
                    int zabijeniUtakmica = int.Parse(rezultat[0]);
                    int primljeniUtakmica = int.Parse(rezultat[1]);
                    zabijeni += zabijeniUtakmica;
                    primljeni += primljeniUtakmica;
                    odigrane++;
                    if (zabijeniUtakmica > primljeniUtakmica)
                    {
                        pobjede++;
                        brojbodova += 3;
                    }
                    if (zabijeniUtakmica == primljeniUtakmica)
                    {
                        nerijeseno++;
                        brojbodova++;
                    }
                    if (zabijeniUtakmica < primljeniUtakmica) porazi++;
                }
                string[] podaciKlub = { k.Naziv, k.Trener.Ime, odigrane.ToString(), pobjede.ToString(),
                    nerijeseno.ToString(), porazi.ToString(), zabijeni.ToString(), 
                    primljeni.ToString(), (zabijeni-primljeni).ToString(),
                    brojbodova.ToString()};
                data.Add(podaciKlub);
            }
            data = data.OrderByDescending(o => int.Parse(o[9])).ThenByDescending(o => int.Parse(o[8]))
                .ThenByDescending(o => int.Parse(o[6])).ThenByDescending(o => int.Parse(o[3])).ToList();
            data.Add(izracunajSumarne(data));
            return data;
        }

        private string[] izracunajSumarne(List<string[]> data)
        {
            string[] sumarni = { 
                "",
                "", 
                "", 
                data.Sum(o => int.Parse(o[3])).ToString(), 
                data.Sum(o => int.Parse(o[4])).ToString(),
                data.Sum(o => int.Parse(o[5])).ToString(),
                data.Sum(o => int.Parse(o[6])).ToString(),
                data.Sum(o => int.Parse(o[7])).ToString(),
                "",
                data.Sum(o => int.Parse(o[9])).ToString()
            };
            return sumarni;
        }

        public List<string> Zaglavlje()
        {
            return new List<string> { "Klub", "Trener", "Odigrano", "Pobjede", "Nerijeseno", "Porazi",
                "Zabijeno", "Primljeno", "Razlika", "Bodovi"};
        }

        public string[] izracunajRezultat(string klub, Game g)
        {
            if (g.Domaćin.Klub == klub)
            {
                return rezultatDomacin(klub, g);
            }
            else if (g.Gost.Klub == klub)
            {

                return rezultatGost(klub, g);
            }
            return null;
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

            return new string[] { zabijeniGolovi.ToString(), primljeniGolovi.ToString() };
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

            return new string[] { zabijeniGolovi.ToString(), primljeniGolovi.ToString() };
        }
    }
}
