using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Decorator;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Observer
{
    public class EventObserver : IObserver
    {
        private string Domacin;
        private string Gost;
        private List<string> GoloviDomacin = new List<string>();
        private List<string> GoloviGost = new List<string>();

        public EventObserver()
        {

        }

        public EventObserver(ISubject subject)
        {
            subject.Register(this);
        }

        public void Update(Event e)
        {
            Domacin = e.Broj.Domaćin.Naziv;
            Gost = e.Broj.Gost.Naziv;

            if(e.Vrsta == EventType.Gol || e.Vrsta == EventType.KazneniUdarac)
            {
                var eoDecorator = new EventObserverDecorator(this);

                if(e.Igrač.Klub.Naziv == Domacin)
                {
                    GoloviDomacin.Add(eoDecorator.GolString(e));
                }
                else
                {
                    GoloviGost.Add(eoDecorator.GolString(e));
                }
            }

            if (e.Vrsta == EventType.Autogol)
            {
                if (e.Igrač.Klub.Naziv == Domacin)
                {
                    string gol = e.Igrač.Ime.ToString()+" (og) "+ e.Min.ToString();
                    GoloviGost.Add(gol);
                }
                else
                {
                    string gol = e.Igrač.Ime.ToString() + " (og) "+ e.Min.ToString();
                    GoloviDomacin.Add(gol);
                }
            }
            printBaseSemafor(e);
        }

        private void printBaseSemafor(Event e)
        {
            Console.WriteLine($"{e.Min}");
            Console.WriteLine($"{Domacin,30}{Gost,60}");
            int goloviDomacin = GoloviDomacin.Count();
            int goloviGost = GoloviGost.Count();
            int max = Math.Max(goloviDomacin, goloviGost);
            Console.WriteLine($"{goloviDomacin,30}{goloviGost,60}");
            for (int i = 0; i < max; i++)
            {
                if (i < goloviDomacin)
                {
                    Console.Write($"{GoloviDomacin[i],30}");
                }
                else
                {
                    Console.Write($"{"",30}");
                }
                if (i < goloviGost)
                {
                    Console.Write($"{GoloviGost[i],60}");
                }
                else 
                {
                    Console.Write($"{"",60}");
                }
                Console.WriteLine();
            }
            printOstalo(e);
            Console.WriteLine();
        }

        private void printOstalo(Event e)
        {
            if(e.Vrsta == EventType.Pocetak){
                Console.WriteLine("Pocetak utakmice!");
            }
            if(e.Vrsta == EventType.Kraj)
            {
                Console.WriteLine("Kraj utakmice!");
            }
            if (e.Vrsta == EventType.ZutiKarton)
            {
                if(e.Klub.Naziv == Domacin) Console.WriteLine($"{"Zuti karton: " + e.Igrač.Ime,30}{"",60}");
                else Console.WriteLine($"{"",30}{"Zuti karton: " + e.Igrač.Ime,60}");
            }
            if (e.Vrsta == EventType.CrveniKarton)
            {
                if (e.Klub.Naziv == Domacin) Console.WriteLine($"{"Crveni karton: " + e.Igrač.Ime,30}{"",60}");
                else Console.WriteLine($"{"",30}{"Crveni karton: " + e.Igrač.Ime,60}");
            }
            if (e.Vrsta == EventType.Zamjena)
            {
                if (e.Klub.Naziv == Domacin) Console.WriteLine($"{"Zamjena: " + e.Igrač.Ime+" "+e.Zamjena.Ime,30}{"",60}");
                else Console.WriteLine($"{"",30}{"Zamjena: " + e.Igrač.Ime+" "+e.Zamjena.Ime,60}");
            }
        }

        public virtual string GolString(Event e)
        {
            return e.Igrač.Ime.ToString() + " " + e.Min.ToString();
        }
    }
}
