using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dmahnet_zadaca_3.Logic
{
    public class Validator
    {
        public static bool Validate(string[] columns, Dictionary<int, string> stupci) 
        {
            if (columns.Where(x => x == "").Count() != 0)
            {
                Console.Write("Prazni podaci u stupcima: ");
                int colIndex = 0;
                foreach (string col in columns)
                {
                    if (col == "") Console.Write(stupci[colIndex]+" ");
                    colIndex++;
                }
                Console.WriteLine();
                return false;
            }
            return true;
        }

        public static bool ValidirajIgrace(string[] columns, Dictionary<int, string> stupci)
        {
            if (!Validate(columns, stupci)) return false;
            bool error = false;
            string[] pozicije = columns[2].Split(',');
            if (Storage.listaKlubova.Find(x => x.Klub == columns[0]) == null) {
                Console.WriteLine("Ne postoji traženi klub "+columns[0]+"!");
                error = true;
            }
            if (pozicije.Length > 5) {
                Console.WriteLine("Igrač ima više od 5 pozicija! ");
                error = true;
            } 
            foreach(string p in pozicije)
            {
                if(!Constants.OpcePozicije.Contains(p) && !Constants.SpecijaliziranePozicije.Contains(p))
                {
                    Console.WriteLine("Ne postoji pozicija "+p+"!");
                    error = true;
                }
            }
            try
            {
                DateTime.Parse(columns[3]);
            }
            catch
            {
                Console.WriteLine("Neispravan format datuma rođenja!");
                error = true;
            }
            if (error) return false;
            return true;
        }

        public static bool ValidirajUtakmice(string[] columns, Dictionary<int, string> stupci)
        {
            if (!Validate(columns, stupci)) return false;
            bool error = false;
            try
            {
                int.Parse(columns[0]);
            }
            catch
            {
                Console.WriteLine("Neispravan format stupca Broj!");
                error = true;
            }
            try
            {
                int.Parse(columns[1]);
            }
            catch
            {
                Console.WriteLine("Neispravan format stupca Kolo!");
                error = true;
            }
            if (Storage.listaKlubova.Find(x => x.Klub == columns[2]) == null)
            {
                Console.WriteLine("Ne postoji traženi klub " + columns[2] + "!");
                error = true;
            }
            if (Storage.listaKlubova.Find(x => x.Klub == columns[3]) == null)
            {
                Console.WriteLine("Ne postoji traženi klub " + columns[3] + "!");
                error = true;
            }
            try
            {
                DateTime.Parse(columns[4]);
            }
            catch
            {
                Console.WriteLine("Neispravan format početka!");
                error = true;
            }
            if (error) return false;
            return true;
        }

        public static bool ValidirajSastave(string[] columns, Dictionary<int, string> stupci)
        {
            if (!Validate(columns, stupci)) return false;
            bool error = false;
            try
            {
                int brojUtakmice = int.Parse(columns[0]);
                if (Storage.listaUtakmica.Find(u => u.Broj == brojUtakmice) == null)
                {
                    Console.WriteLine("Ne postoji utakmica s brojem " + brojUtakmice + "!");
                    error = true;
                }
            }
            catch
            {
                Console.WriteLine("Neispravan format stupca Broj!");
                error = true;
            }
            Club k = Storage.listaKlubova.Find(x => x.Klub == columns[1]);
            if (k == null)
            {
                Console.WriteLine("Ne postoji traženi klub " + columns[1] + "!");
                error = true;
            }
            if (columns[2] != Constants.Pričuva && columns[2] != Constants.Sastav)
            {
                Console.WriteLine("Ne postoji vrsta " + columns[2] + "!");
                error = true;
            }
            Player p = Storage.listaIgrača.Find(x => x.Ime == columns[3]);
            if (p == null || p.Klub.Klub != k.Klub)
            {
                Console.WriteLine("Ne postoji traženi igrač " + columns[3] 
                    + " u klubu " + columns[1] + "!");
                error = true;
            }
            else if (!p.Pozicije.Contains(columns[4]))
            {
                Console.WriteLine("Igrač nema pridruženu poziciju " + columns[4] + "!");
                error = true;
            }
            if (error) return false;
            return true;
        }

        public static bool ValidirajDodagaj(string[] columns)
        {
            bool error = false;
            try
            {
                int brojUtakmice = int.Parse(columns[0]);
                if(Storage.listaUtakmica.Find(u => u.Broj == brojUtakmice) == null)
                {
                    Console.WriteLine("Ne postoji utakmica s brojem "+ brojUtakmice + "!");
                    error = true;
                }
            }
            catch
            {
                Console.WriteLine("Neispravan format stupca Broj!");
                error = true;
            }
            try
            {
                EventType type = (EventType)Enum.Parse(typeof(EventType), columns[2]);
                if (!Constants.DogadajTriStupca.Contains(type))
                {
                    Club k = Storage.listaKlubova.Find(x => x.Klub == columns[3]);
                    if (k == null)
                    {
                        Console.WriteLine("Ne postoji traženi klub " + columns[3] + "!");
                        error = true;
                    }
                    Player p = Storage.listaIgrača.Find(x => x.Ime == columns[4]);
                    if (p == null || p.Klub.Klub != k.Klub)
                    {
                        Console.WriteLine("Ne postoji traženi igrač " + columns[4] + " u klubu " + 
                            columns[3] + "!");
                        error = true;
                    }
                    if (!Constants.DogadajPetStupca.Contains(type))
                    {
                        Player z = Storage.listaIgrača.Find(x => x.Ime == columns[5]);

                        if (z == null || z.Klub.Klub != k.Klub)
                        {
                            Console.WriteLine("Ne postoji traženi igrač " + columns[5] +" u klubu " 
                                + columns[3] + "!");
                            error = true;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Ne postoji tražena vrsta " + columns[2] + "!");
                error = true;
            }
            if (error) return false;
            return true;
        }
    }
}
