using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CamelUp
{
    class Dice
    {
        public List<int> diceList;
        private double _height;
        private double _width;
        private Color _colour;
        private Bitmap bmp;

        /// <summary>
        /// Creates three new dices 1, 2 and 3
        /// </summary>
        /// <param name="colour">Colour of dice</param>
        /// <param name="height">Height of image</param>
        /// <param name="width">Width of image</param>
        public Dice(Color colour)
        {
            diceList = new List<int>();
            diceList.Add(1);
            diceList.Add(2);
            diceList.Add(3);
            _colour = colour;
            _height = 50;
            _width = 50;
        }

        /// <summary>
        /// Get the image for the dice
        /// </summary>
        public Bitmap Bmp
        {
            get { return bmp; }
        }

        /// <summary>
        /// Gets the colour of the dice
        /// </summary>
        public Color Colour
        {
            get { return _colour; }
        }

        /// <summary>
        /// Get the height of the dice image
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Get the width of the dice image
        /// </summary>
        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Rolls the dice
        /// </summary>
        /// <param name="roll">Dice in list</param>
        /// <returns>Dice roll</returns>
        public int Roll(int roll)
        {
            if (Colour == Color.Blue)
            {
                if (diceList[roll] == 1)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.BlueDie1, (int)Width, (int)Height);
                }
                else if (diceList[roll] == 2)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.BlueDie2, (int)Width, (int)Height);
                }
                else
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.BlueDie3, (int)Width, (int)Height);
                }
            }
            else if (Colour == Color.Yellow)
            {
                if (diceList[roll] == 1)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.YellowDie1, (int)Width, (int)Height);
                }
                else if (diceList[roll] == 2)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.YellowDie2, (int)Width, (int)Height);
                }
                else
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.YellowDie3, (int)Width, (int)Height);
                }
            }
            else if (Colour == Color.Green)
            {
                if (diceList[roll] == 1)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.GreenDie1, (int)Width, (int)Height);
                }
                else if (diceList[roll] == 2)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.GreenDie2, (int)Width, (int)Height);
                }
                else
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.GreenDie3, (int)Width, (int)Height);
                }
            }
            else if (Colour == Color.Orange)
            {
                if (diceList[roll] == 1)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.OrangeDie1, (int)Width, (int)Height);
                }
                else if (diceList[roll] == 2)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.OrangeDie2, (int)Width, (int)Height);
                }
                else
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.OrangeDie3, (int)Width, (int)Height);
                }
            }
            else if (Colour == Color.White)
            {
                if (diceList[roll] == 1)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.WhiteDie1, (int)Width, (int)Height);
                }
                else if (diceList[roll] == 2)
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.WhiteDie2, (int)Width, (int)Height);
                }
                else
                {
                    bmp = new Bitmap(CamelUp.Properties.Resources.WhiteDie3, (int)Width, (int)Height);
                }
            }
            return diceList[roll];
        }
    }
}
