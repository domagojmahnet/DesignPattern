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
    class LoadPlayers : ILoad
    {
        public void Ucitaj(string path)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path, Encoding.Default);
                foreach (string line in lines.Skip(1))
                {
                    string[] columns = line.Split(';');
                    if (Validator.ValidirajIgrace(columns, Constants.StupciIgraca))
                    {
                        Player igrač = new Player
                        (
                            Storage.listaKlubova.Find(x => x.Klub == columns[0]),
                            columns[1],
                            columns[2].Split(',').ToList(),
                            DateTime.Parse(columns[3])
                        );
                        Storage.listaIgrača.Add(igrač);
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
                Console.WriteLine("Neispravan naziv datoteke igrača!");
            }
        }
    }
}
