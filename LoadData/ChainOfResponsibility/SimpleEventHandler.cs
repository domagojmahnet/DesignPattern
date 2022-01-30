using dmahnet_zadaca_3.Core;
using dmahnet_zadaca_3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.LoadData.ChainOfResponsibility
{
    class SimpleEventHandler : Handler
    {
        public override Event HandleEvent(string[] columns, EventType type)
        {
            if (Constants.DogadajTriStupca.Contains(type)){
                return EventBuilder
                .Init(Storage.listaUtakmica
                    .Find(x => x.Broj == int.Parse(columns[0])), columns[1], type)
                .SetKlub(Storage.listaKlubova.Find(x => x.Klub == columns[3]))
                .SetIgrač(Storage.listaIgrača.Find(x => x.Ime == columns[4]))
                .Build();
            }
            else if (successor != null)
            {
                return successor.HandleEvent(columns, type);
            }
            return null;
        }
    }
}
