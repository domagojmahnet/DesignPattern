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
    class LoadClubs : ILoad
    {
        public void Ucitaj(string path)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path, Encoding.Default);
                foreach (string line in lines.Skip(1))
                {
                    string[] columns = line.Split(';');
                    if (Validator.Validate(columns, Constants.StupciKlubova))
                    {
                        Club klub = new Club(columns[0], columns[1], new Coach(columns[2]));
                        Storage.listaKlubova.Add(klub);
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
                Console.WriteLine("Neispravan naziv datoteke klubova!");
            }
        }
    }
}
