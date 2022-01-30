using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Core
{
    public class Game
    {
        public int Broj { get; set; }
        public int Kolo { get; set; }
        public Club Domaćin { get; set; }
        public Club Gost { get; set; }
        public DateTime Početak { get; set; }

        public Game(int broj, int kolo, Club domaćin, Club gost, DateTime početak)
        {
            Broj = broj;
            Kolo = kolo;
            Domaćin = domaćin;
            Gost = gost;
            Početak = početak;
        }

        public Game(int kolo, Club domaćin, Club gost)
        {
            Kolo = kolo;
            Domaćin = domaćin;
            Gost = gost;
        }
    }
}
