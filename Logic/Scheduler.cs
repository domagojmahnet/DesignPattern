using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Logic
{
    class Scheduler
    {
        public static void GenerirajRaspored(AlgorithmType algorithm)
        {
            List<Game> generiraneUtakmice = new List<Game>();
            switch (algorithm)
            {
                case AlgorithmType.RandomNumbers:
                    generiraneUtakmice = GenerirajRandomNumbers();
                    break;
                case AlgorithmType.AlphabetSort:
                    generiraneUtakmice = GenerirajAlphabetSort();
                    break;
                case AlgorithmType.NameLength:
                    generiraneUtakmice = GenerirajNameLength();
                    break;
                default:
                    break;
            }
            if(generiraneUtakmice != null)
            {
                Console.WriteLine("Generiran raspored pod rednim brojem: " +
                    (Storage.listaRasporeda.Count() + 1));
                Console.WriteLine();
                Storage.listaRasporeda.Add(new Schedule(Storage.listaRasporeda.Count() + 1, 
                    DateTime.Now, generiraneUtakmice, false));
            }
        }

        private static List<Game> GenerirajRandomNumbers()
        {
            List<Club> domacini = new List<Club>();
            List<Club> gosti = new List<Club>();
            List<Club> klubovi = Storage.listaKlubova;
            klubovi.RemoveAt(1);
            klubovi.RemoveAt(2);
            klubovi.RemoveAt(3);

            if (klubovi.Count() % 2 != 0)
            {
                klubovi.Add(new Club("SLOB", "Slobodno"));
            }
            var rng = new Random();
            var values = Enumerable.Range(0, klubovi.Count()).OrderBy(x => rng.Next()).ToArray();
            var half = klubovi.Count() / 2;

            for (int i = 0; i < half; i++)
            {
                domacini.Add(klubovi[values[i]]);
            }
            for (int i = half; i < klubovi.Count(); i++)
            {
                gosti.Add(klubovi[values[i]]);
            }

            return GenerirajUtakmice(domacini, gosti);
        }
        private static List<Game> GenerirajAlphabetSort()
        {
            List<Club> domacini = new List<Club>();
            List<Club> gosti = new List<Club>();
            List<Club> klubovi = Storage.listaKlubova;
            klubovi = klubovi.OrderBy(x => x.Naziv).ToList();
            var half = Math.Ceiling((decimal)klubovi.Count() / 2);

            for (int i = 0; i < half; i++)
            {
                domacini.Add(klubovi[i]);
            }
            for (int i = (int)half; i < klubovi.Count(); i++)
            {
                gosti.Add(klubovi[i]);
            }

            if (klubovi.Count() % 2 != 0)
            {
                gosti.Add(new Club("SLOB", "Slobodno"));
            }
            return GenerirajUtakmice(domacini, gosti);
        }
        private static List<Game> GenerirajNameLength()
        {
            List<Club> domacini = new List<Club>();
            List<Club> gosti = new List<Club>();
            List<Club> klubovi = Storage.listaKlubova;
            var half = Math.Ceiling((decimal)klubovi.Count() / 2);
            klubovi = klubovi.OrderBy(k => k.Naziv.Length).ThenByDescending(k => 
                Regex.Matches(k.Trener.Ime, "a|e|i|o|u", RegexOptions.IgnoreCase).Count).ToList();
            for (int i = 0; i < half; i++)
            {
                domacini.Add(klubovi[i]);
            }
            for (int i = (int)half; i < klubovi.Count(); i++)
            {
                gosti.Add(klubovi[i]);
            }

            if (klubovi.Count() % 2 != 0)
            {
                gosti.Add(new Club("SLOB", "Slobodno"));
            }
            return GenerirajUtakmice(domacini, gosti);
        }

        private static List<Game> GenerirajUtakmice(List<Club> domacini, List<Club> gosti)
        {
            List<Game> generiraneUtakmice = new List<Game>();
            bool praviGosti = true;
            var processDomacini = domacini;
            var processGosti = gosti;
            int kolo = 1;
            for (int y = 1; y <= (Storage.listaKlubova.Count() < 10 ? 4 : 2); y++)
            {
                for (int i = 0; i < domacini.Count(); i++)
                {
                    for (int x = 0; x < domacini.Count(); x++)
                    {
                        generiraneUtakmice.Add(new Game(kolo, processDomacini[x], processGosti[x]));
                    }

                    if (praviGosti)
                    {
                        var zadnji = processGosti.Last();
                        processGosti.Remove(zadnji);
                        processGosti.Reverse();
                        processGosti.Add(zadnji);
                        processGosti.Reverse();
                    }
                    else
                    {
                        var zadnji = processDomacini.Last();
                        processDomacini.Remove(zadnji);
                        processDomacini.Reverse();
                        processDomacini.Add(zadnji);
                        processDomacini.Reverse();
                    }
                    var oldGosti = processGosti;
                    processGosti = processDomacini;
                    processDomacini = oldGosti;
                    praviGosti = !praviGosti;
                    kolo++;
                }

                processDomacini = y % 2 != 0 ? gosti : domacini;
                processGosti = y % 2 != 0 ? domacini : gosti;
            }
            return generiraneUtakmice;
        }

        public static void IspisSvihRasporeda()
        {
            Console.WriteLine();
            Console.WriteLine($"{"Redni broj",10} {"Vrijeme kreiranja",-20}");
            Console.WriteLine();
            foreach (Schedule s in Storage.listaRasporeda)
            {
                Console.WriteLine($"{s.RedniBroj,10} {s.VrijemeKreiranja,-20}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void PostaviVazecRaspored(int broj)
        {
            var schedule = Storage.listaRasporeda.Find(s => s.RedniBroj == broj);
            if (schedule != null)
            {
                foreach (Schedule s in Storage.listaRasporeda)
                {
                    s.VazeciRaspored = false;
                }
                schedule.VazeciRaspored = true;
                Console.WriteLine("Raspored pod rednim brojem " + schedule.RedniBroj +
                    " postavljen kao važeći!");
                Console.WriteLine();
            }
        }

        public static void IspisUtakmicaKlub(string klub)
        {
            if(Storage.listaRasporeda.FindAll(s => s.VazeciRaspored == true).Count() == 0)
            {
                Console.WriteLine("Ne postoji važeći raspored!");
                return;
            }

            Console.WriteLine($"{"Kolo",10} {"Domaćin",-30} {"Gost", -50}");
            Console.WriteLine();
            foreach (Game g in Storage.listaRasporeda.Find(s => s.VazeciRaspored == true)
                .Utakmice.Where(game => game.Domaćin.Klub == klub || game.Gost.Klub == klub))
            {
                Console.WriteLine($"{g.Kolo,10} {g.Domaćin.Naziv,-30} {g.Gost.Naziv,-50}");
            }
            Console.WriteLine();
        }

        public static void IspisUtakmicaKolo(int kolo)
        {
            if (Storage.listaRasporeda.FindAll(s => s.VazeciRaspored == true).Count() == 0)
            {
                Console.WriteLine("Ne postoji važeći raspored!");
                return;
            }

            Console.WriteLine($"{"Kolo",10} {"Domaćin",-30} {"Gost",-50}");
            Console.WriteLine();
            foreach (Game g in Storage.listaRasporeda.Find(s => s.VazeciRaspored == true)
                .Utakmice.Where(game => game.Kolo == kolo))
            {
                Console.WriteLine($"{g.Kolo,10} {g.Domaćin.Naziv,-30} {g.Gost.Naziv,-50}");
            }
            Console.WriteLine();
        }
    }
}
