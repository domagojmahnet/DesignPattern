using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace dmahnet_zadaca_3.TableFactory
{
    class CardTable : ITable
    {
        public List<string> Header { get; set; }
        public List<string[]> Data { get; set; }

        public CardTable(int? kolo = null, string klub = null)
        {
            Header = Zaglavlje();
            Data = Podaci(kolo, klub);
        }

        public List<string[]> Podaci(int? kolo = null, string klub = null)
        {
            List<string[]> data = new List<string[]>();
            foreach (Club k in Storage.listaKlubova)
            {
                var brojZutih = Storage.listaDogadaja
                    .Count(x => x.Vrsta == EventType.ZutiKarton 
                        && x.Igrač.Klub.Klub == k.Klub 
                        && (kolo == null ? x.Broj.Kolo > 0 : x.Broj.Kolo <= kolo));

                var brojDvaZuta = 0;
                foreach(Game g in Storage.listaUtakmica)
                {
                    foreach (Player p in Storage.listaIgrača.Where(x => x.Klub.Klub == k.Klub))
                    {
                        var igraceviKartoniUtakmica = Storage.listaDogadaja
                            .Count(x => x.Vrsta == EventType.ZutiKarton
                                && x.Igrač.Ime == p.Ime
                                && x.Igrač.Klub.Klub == k.Klub 
                                && x.Broj.Broj == g.Broj
                                && (kolo == null ? x.Broj.Kolo > 0 : x.Broj.Kolo <= kolo));
                        if (igraceviKartoniUtakmica == 2) brojDvaZuta++;
                    }
                }

                var brojCrvenih = Storage.listaDogadaja
                    .Count(x => x.Vrsta == EventType.CrveniKarton 
                        && x.Igrač.Klub.Klub == k.Klub
                        && (kolo == null ? x.Broj.Kolo > 0 : x.Broj.Kolo <= kolo));

                string[] podaciKartoni = { k.Naziv, (brojZutih-brojDvaZuta).ToString(), brojDvaZuta.
                        ToString(), brojCrvenih.ToString(), (brojZutih+brojCrvenih)
                        .ToString() };
                data.Add(podaciKartoni);
            }
            data = data.OrderByDescending(o => int.Parse(o[4])).ThenByDescending(o => int.Parse(o[3]))
                .ThenByDescending(o => int.Parse(o[2])).ToList();
            data.Add(izracunajSumarne(data));
            return data;
        }

        private string[] izracunajSumarne(List<string[]> data)
        {
            string[] sumarni = { 
                "",
                data.Sum(o => int.Parse(o[1])).ToString(),
                data.Sum(o => int.Parse(o[2])).ToString(),
                data.Sum(o => int.Parse(o[3])).ToString(),
                data.Sum(o => int.Parse(o[4])).ToString(),
            };
            return sumarni;
        }

        public List<string> Zaglavlje()
        {
            return new List<string> { "Klub", "Žuti", "Dva Žuta", "Crveni", 
                "Ukupno" };
        }
    }
}
