using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamelUp
{
    /// <summary>
    /// Computer in which the user plays against
    /// Chooses random version of computer to use turn
    /// </summary>
    public abstract class Computer
    {
        protected string _name;

        /// <summary>
        /// Creates a new computer
        /// </summary>
        public Computer()
        {
            _name = "Computer";
        }

        /// <summary>
        /// Gets the name of the computer
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Using probabilties, chooses what move the computer does
        /// </summary>
        /// <param name="random">Random number</param>
        /// <returns>Choice that computer makes</returns>
        public abstract string Choice(int random);
    }
}
