using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CamelUp
{
    /// <summary>
    /// A card to bet on who will be first in the leg
    /// </summary>
    class LegBet
    {
        protected decimal _winValue;
        protected decimal _loseValue;
        private Color _colour;

        /// <summary>
        /// Creates a legbet card
        /// </summary>
        /// <param name="colour">Colour of legbet card</param>
        public LegBet(Color colour, decimal winValue)
        {
            _colour = colour;
            _winValue = winValue;
            _loseValue = 1m;
        }

        /// <summary>
        /// Gets colour of legbet card
        /// </summary>
        public Color Colour
        {
            get { return _colour; }
        }

        /// <summary>
        /// Gets the win value of the card
        /// </summary>
        public decimal WinValue
        {
            get { return _winValue; }
        }

        /// <summary>
        /// Gets the lose value of the card
        /// </summary>
        public decimal LoseValue
        {
            get { return _loseValue; }
        }

        /// <summary>
        /// Puts the leg bet into a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "-Leg Bet " + Colour + " " + WinValue;
        }
    }
}
