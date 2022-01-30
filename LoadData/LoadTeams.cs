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
    class LoadTeams : ILoad
    {
        public void Ucitaj(string path)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path, Encoding.Default);
                foreach (string line in lines.Skip(1))
                {
                    string[] columns = line.Split(';');
                    if (Validator.ValidirajSastave(columns, Constants.StupciSastava))
                    {
                        var player = (Player)Storage.listaIgrača.Find(x => x.Ime == columns[3]).Clone();
                        Team sastav = new Team(
                            Storage.listaUtakmica.Find(x => x.Broj == int.Parse(columns[0])),
                            Storage.listaKlubova.Find(x => x.Klub == columns[1]),
                            columns[2],
                            player,
                            columns[4]
                        );

                        Storage.listaSastava.Add(sastav);
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
                Console.WriteLine("Neispravan naziv datoteke sastava!");
            }
        }
    }
}
