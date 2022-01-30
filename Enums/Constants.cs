using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Enums
{
    public class Constants
    {
        public const string Sastav = "S";
        public const string Pričuva = "P";
        public static readonly Dictionary<int, string> StupciKlubova = new Dictionary<int, string>()
        {
            { 0, "Klub"},
            { 1, "Naziv"},
            { 2, "Trener"}
        };
        public static readonly Dictionary<int, string> StupciIgraca = new Dictionary<int, string>()
        {
            { 0, "Klub"},
            { 1, "Igrač"},
            { 2, "Pozicije"},
            { 3, "Rođen"}
        };

        public static readonly Dictionary<int, string> StupciUtakmica = new Dictionary<int, string>()
        {
            { 0, "Broj"},
            { 1, "Kolo"},
            { 2, "Domaćin"},
            { 3, "Gost"},
            { 4, "Početak"}
        };

        public static readonly Dictionary<int, string> StupciSastava = new Dictionary<int, string>()
        {
            { 0, "Broj"},
            { 1, "Klub"},
            { 2, "Vrsta"},
            { 3, "Igrač"},
            { 4, "Pozicija"}
        };

        public static readonly Dictionary<int, string> StupciDogadaja = new Dictionary<int, string>()
        {
            { 0, "Broj"},
            { 1, "Min"},
            { 2, "Vrsta"},
            { 3, "Klub"},
            { 4, "Igrač"},
            { 5, "Zamjena"}
        };

        public static readonly List<EventType> DogadajTriStupca = new List<EventType>
            { EventType.Pocetak, EventType.Kraj };

        public static readonly List<EventType> DogadajPetStupca = new List<EventType> 
            { EventType.Gol, EventType.KazneniUdarac, EventType.Autogol, EventType.ZutiKarton, 
            EventType.CrveniKarton};

        public static readonly List<string> OpcePozicije = new List<string> 
            { "G", "B", "V", "N" };

        public static readonly List<string> SpecijaliziranePozicije = new List<string> 
            { "LB", "DB","CB","LDV","DDV","CDV","LV","DV","CV","LOV","DOV","COV","LN","DN","CN" };

        public static readonly List<string> NoviPodaciKomanda = new List<string> { "NS", "NU", "ND" };

        public static readonly string SastaviRegex = @"^DZ_3_sastavi_utakmica_\d+\.csv$";
        public static readonly string UtakmicaRegex = @"^DZ_3_utakmice_\d+\.csv$";
        public static readonly string DogadajiRegex = @"^DZ_3_dogadaji_\d+\.csv$";
    }
}
