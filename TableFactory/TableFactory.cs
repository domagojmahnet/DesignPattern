using dmahnet_zadaca_3.Enums;
using dmahnet_zadaca_3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.TableFactory
{
    class TableFactory
    {
            public static ITable Build(TableType type, int? kolo = null, string klub = null)
            {
            switch (type)
            {
                case TableType.ScorerTable:
                    return new ScorerTable(kolo);
                case TableType.CardTable:
                    return new CardTable(kolo);
                case TableType.ResultTable:
                    return new ResultTable(kolo, klub);
                case TableType.StandingsTable:
                    return new StandingsTable(kolo);
                default:
                    return null;
            }
        }
    }
}
