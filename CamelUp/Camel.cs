using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CamelUp
{
    /// <summary>
    /// The camels that race to the finish line
    /// </summary>
    class Camel
    {
        private Color _colour;
        private double _x;
        private double _y;
        private double _camelHeight;
        private double _camelWidth;
        public Dice camelDice;

        /// <summary>
        /// Creates a new camel object
        /// </summary>
        /// <param name="colour">Colour of the camel</param>
        /// <param name="x">X position of camel</param>
        /// <param name="y">Y position of the camel</param>
        public Camel(Color colour, int x, int y)
        {
            _colour = colour;
            _x = x;
            _y = y;
            _camelHeight = 50;
            _camelWidth = 65;
            camelDice = new Dice(_colour);
        }

        /// <summary>
        /// Gets and sets the colour of the camel
        /// </summary>
        public Color Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        /// <summary>
        /// Gets and sets the X position of the camel
        /// </summary>
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Gets and sets the Y position of the camel
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Gets and sets the height of the camel image
        /// </summary>
        public double CamelHeight
        {
            get { return _camelHeight; }
            set { _camelHeight = value; }
        }

        /// <summary>
        /// Gets and sets the width of the camel image
        /// </summary>
        public double CamelWidth
        {
            get { return _camelWidth; }
            set { _camelWidth = value; }
        }

        /// <summary>
        /// Draws the camel
        /// </summary>
        /// <param name="paper">Where to draw the camel</param>
        public void Draw(Graphics paper)
        {
            Bitmap bmp = new Bitmap(CamelUp.Properties.Resources.Blue, (int)CamelWidth, (int)CamelHeight); ;

            if (_colour == Color.Yellow)
            {
                bmp = new Bitmap(CamelUp.Properties.Resources.Yellow, (int)CamelWidth, (int)CamelHeight);
            }
            else if (_colour == Color.Green)
            {
                bmp = new Bitmap(CamelUp.Properties.Resources.Green, (int)CamelWidth, (int)CamelHeight);
            }
            else if (_colour == Color.Orange)
            {
                bmp = new Bitmap(CamelUp.Properties.Resources.Orange, (int)CamelWidth, (int)CamelHeight);
            }
            else if (_colour == Color.White)
            {
                bmp = new Bitmap(CamelUp.Properties.Resources.White, (int)CamelWidth, (int)CamelHeight);
            }

            paper.DrawImage(bmp, (int)X, (int)Y);
        }
    }
}
