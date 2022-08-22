using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamelUp
{
    /// <summary>
    /// Randomized computer which chooses randomly
    /// </summary>
    class RandomComputer : Computer
    {

        public RandomComputer()
        {
            _name = "Random Computer";
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
                return "Leg";
            }
            else if (random > 750 && random < 876)
            {
                return "Winner";
            }
            else
            {
                return "Loser";
            }
        }
    }
}
