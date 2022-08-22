using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamelUp
{
    /// <summary>
    /// Cautious computer which bets low and always bets on leading camels
    /// </summary>
    class CautiousComputer : Computer
    {
        public CautiousComputer()
        {
            _name = "Cautious Computer";
        }

        public override string Choice(int random)
        {
            if (random > 0 && random < 251)
            {
                return "Move";
            }
            else if (random > 250 && random < 376)
            {
                return "Mirage";
            }
            else if (random > 375 && random < 501)
            {
                return "Oasis";
            }
            else if (random > 500 && random < 751)
            {
                return "Leg Cautious";
            }
            else if (random > 750 && random < 876)
            {
                return "Winner Cautious";
            }
            else
            {
                return "Loser Cautious";
            }
        }
    }
}
