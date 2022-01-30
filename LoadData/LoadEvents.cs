using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.LoadData.ChainOfResponsibility;
using dmahnet_zadaca_3.Logic;
using dmahnet_zadaca_3.State;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace dmahnet_zadaca_3.LoadData
{
    class LoadEvents : ILoad
    {
        public void Ucitaj(string path)
        {
            Handler h1 = new SimpleEventHandler();
            Handler h2 = new IntermediateEventHandler();
            Handler h3 = new AdvancedEventHandler();
            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path, Encoding.Default);
                foreach (string line in lines.Skip(1))
                {
                    string[] columns = line.Split(';');
                    if (Validator.ValidirajDodagaj(columns))
                    {
                        var type = (EventType)Enum.Parse(typeof(EventType), columns[2]);
                        Event dogadaj = new Event();
                        dogadaj = h1.HandleEvent(columns, type);
                        if (!Constants.DogadajTriStupca.Contains(dogadaj.Vrsta))
                        {
                            if (!ChangeState(dogadaj, columns))
                            {
                                continue;
                            }
                        }
                        Storage.listaDogadaja.Add(dogadaj);
                    }
                    else
                    {
                        Console.WriteLine(line);
                        Console.WriteLine();
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Neispravan naziv datoteke događaja!");
            }
        }

        private static bool ChangeState(Event e, string[] columns)
        {
            Game g = Storage.listaUtakmica
                    .Find(x => x.Broj == int.Parse(columns[0]));
            Team t = Storage.listaSastava.Find(x => x.Broj == g && x.Igrač.Ime == columns[4]);

            if (t == null) return false;
            Player p = t.Igrač;
            if (e.Vrsta != EventType.Zamjena)
            {
                if (p.Validity(e))
                {
                    p.Update(e);
                    return true;
                }
            }
            else
            {
                Team t2 = Storage.listaSastava.Find(x => x.Broj == g 
                    && x.Igrač.Ime == columns[5]);
                if (t2 == null) return false;
                Player p2 = t2.Igrač;
                if (p.Validity(e) && p2.Validity(e) && p.playerState != SubstituteState.Instance() 
                    && p2.playerState != PlayingState.Instance())
                {
                    p.Update(e);
                    p2.Update(e);
                    return true;
                }
            }
            return false;
        }
    }
}
