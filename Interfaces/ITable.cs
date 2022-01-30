using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Interfaces
{
    interface ITable
    {
        List<string> Header { get; set; }
        List<string[]> Data { get; set; }
        List<string> Zaglavlje();
        List<string[]> Podaci(int? kolo = null, string klub = null);
    }
}
