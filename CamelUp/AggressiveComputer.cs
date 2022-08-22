using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamelUp
{
    /// <summary>
    /// Aggressive computer which bets high and takes chances on lower camels
    /// </summary>
    class AggressiveComputer : Computer
    {
        public AggressiveComputer()
        {
            _name = "Aggresive Computer";
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
                return "Leg Aggressive";
            }
            else if (random > 750 && random < 876)
            {
                return "Winner Aggressive";
            }
            else
            {
                return "Loser Aggressive";
            }
        }
    }
}
