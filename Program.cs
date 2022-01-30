using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using dmahnet_zadaca_3.Logic;
using dmahnet_zadaca_3.Observer;
using dmahnet_zadaca_3.State;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace dmahnet_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            UcitajDatoteke(args);
            while (1 == 1)
            {
                Console.WriteLine("Unesite komandu!");
                string[] komanda = Console.ReadLine().Split(' ');
                UcitajKomandu(komanda);
            }
        }
        static void UcitajDatoteke(string[] argumenti)
        {
            try
            {
                int? kluboviIndex = Array.FindIndex(argumenti, x => x == "-k");
                if (kluboviIndex != null)
                {
                    LoadDataFacade.Instance.UcitajKlubove(argumenti[(int)kluboviIndex + 1]);
                }
            }
            catch{ }
            try
            {
                int? igraciIndex = Array.FindIndex(argumenti, x => x == "-i");
                if (igraciIndex != null)
                {
                    LoadDataFacade.Instance.UcitajIgrače(argumenti[(int)igraciIndex + 1]);
                }
            }
            catch { }
            try
            {
                int? utakmiceIndex = Array.FindIndex(argumenti, x => x == "-u");
                if (utakmiceIndex != null)
                {
                    LoadDataFacade.Instance.UcitajUtakmice(argumenti[(int)utakmiceIndex + 1]);
                }
            }
            catch { }
            try
            {
                int? sastaviIndex = Array.FindIndex(argumenti, x => x == "-s");
                if (sastaviIndex != null)
                {
                    LoadDataFacade.Instance.UcitajSastave(argumenti[(int)sastaviIndex + 1]);
                }
            }
            catch { }
            try
            {
                int? dogadajiIndex = Array.FindIndex(argumenti, x => x == "-d");
                if (dogadajiIndex != null)
                {
                    LoadDataFacade.Instance.UcitajDogadaje(argumenti[(int)dogadajiIndex + 1]);
                }
            }
            catch { }
        }

        static void UcitajKomandu(string[] komanda)
        {

            try { if (komanda[0] == "SU") IspisiSastave(komanda); }
            catch { }
            IspisiTablice(komanda);
            ObradiRaspored(komanda);
            if (Constants.NoviPodaciKomanda.Contains(komanda[0]))
            {
                DodajPodatke(komanda);
            }
        }

        static void IspisiTablice(string[] komanda)
        {
            int? kolo = null;
            switch (komanda[0])
            {
                case "S":
                    try { kolo = int.Parse(komanda[1]); }
                    catch { }
                    ispisiTablicu(TableFactory.TableFactory.Build(TableType.ScorerTable, kolo));
                    break;
                case "T":
                    try { kolo = int.Parse(komanda[1]); }
                    catch { }
                    ispisiTablicu(TableFactory.TableFactory.Build(TableType.StandingsTable, kolo));
                    break;
                case "K":
                    try { kolo = int.Parse(komanda[1]); }
                    catch { }
                    ispisiTablicu(TableFactory.TableFactory.Build(TableType.CardTable, kolo));
                    break;
                case "R":
                    try { if (komanda[2] != null) kolo = int.Parse(komanda[2]); }
                    catch { }
                    try
                    {
                        ispisiTablicu(TableFactory.TableFactory.Build(TableType.ResultTable,
                            kolo, komanda[1].ToString()));
                        break;
                    }
                    catch { break; }
                case "D":
                    SendEvent(komanda);
                    break;
                default:
                    break;
            }
        }

        static void ObradiRaspored(string[] komanda)
        {
            switch (komanda[0])
            {
                case "GR":
                    try { Scheduler.GenerirajRaspored((AlgorithmType)Enum.Parse(typeof(AlgorithmType), komanda[1])); }
                    catch { }
                    break;
                case "IG":
                    Scheduler.IspisSvihRasporeda();
                    break;
                case "VR":
                    try { Scheduler.PostaviVazecRaspored(int.Parse(komanda[1])); }
                    catch { }
                    break;
                case "IR":
                    try { Scheduler.IspisUtakmicaKlub(komanda[1]); }
                    catch{ }
                    break;
                case "IK":
                    try { Scheduler.IspisUtakmicaKolo(int.Parse(komanda[1])); }
                    catch { }
                    break;
                default:
                    break;
            }
        }

        static void DodajPodatke(string[] komanda)
        {
            switch (komanda[0])
            {
                case "NU":
                    if(!File.Exists(komanda[1]))
                    {
                        Console.WriteLine("Ne postoji datoteka!");
                        return;
                    }
                    LoadDataFacade.Instance.UcitajUtakmice(komanda[1]);
                    Console.WriteLine("Učitani podaci dodatne datoteke!");
                    break;
                case "NS":
                    if (!File.Exists(komanda[1]))
                    {
                        Console.WriteLine("Ne postoji datoteka!");
                        return;
                    }
                    LoadDataFacade.Instance.UcitajSastave(komanda[1]);
                    Console.WriteLine("Učitani podaci dodatne datoteke!");
                    break;
                case "ND":
                    if (!File.Exists(komanda[1]))
                    {
                        Console.WriteLine("Ne postoji datoteka!");
                        return;
                    }
                    LoadDataFacade.Instance.UcitajDogadaje(komanda[1]);
                    Console.WriteLine("Učitani podaci dodatne datoteke!");
                    break;
            }
        }

        static void ispisiTablicu(ITable table)
        {
            const int separatorTekst = -25;
            const int separatorBroj = 10;
            int i = 0;
            foreach (string s in table.Header)
            {
                
                if (i < 2) 
                {
                    Console.Write($"{s,separatorTekst}");
                }
                else
                {
                    Console.Write($"{s,separatorBroj}");
                }
                i++; 
            }
            Console.WriteLine();
            Console.WriteLine();
            foreach (string[] data in table.Data)
            {
                i = 0;
                foreach(string s in data)
                {
                    if (i < 2)
                    {
                        Console.Write($"{s,separatorTekst}");
                    }
                    else
                    {
                        Console.Write($"{s,separatorBroj}");
                    }
                    i++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void SendEvent(string[] komanda)
        {
            try { int.Parse(komanda[1]); }
            catch {
                Console.WriteLine("Neispravan format broja kola");
                return; 
            }
            try { int.Parse(komanda[4]); }
            catch
            {
                Console.WriteLine("Neispravan format broja sekunda");
                return;
            }
            Semafor semafor = new Semafor();
            EventObserver eventObserver = new EventObserver(semafor);

            Game g = Storage.listaUtakmica.Find(x => x.Kolo == int.Parse(komanda[1])
                && ((x.Domaćin.Klub == komanda[2] && x.Gost.Klub == komanda[3])
                || (x.Domaćin.Klub == komanda[3] && x.Gost.Klub == komanda[2])));

            foreach (Event e in Storage.listaDogadaja.FindAll(d => d.Broj == g))
            {
                semafor.SetEvent(e);
                Thread.Sleep(int.Parse(komanda[4]) * 1000);
            }
        }

        static void IspisiSastave(string[] komanda)
        {
            if(Storage.listaUtakmica.Find(x => x.Kolo == int.Parse(komanda[1]) && x.Domaćin.Klub 
            == komanda[2] && x.Gost.Klub == komanda[3]) == null)
            {
                Console.WriteLine("Nepostojeća utakmica!");
                Console.WriteLine();
                return;
            }
            var teamOrder = new[] { "G", "B", "LB", "DB", "CB", "V", "LDV", "DDV", "CDV", "LV", "DV"
                , "CV", "LOV", "DOV", "COV", "N", "LN","DN","CN"};
            var domaciSastav = Storage.listaSastava.FindAll(x => x.Klub.Klub == komanda[2] &&
                x.Broj.Kolo == int.Parse(komanda[1]) && x.Vrsta =="S")
                .OrderBy(p => Array.IndexOf(teamOrder, p.Pozicija)).ToList();
            var gostujuciSastav = Storage.listaSastava.FindAll(x => x.Klub.Klub == komanda[3] && 
                x.Broj.Kolo == int.Parse(komanda[1]) && x.Vrsta == "S")
                .OrderBy(p=> Array.IndexOf(teamOrder, p.Pozicija)).ToList();

            if (domaciSastav.Count == 0 || gostujuciSastav.Count == 0)
            {
                Console.WriteLine("Nepostojeći sastavi!");
                Console.WriteLine();
                return;
            }
            int max = Math.Max(domaciSastav.Count, gostujuciSastav.Count);
            Console.WriteLine();
            Console.WriteLine($"{"Domaci",-50} {"Gosti",-50}");
            Console.WriteLine();
            for (int i = 0; i<max; i++)
            {
                Console.WriteLine($"{domaciSastav[i].Igrač.Ime,-50} " +
                    $"{gostujuciSastav[i].Igrač.Ime,-50}");
            }
            IspisiSastaveKraj(komanda, teamOrder);
        }

        static void IspisiSastaveKraj(string[] komanda, string [] teamOrder)
        {
            Console.WriteLine();

            var domaciSastavKraj = Storage.listaSastava.FindAll(x => x.Klub.Klub == komanda[2] &&
                x.Broj.Kolo == int.Parse(komanda[1]) && x.Igrač.playerState == PlayingState.Instance())
                .OrderBy(p => Array.IndexOf(teamOrder, p.Pozicija)).ToList();
            var gostujuciSastavKraj = Storage.listaSastava.FindAll(x => x.Klub.Klub == komanda[3] &&
                x.Broj.Kolo == int.Parse(komanda[1]) && x.Igrač.playerState == PlayingState.Instance())
                .OrderBy(p => Array.IndexOf(teamOrder, p.Pozicija)).ToList();

            Console.WriteLine($"{"Domaci",-50} {"Gosti",-50}");
            Console.WriteLine();
            for (int i = 0; i < 11; i++)
            {
                if (i < domaciSastavKraj.Count())
                {
                    Console.Write($"{domaciSastavKraj[i].Igrač.Ime,-50}");
                }
                else
                {
                    Console.Write($"{"",-50}");
                }
                if (i < gostujuciSastavKraj.Count())
                {
                    Console.Write($"{gostujuciSastavKraj[i].Igrač.Ime,-50}");
                }
                else
                {
                    Console.Write($"{"",-50}");
                }
                Console.WriteLine();
            }
        }
    }
}
