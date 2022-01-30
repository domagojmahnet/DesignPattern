using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.LoadData
{
    class LoadGames : ILoad
    {
        public void Ucitaj(string path)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path, Encoding.Default);
                foreach (string line in lines.Skip(1))
                {
                    string[] columns = line.Split(';');
                    if (Validator.ValidirajUtakmice(columns, Constants.StupciUtakmica))
                    {
                        Game utakmica = new Game
                        (
                            int.Parse(columns[0]),
                            int.Parse(columns[1]),
                            Storage.listaKlubova.Find(x => x.Klub == columns[2]),
                            Storage.listaKlubova.Find(x => x.Klub == columns[3]),
                            DateTime.Parse(columns[4])
                        );
                        Storage.listaUtakmica.Add(utakmica);
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
                Console.WriteLine("Neispravan naziv datoteke utakmica!");
            }
        }
    }
}
