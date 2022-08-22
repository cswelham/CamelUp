using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace CamelUp
{
    //Name: Connor Welham
    //ID: 1508018

    public partial class Form1 : Form
    {
        //The money that the user and computer starts with
        decimal playerMoney = 3m;
        decimal computerMoney = 3m;

        //Dynamic sizes when form is resized
        int formHeight = 550;
        int formWidth = 1260;

        //Creates 5 camels
        Camel camelBlue = new Camel(Color.Blue, 0, 0);
        Camel camelYellow = new Camel(Color.Yellow, 0, 50);
        Camel camelGreen = new Camel(Color.Green, 0, 100);
        Camel camelOrange = new Camel(Color.Orange, 0, 150);
        Camel camelWhite = new Camel(Color.White, 0, 200);

        //Legbets played for player and computer
        List<LegBet> playerPlayed = new List<LegBet>();
        List<LegBet> computerPlayed = new List<LegBet>();

        //Legbets that can be played for player and computer
        List<LegBet> playerPossible = new List<LegBet>();
        List<LegBet> computerPossible = new List<LegBet>();

        //Creates a list to stores all camels in
        List<Camel> camelList = new List<Camel>();

        //Pyramid of Dice
        List<Dice> pyramid = new List<Dice>();

        //List of strings to output to listbox
        List<string> playerLegOutput = new List<string>();
        List<string> computerLegOutput = new List<string>();
        List<string> playerWLOutput = new List<string>();
        List<string> computerWLOutput = new List<string>();
        List<string> allWOutput = new List<string>();
        List<string> allLOutput = new List<string>();

        //List of die to output
        List<Dice> rolledDice = new List<Dice>();

        Random r = new Random();

        //Whos turn it is
        bool playerTurn = true;

        //Create a new AI and list of AI
        Computer c1 = new RandomComputer();
        Computer c2 = new CautiousComputer();
        Computer c3 = new AggressiveComputer();
        List<Computer> computerList = new List<Computer>();

        // User putting mirage or desert down
        bool playerMirage = false;
        bool playerOasis = false;
        int playerDesertX = 0;
        int playerDesertY = 0;
        string playerDesert = "";

        //Computer putting mirage or desert down
        int computerDesertX = 0;
        string computerDesert = "";

        //Computer list for WL cards
        List<Color> computerWins = new List<Color>();
        List<Color> computerLoss = new List<Color>();

        public Form1()
        {
            InitializeComponent();

            //Store all camels in list
            camelList.Add(camelBlue);
            camelList.Add(camelYellow);
            camelList.Add(camelGreen);
            camelList.Add(camelOrange);
            camelList.Add(camelWhite);

            //Add player cards and computer cards to listbox
            playerWLOutput.Add("Player Bets:");
            computerWLOutput.Add("Computer Bets:");

            //Adds new computers to list
            computerList.Add(c1);
            computerList.Add(c2);
            computerList.Add(c3);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Draw the race track
            SolidBrush br = new SolidBrush(Color.SandyBrown);
            SolidBrush brMirage = new SolidBrush(Color.Red);
            SolidBrush brOasis = new SolidBrush(Color.LightBlue);
            Pen pen1 = new Pen(Color.Black, 2);
            for (int i = 0; i < camelList[0].CamelWidth*16; i+= (int)camelList[0].CamelWidth)
            {
                e.Graphics.DrawRectangle(pen1, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                //If race track part is now a desert
                if (i < playerDesertX && playerDesertX < i+65)
                {
                    if (playerDesert == "Mirage")
                    {
                        e.Graphics.FillRectangle(brMirage, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                    }
                    else if (playerDesert == "Oasis")
                    {
                        e.Graphics.FillRectangle(brOasis, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(br, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                    }
                }
                else if (i < computerDesertX && computerDesertX < i + camelList[0].CamelWidth)
                {
                    if (computerDesert == "Mirage")
                    {
                        e.Graphics.FillRectangle(brMirage, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                    }
                    else if (computerDesert == "Oasis")
                    {
                        e.Graphics.FillRectangle(brOasis, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(br, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(br, i, 0, (int)camelList[0].CamelWidth, (int)camelList[0].CamelHeight * 5);
                }                
            }
        
            //Draw all camels at x and y
            foreach (Camel c in camelList)
            {
                c.Draw(e.Graphics);
            }

            //Write all strings into listbox
            listBoxOutput.Items.Clear();
            for (int i = 0; i < playerWLOutput.Count; i++)
            {
                listBoxOutput.Items.Add(playerWLOutput[i]);
            }
            for (int i = 0; i < playerLegOutput.Count; i++)
            {
                listBoxOutput.Items.Add(playerLegOutput[i]);
            }
            listBoxOutput.Items.Add("");
            for (int i = 0; i < computerWLOutput.Count; i++)
            {
                listBoxOutput.Items.Add(computerWLOutput[i]);
            }
            for (int i = 0; i < computerLegOutput.Count; i++)
            {
                listBoxOutput.Items.Add(computerLegOutput[i]);
            }

            //Write money to textbox
            textBoxPlayerMoney.Text = playerMoney.ToString("c");
            textBoxComputerMoney.Text = computerMoney.ToString("c");

            //Draw all rolled die
            int x = label8.Width + 10;
            if (rolledDice.Count != 0)
            {
                foreach (Dice d in rolledDice)
                {
                    e.Graphics.DrawImage(d.Bmp, x, label8.Top);
                    x += (int)rolledDice[0].Width;
                }
            }            
        }

        /// <summary>
        /// Places a mirage or oasis down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            bool valid = true;

            if (e.Y <= camelList[0].CamelHeight * 5 && e.X <= camelList[0].CamelWidth * 16)
            {
                if (playerOasis)
                {
                    playerDesertX = e.X;
                    playerDesertY = e.Y;
                    playerOasis = false;
                    playerDesert = "Oasis";
                    Console.WriteLine("Player has placed an oasis.");
                    buttonDisable();
                }
                else if (playerMirage)
                {
                    playerDesertX = e.X;
                    playerDesertY = e.Y;
                    playerOasis = false;
                    playerDesert = "Mirage";
                    Console.WriteLine("Player has placed a mirage.");
                    buttonDisable();
                }
                //Check if no camels are one space away
                foreach (Camel a in camelList)
                {
                    if ((a.X+ camelList[0].CamelWidth) < e.X && e.X < (a.X+ camelList[0].CamelWidth * 2) || 
                        (a.X- camelList[0].CamelWidth * 2) < e.X && e.X < (a.X- camelList[0].CamelWidth) || 
                        a.X < e.X && e.X < (a.X + camelList[0].CamelWidth))
                    {
                        valid = false;
                    }
                }
                if (!valid)
                {
                    MessageBox.Show("You can not place a desert there.");
                }
                else
                {
                    this.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("You have not clicked inside the zone for a desert.");
            }
        }

        /// <summary>
        /// Creates all leg bet cards
        /// </summary>
        private void InitialiseCards()
        {
            playerPossible.Clear();
            playerPlayed.Clear();

            computerPossible.Clear();
            computerPlayed.Clear();

            foreach (Camel a in camelList)
            {
                playerPossible.Add(new LegBet(a.Colour, 2));
                playerPossible.Add(new LegBet(a.Colour, 3));
                playerPossible.Add(new LegBet(a.Colour, 5));

                computerPossible.Add(new LegBet(a.Colour, 2));
                computerPossible.Add(new LegBet(a.Colour, 3));
                computerPossible.Add(new LegBet(a.Colour, 5));

            }
        }

        /// <summary>
        /// Initialise pyramid dice
        /// </summary>
        private void InitialisePyramid()
        {
            foreach (Camel a in camelList)
            {
                pyramid.Add(a.camelDice);
            }
        }

        /// <summary>
        /// Initialise colours that computer can bet on for winning or losing
        /// </summary>
        private void InitialiseComputerWL()
        {
            foreach (Camel a in camelList)
            {
                computerWins.Add(a.Colour);
                computerLoss.Add(a.Colour);
            }
        }

        /// <summary>
        /// Rolls all dice to start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRollDice_Click(object sender, EventArgs e)
        {
            int move = 0;
            InitialiseCards();
            InitialiseComputerWL();
            InitialisePyramid();
            foreach (Camel a in camelList)
            {
                move = a.camelDice.Roll(r.Next(0,3));
                rolledDice.Add(a.camelDice);
                a.X += move * (int)camelList[0].CamelWidth;
                a.Y = (int)camelList[0].CamelHeight * 5;
                //Check if there are any camels on the same place
                foreach (Camel b in camelList)
                {
                    if (b.X == a.X)
                    {
                        a.Y -= (int)camelList[0].CamelHeight;
                    }
                }              
            }
            this.Invalidate();
            buttonRollDice.Enabled = false;
            buttonEnable();
        }

        /// <summary>
        /// Enables all buttons
        /// </summary>
        private void buttonEnable()
        {
            foreach (Control a in Controls)
            {
                if (a is Button)
                {
                    Button b = (Button)a;
                    if (b.Text == "1")
                    {
                        b.Enabled = false;
                    }
                    else
                    {
                        b.Enabled = true;
                    }                  
                }
                else if (a is RadioButton)
                {
                    RadioButton c = (RadioButton)a;
                    c.Enabled = true;
                }
            }

            buttonRollDice.Enabled = false;

        }

        /// <summary>
        /// Disables all buttons
        /// </summary>
        private void buttonDisable()
        {
            foreach (Control a in Controls)
            {
                if (a is Button)
                {
                    Button b = (Button)a;
                    b.Enabled = false;
                }
                else if (a is RadioButton)
                {
                    RadioButton c = (RadioButton)a;
                    c.Enabled = false;
                }
            }

            buttonEndTurn.Enabled = true;
        }

        /// <summary>
        /// Gets a random dice from the pyramid and rolls it and moves the corresponding camel and any above
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMoveCamel_Click(object sender, EventArgs e)
        {
            if (playerTurn)
            {
                playerLegOutput.Add("-Pyramid");
                Console.WriteLine("Player has taken one pyramid tile.");
                playerTurn = false;
            }
            else
            {
                computerLegOutput.Add("-Pyramid");
                Console.WriteLine("Computer has taken one pyramid tile.");
                playerTurn = true;
            }
            if (rolledDice.Count == 5)
            {
                rolledDice.Clear();
            }
            int move;
            int pyramidPick = 0;
            pyramidPick = r.Next(0, pyramid.Count);
            OrderCamels();

            foreach (Camel a in camelList)
            {
                if (a.Colour == pyramid[pyramidPick].Colour)
                {
                    pyramid.RemoveAt(pyramidPick);
                    move = a.camelDice.Roll(r.Next(0, 3));
                    rolledDice.Add(a.camelDice);

                    //Check if the camel is moving onto a desert
                    if ((a.X + move * camelList[0].CamelWidth) < playerDesertX && playerDesertX < (a.X + move * camelList[0].CamelWidth + camelList[0].CamelWidth) 
                        || (a.X + move * camelList[0].CamelWidth) < computerDesertX && computerDesertX < (a.X + move * camelList[0].CamelWidth + camelList[0].CamelWidth))
                    {
                        if (playerDesert == "Mirage")
                        {
                            move -= 1;
                            playerMoney += 1m;
                            MessageBox.Show("Camel has landed on player Mirage.");
                        }
                        else if (playerDesert == "Oasis")
                        {
                            move += 1;
                            playerMoney += 1m;
                            MessageBox.Show("Camel has landed on player Oasis.");
                        }
                        else if (computerDesert == "Mirage")
                        {
                            move -= 1;
                            computerMoney += 1m;
                            MessageBox.Show("Camel has landed on computer Mirage.");
                        }
                        else if (computerDesert == "Oasis")
                        {
                            move += 1;
                            computerMoney += 1m;
                            MessageBox.Show("Camel has landed on computer Oasis.");
                        }
                    }

                    OrderCamels();
                    //Find camels above the camel to move
                    foreach (Camel b in camelList)
                    {
                        if (b.X == a.X && b.Y < a.Y && b != a)
                        {
                            b.X += move * (int)camelList[0].CamelWidth;
                            b.Y = (int)camelList[0].CamelHeight * 3;
                            //Check if there are any camels on the same place
                            foreach (Camel c in camelList)
                            {
                                if (c.X == b.X && c.Y >= b.Y && c != b)
                                {
                                    b.Y -= (int)camelList[0].CamelHeight;
                                }                                
                            }
                        }
                    }

                    //Move the camel that dice was rolled
                    a.X += move * (int)camelList[0].CamelWidth;
                    a.Y = (int)camelList[0].CamelHeight * 4;
                    //Sort the camels by y position
                    OrderCamels();
                    //Check if there are any camels on the same place
                    foreach (Camel d in camelList)
                    {
                        if (d.X == a.X && d.Y >= a.Y && d != a)
                        {
                            a.Y -= (int)camelList[0].CamelHeight;
                        }
                    }

                    OrderCamels();
                    this.Invalidate();
                    buttonDisable();
                    buttonEndTurn.Enabled = true;
                    //Check if there is a winner
                    foreach (Camel f in camelList)
                    {
                        if (f.X >= camelList[0].CamelWidth*15)
                        {
                            playerTurn = true;
                            EndLeg();
                            GameOver();
                        }
                    }
                    //Check if leg has ended
                    if (rolledDice.Count == 5)
                    {
                        EndLeg();
                    }                   
                    break;
                }
            }           
        }

        /// <summary>
        /// Orders the camels from last to first place
        /// </summary>
        private void OrderCamels()
        {
            camelList.Sort(
                delegate (Camel p1, Camel p2)
                {
                    int compare = p1.X.CompareTo(p2.X);
                    if (compare == 0)
                    {
                        return p2.Y.CompareTo(p1.Y);
                    }
                    return compare;
                }               
            );
        }

        /// <summary>
        /// Places mirage down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlaceMirage_Click(object sender, EventArgs e)
        {
            buttonDisable();
            playerMirage = true;
        }

        /// <summary>
        /// Places oasis down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlaceOasis_Click(object sender, EventArgs e)
        {
            buttonDisable();
            playerOasis = true;
        }

        /// <summary>
        /// Places bet on blue for the leg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLegBlue_Click(object sender, EventArgs e)
        {
            int currentValue = 0;
            int amountToChange = 0;
            LegBet cardToRemove = null;
            bool found = false;
            currentValue = int.Parse(buttonLegBlue.Text);
            if (radioButton2.Checked == true)
            {
                amountToChange = 2;
            }
            else if (radioButton3.Checked == true)
            {
                amountToChange = 3;
            }
            else if (radioButton5.Checked == true)
            {
                amountToChange = 5;
            }
            else
            {
                MessageBox.Show("Please select amount to bet.");
            }

            if (amountToChange != 0)
            {
                foreach (LegBet a in playerPossible)
                {
                    if (a.Colour == Color.Blue && a.WinValue == amountToChange)
                    {
                        currentValue += amountToChange;
                        cardToRemove = a;
                        playerLegOutput.Add(a.ToString());
                        found = true;
                    }
                }
            }
            
            buttonLegBlue.Text = currentValue.ToString();
            if (!found)
            {
                MessageBox.Show("You can not play that card.");
            }
            else
            {
                Console.WriteLine("Player has made a leg bet.");
                playerPlayed.Add(cardToRemove);
                playerPossible.Remove(cardToRemove);
                buttonDisable();
            }
        }

        /// <summary>
        /// Places bet on yellow for the leg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLegYellow_Click(object sender, EventArgs e)
        {
            int currentValue = 0;
            int amountToChange = 0;
            LegBet cardToRemove = null;
            bool found = false;
            currentValue = int.Parse(buttonLegYellow.Text);
            if (radioButton2.Checked == true)
            {
                amountToChange = 2;
            }
            else if (radioButton3.Checked == true)
            {
                amountToChange = 3;
            }
            else if (radioButton5.Checked == true)
            {
                amountToChange = 5;
            }
            else
            {
                MessageBox.Show("Please select amount to bet.");
            }

            if (amountToChange != 0)
            {
                foreach (LegBet a in playerPossible)
                {
                    if (a.Colour == Color.Yellow && a.WinValue == amountToChange)
                    {
                        currentValue += amountToChange;
                        cardToRemove = a;
                        playerLegOutput.Add(a.ToString());
                        found = true;
                    }
                }
            }

            buttonLegYellow.Text = currentValue.ToString();
            if (!found)
            {
                MessageBox.Show("You can not play that card.");
            }
            else
            {
                Console.WriteLine("Player has made a leg bet.");
                playerPlayed.Add(cardToRemove);
                playerPossible.Remove(cardToRemove);
                buttonDisable();
            }
        }

        /// <summary>
        /// Places bet on green for the leg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLegGreen_Click(object sender, EventArgs e)
        {
            int currentValue = 0;
            int amountToChange = 0;
            LegBet cardToRemove = null;
            bool found = false;
            currentValue = int.Parse(buttonLegGreen.Text);
            if (radioButton2.Checked == true)
            {
                amountToChange = 2;
            }
            else if (radioButton3.Checked == true)
            {
                amountToChange = 3;
            }
            else if (radioButton5.Checked == true)
            {
                amountToChange = 5;
            }
            else
            {
                MessageBox.Show("Please select amount to bet.");
            }

            if (amountToChange != 0)
            {
                foreach (LegBet a in playerPossible)
                {
                    if (a.Colour == Color.Green && a.WinValue == amountToChange)
                    {
                        currentValue += amountToChange;
                        cardToRemove = a;
                        playerLegOutput.Add(a.ToString());
                        found = true;
                    }
                }
            }

            buttonLegGreen.Text = currentValue.ToString();
            if (!found)
            {
                MessageBox.Show("You can not play that card.");
            }
            else
            {
                Console.WriteLine("Player has made a leg bet.");
                playerPlayed.Add(cardToRemove);
                playerPossible.Remove(cardToRemove);
                buttonDisable();
            }
        }

        /// <summary>
        /// Places bet on orange for the leg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLegOrange_Click(object sender, EventArgs e)
        {
            int currentValue = 0;
            int amountToChange = 0;
            LegBet cardToRemove = null;
            bool found = false;
            currentValue = int.Parse(buttonLegOrange.Text);
            if (radioButton2.Checked == true)
            {
                amountToChange = 2;
            }
            else if (radioButton3.Checked == true)
            {
                amountToChange = 3;
            }
            else if (radioButton5.Checked == true)
            {
                amountToChange = 5;
            }
            else
            {
                MessageBox.Show("Please select amount to bet.");
            }

            if (amountToChange != 0)
            {
                foreach (LegBet a in playerPossible)
                {
                    if (a.Colour == Color.Orange && a.WinValue == amountToChange)
                    {
                        currentValue += amountToChange;
                        cardToRemove = a;
                        playerLegOutput.Add(a.ToString());
                        found = true;
                    }
                }
            }

            buttonLegOrange.Text = currentValue.ToString();
            if (!found)
            {
                MessageBox.Show("You can not play that card.");
            }
            else
            {
                Console.WriteLine("Player has made a leg bet.");
                playerPlayed.Add(cardToRemove);
                playerPossible.Remove(cardToRemove);
                buttonDisable();
            }
        }

        /// <summary>
        /// Places bet on white for the leg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLegWhite_Click(object sender, EventArgs e)
        {
            int currentValue = 0;
            int amountToChange = 0;
            LegBet cardToRemove = null;
            bool found = false;
            currentValue = int.Parse(buttonLegWhite.Text);
            if (radioButton2.Checked == true)
            {
                amountToChange = 2;
            }
            else if (radioButton3.Checked == true)
            {
                amountToChange = 3;
            }
            else if (radioButton5.Checked == true)
            {
                amountToChange = 5;
            }
            else
            {
                MessageBox.Show("Please select amount to bet.");
            }

            if (amountToChange != 0)
            {
                foreach (LegBet a in playerPossible)
                {
                    if (a.Colour == Color.White && a.WinValue == amountToChange)
                    {
                        currentValue += amountToChange;
                        cardToRemove = a;
                        playerLegOutput.Add(a.ToString());
                        found = true;
                    }
                }
            }

            buttonLegWhite.Text = currentValue.ToString();
            if (!found)
            {
                MessageBox.Show("You can not play that card.");
            }
            else
            {
                Console.WriteLine("Player has made a leg bet.");
                playerPlayed.Add(cardToRemove);
                playerPossible.Remove(cardToRemove);
                buttonDisable();
            }
        }

        /// <summary>
        /// Bets on blue for the overall winner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWinnerBlue_Click(object sender, EventArgs e)
        {
            buttonWinnerBlue.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Winner Color [Blue]");
            allWOutput.Add("-Player Winner Color [Blue]");
            Console.WriteLine("Player has made a winner bet.");
        }

        /// <summary>
        /// Bets on yellow for the overall winner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWinnerYellow_Click(object sender, EventArgs e)
        {
            buttonWinnerYellow.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Winner Color [Yellow]");
            allWOutput.Add("-Player Winner Color [Yellow]");
            Console.WriteLine("Player has made a winner bet.");
        }

        /// <summary>
        /// Bets on green for the overall winner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWinnerGreen_Click(object sender, EventArgs e)
        {
            buttonWinnerGreen.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Winner Color [Green]");
            allWOutput.Add("-Player Winner Color [Green]");
            Console.WriteLine("Player has made a winner bet.");
        }

        /// <summary>
        /// Bets on green for the overall winner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWinnerOrange_Click(object sender, EventArgs e)
        {
            buttonWinnerOrange.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Winner Color [Orange]");
            allWOutput.Add("-Player Winner Color [Orange]");
            Console.WriteLine("Player has made a winner bet.");
        }

        /// <summary>
        /// Bets on white for the overall winner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWinnerWhite_Click(object sender, EventArgs e)
        {
            buttonWinnerWhite.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Winner Color [White]");
            allWOutput.Add("-Player Winner Color [White]");
            Console.WriteLine("Player has made a winner bet.");
        }

        /// <summary>
        /// Bets on blue for the overall loser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoserBlue_Click(object sender, EventArgs e)
        {
            buttonLoserBlue.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Loser Color [Blue]");
            allLOutput.Add("-Player Loser Color [Blue]");
            Console.WriteLine("Player has made a loser bet.");
        }

        /// <summary>
        /// Bets on yellow for the overall loser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoserYellow_Click(object sender, EventArgs e)
        {
            buttonLoserYellow.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Loser Color [Yellow]");
            allLOutput.Add("-Player Loser Color [Yellow]");
            Console.WriteLine("Player has made a loser bet.");
        }

        /// <summary>
        /// Bets on green for the overall loser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoserGreen_Click(object sender, EventArgs e)
        {
            buttonLoserGreen.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Loser Color [Green]");
            allLOutput.Add("-Player Loser Color [Green]");
            Console.WriteLine("Player has made a loser bet.");
        }

        /// <summary>
        /// Bets on green for the overall loser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoserOrange_Click(object sender, EventArgs e)
        {
            buttonLoserOrange.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Loser Color [Orange]");
            allLOutput.Add("-Player Loser Color [Orange]");
            Console.WriteLine("Player has made a loser bet.");
        }

        /// <summary>
        /// Bets on white for the overall loser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoserWhite_Click(object sender, EventArgs e)
        {
            buttonLoserWhite.Text = "1";
            buttonDisable();
            playerWLOutput.Add("-Loser Color [White]");
            allLOutput.Add("-Player Loser Color [White]");
            Console.WriteLine("Player has made a loser bet.");
        }

        /// <summary>
        /// Ends the players turn and begins the computer's turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            string choice = "";
            bool valid = false;
            playerTurn = false;
            OrderCamels();

            while (!valid)
            {
                valid = true;
                try
                {
                    //Choose computer to use
                    Random r = new Random();
                    Computer computer = computerList[r.Next(0, 3)];
                    Console.WriteLine("Using " + computer.Name);

                    //Computer Turn
                    choice = computer.Choice(r.Next(0, 1001));
                    if (choice == "Move")
                    {
                        buttonMoveCamel_Click(sender, e);
                    }
                    else if (choice == "Oasis")
                    {
                        Console.WriteLine("Computer has placed an oasis.");
                        computerDesertX = (int)camelList[4].X + r.Next(2, 4) * (int)camelList[4].CamelWidth + 1;
                        if (computerDesertX > camelList[4].CamelWidth * 16)
                        {
                            computerDesertX = 0;
                            valid = false;
                        }
                        else
                        {
                            computerDesert = "Oasis";
                        }
                    }
                    else if (choice == "Mirage")
                    {
                        Console.WriteLine("Computer has placed an mirage.");
                        computerDesertX = (int)camelList[4].X + r.Next(2, 4) * (int)camelList[4].CamelWidth + 1;
                        if (computerDesertX > camelList[4].CamelWidth * 16)
                        {
                            computerDesertX = 0;
                            valid = false;
                        }
                        else
                        {
                            computerDesert = "Mirage";
                        }
                    }
                    else if (choice == "Leg")
                    {
                        //Bet on a random colour with random amount
                        Contract.Requires(computerPossible != null);
                        LegBet a = computerPossible[r.Next(0, computerPossible.Count)];
                        computerPlayed.Add(a);
                        computerPossible.Remove(a);
                        Console.WriteLine("Computer has made a leg bet.");
                        computerLegOutput.Add(a.ToString());
                    }
                    else if (choice == "Leg Cautious")
                    {
                        //Bet on the lead or second camel with low amount
                        Contract.Requires(computerPossible != null);
                        List<LegBet> possible = new List<LegBet>();
                        foreach (LegBet l in computerPossible)
                        {
                            if ((l.WinValue == 2 || l.WinValue == 3) &&
                                l.Colour != camelList[3].Colour && l.Colour == camelList[4].Colour)
                            {
                                possible.Add(l);
                            }
                        }

                        LegBet a = possible[r.Next(0, possible.Count)];
                        computerPlayed.Add(a);
                        computerPossible.Remove(a);
                        Console.WriteLine("Computer has made a leg bet.");
                        computerLegOutput.Add(a.ToString());
                    }
                    else if (choice == "Leg Aggressive")
                    {
                        //Bets with a high amount with a smaller chance of winning
                        Contract.Requires(computerPossible != null);
                        List<LegBet> possible = new List<LegBet>();
                        foreach (LegBet l in computerPossible)
                        {
                            if ((l.WinValue == 5 || l.WinValue == 3) &&
                                l.Colour != camelList[3].Colour && l.Colour == camelList[0].Colour)
                            {
                                possible.Add(l);
                            }
                        }

                        LegBet a = possible[r.Next(0, possible.Count)];
                        computerPlayed.Add(a);
                        computerPossible.Remove(a);
                        Console.WriteLine("Computer has made a leg bet.");
                        computerLegOutput.Add(a.ToString());
                    }
                    else if (choice == "Winner")
                    {
                        //Bet on random winner
                        Contract.Requires(computerWins != null);
                        Color b = computerWins[r.Next(0, computerWins.Count)];
                        computerWins.Add(b);
                        computerWins.Remove(b);
                        Console.WriteLine("Computer has made a winner bet.");
                        computerWLOutput.Add("-Winner " + b.ToString());
                        allWOutput.Add("-Computer Winner " + b.ToString());
                    }
                    else if (choice == "Winner Cautious")
                    {
                        //Bet on winner to be leading camel
                        Contract.Requires(computerWins != null);
                        Color b = computerWins[r.Next(0, computerWins.Count)];
                        List<Color> possible = new List<Color>();
                        foreach (Color l in computerWins)
                        {
                            if (l == camelList[3].Colour || l == camelList[4].Colour)
                            {
                                possible.Add(l);
                            }
                        }

                        Color a = possible[r.Next(0, possible.Count)];
                        computerWins.Remove(a);
                        Console.WriteLine("Computer has made a winner bet.");
                        computerWLOutput.Add("-Winner " + b.ToString());
                        allWOutput.Add("-Computer Winner " + b.ToString());
                    }
                    else if (choice == "Winner Aggressive")
                    {
                        //Bet on winner to be lower camel
                        Contract.Requires(computerWins != null);
                        Color b = computerWins[r.Next(0, computerWins.Count)];
                        List<Color> possible = new List<Color>();
                        foreach (Color l in computerWins)
                        {
                            if (l == camelList[1].Colour || l == camelList[2].Colour)
                            {
                                possible.Add(l);
                            }
                        }

                        Color a = possible[r.Next(0, possible.Count)];
                        computerWins.Remove(a);
                        Console.WriteLine("Computer has made a winner bet.");
                        computerWLOutput.Add("-Winner " + a.ToString());
                        allWOutput.Add("-Computer Winner " + a.ToString());
                    }
                    else if (choice == "Loser")
                    {
                        //Bet on random loser
                        Contract.Requires(computerLoss != null);
                        Color b = computerLoss[r.Next(0, computerLoss.Count)];
                        computerLoss.Remove(b);
                        Console.WriteLine("Computer has made a loser bet.");
                        computerWLOutput.Add("-Loser " + b.ToString());
                        allLOutput.Add("-Computer Loser " + b.ToString());
                    }
                    else if (choice == "Loser Cautious")
                    {
                        //Bet on loser to be last camel
                        Contract.Requires(computerLoss != null);
                        Color b = computerWins[r.Next(0, computerWins.Count)];
                        List<Color> possible = new List<Color>();
                        foreach (Color l in computerWins)
                        {
                            if (l == camelList[0].Colour || l == camelList[1].Colour)
                            {
                                possible.Add(l);
                            }
                        }

                        Color a = possible[r.Next(0, possible.Count)];
                        computerLoss.Remove(a);
                        Console.WriteLine("Computer has made a loser bet.");
                        computerWLOutput.Add("-Loser " + a.ToString());
                        allLOutput.Add("-Computer Loser " + a.ToString());
                    }
                    else if (choice == "Loser Aggressive")
                    {
                        //Bet on loser to be last camel
                        Contract.Requires(computerLoss != null);
                        Color b = computerWins[r.Next(0, computerWins.Count)];
                        List<Color> possible = new List<Color>();
                        foreach (Color l in computerWins)
                        {
                            if (l == camelList[2].Colour || l == camelList[3].Colour)
                            {
                                possible.Add(l);
                            }
                        }

                        Color a = possible[r.Next(0, possible.Count)];
                        computerLoss.Remove(a);
                        Console.WriteLine("Computer has made a loser bet.");
                        computerWLOutput.Add("-Loser " + a.ToString());
                        allLOutput.Add("-Computer Loser " + a.ToString());
                    }

                }
                catch
                {
                    valid = false;
                }
            }

            buttonEnable();
            playerTurn = true;
            this.Invalidate();                
        }

        /// <summary>
        /// When the leg has ended calculate new balances
        /// </summary>
        private void EndLeg()
        {
            MessageBox.Show("Leg Has Ended.");
            OrderCamels();

            //Find leg bets and calculate money
            foreach (LegBet a in playerPlayed)
            {
                if (a.Colour == camelList[4].Colour)
                {
                    playerMoney += a.WinValue;
                }
                else if (a.Colour == camelList[3].Colour)
                {
                    playerMoney += 1m;
                }
                else
                {
                    playerMoney -= 1m;
                }
            }
            foreach (LegBet b in computerPlayed)
            {
                if (b.Colour == camelList[4].Colour)
                {
                    computerMoney += b.WinValue;
                }
                else if (b.Colour == camelList[3].Colour)
                {
                    computerMoney += 1m;
                }
                else
                {
                    computerMoney -= 1m;
                }
            }
            InitialiseCards();

            //Find pyramids and calculate money
            foreach (String s in playerLegOutput)
            {
                if (s.Contains("Pyramid") == true)
                {
                    playerMoney += 1m;
                }
            }
            foreach (String r in computerLegOutput)
            {
                if (r.Contains("Pyramid") == true)
                {
                    computerMoney += 1m;
                }
            }
            playerLegOutput.Clear();
            computerLegOutput.Clear();

            //Reset track
            InitialisePyramid();
            rolledDice.Clear();
            playerMirage = false;
            playerOasis = false;
            playerDesertX = 0;
            playerDesertY = 0;
            playerDesert = "";
            computerDesertX = 0;
            computerDesert = "";

            if (playerTurn == true)
            {
                buttonEnable();
            }
            else
            {
                buttonDisable();
                buttonEndTurn_Click(new object(), new EventArgs());
            }
            this.Invalidate();
        }

        /// <summary>
        /// When one camel has reached the finish line
        /// </summary>
        public void GameOver()
        {
            MessageBox.Show("The Game has Finished.");
            Camel winner = camelList[4];
            Camel loser = camelList[0];
            decimal count = 8m;
            //See who picked the correct winner
            foreach (String s in allWOutput)
            {
                if (s.Contains(winner.Colour.ToString()))
                {
                    if (count == 8m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 5m;
                    }
                    else if (count == 5m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 3m;
                    }
                    else if (count == 3m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 2m;
                    }
                    else if (count == 2m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 1m;
                    }
                    else
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                    }
                }
                else
                {
                    if (s.Contains("Player"))
                    {
                        playerMoney -= 1m;
                    }
                    else
                    {
                        computerMoney -= 1m;
                    }
                }                
            }

            count = 8m;
            //See who picked the correct loser
            foreach (String s in allLOutput)
            {
                if (s.Contains(loser.Colour.ToString()))
                {
                    if (count == 8m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 5m;
                    }
                    else if (count == 5m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 3m;
                    }
                    else if (count == 3m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 2m;
                    }
                    else if (count == 2m)
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                        count = 1m;
                    }
                    else
                    {
                        if (s.Contains("Player"))
                        {
                            playerMoney += count;
                        }
                        else
                        {
                            computerMoney += count;
                        }
                    }
                }
                else
                {
                    if (s.Contains("Player"))
                    {
                        playerMoney -= 1m;
                    }
                    else
                    {
                        computerMoney -= 1m;
                    }
                }
            }
            this.Invalidate();

            //See who has won and then close the application
            if (playerMoney > computerMoney)
            {
                MessageBox.Show("You have won!");
            }
            else if (playerMoney < computerMoney)
            {
                MessageBox.Show("The computer has won!");
            }
            else
            {
                MessageBox.Show("You have tied!");
            }
            this.Close();
        }

        /// <summary>
        /// Resizes all buttons when form resizes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            //Gets ratio of change in height/width of form from original
            double heightChange = (double)this.Height / (double)formHeight;
            double widthChange = (double)this.Width / (double)formWidth;

            //Changes size of images
            foreach (Camel a in camelList)
            {
                a.CamelHeight = a.CamelHeight * heightChange;
                a.CamelWidth = a.CamelWidth * widthChange;
                a.X = a.X * widthChange;
                a.Y = a.Y * heightChange;
                a.camelDice.Height = a.camelDice.Height * heightChange;
                a.camelDice.Width = a.camelDice.Width * widthChange;
            }

            foreach (Control c in Controls)
            {
                if (c is ListBox)
                {
                    c.Width = (int)(c.Width * widthChange);
                }
                else
                {
                    c.Left = (int)(c.Left * widthChange);
                    c.Top = (int)(c.Top * heightChange);
                    c.Height = (int)(c.Height * heightChange);
                    c.Width = (int)(c.Width * widthChange);
                }               
            }

            //Sets new form height and width
            formHeight = this.Height;
            formWidth = this.Width;

            this.Invalidate();
        }
    }
}
