using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace CashRegisterSummative
{
    public partial class Form1 : Form
    {
        //Anouncing Global Constants
        //item costs
        const double MCBURGER_COST = 6.79;
        const double MCFRIES_COST = 1.79;
        const double MCDRINK_COST = 1.29;
        //tax percentage
        const double HST_TAX = 0.13;

        //Anouncing Global Variables
        //representing the number of each item purchased
        double mcBurgersPurchased;
        double mcFriesPurchased;
        double mcDrinksPurchased;
        //representing the cost of the purchase
        double subTotalCost;
        double taxCost;
        double totalCost;
        //representing the amount of money used to pay for items
        double tenderedAmount;
        //representing the amount of change the customer will receive
        double changeAmount;

        //Anouncing Global Sound Variables
        //played when button press is successful
        SoundPlayer beepSoundPlayer = new SoundPlayer(Properties.Resources.beepSound);
        //played when there is a problem with the input
        SoundPlayer errorSoundPlayer = new SoundPlayer(Properties.Resources.errorSound);
        //played when 'McPrint' button is pressed to represent the printing of the receipt
        SoundPlayer printingSoundPlayer = new SoundPlayer(Properties.Resources.printingSound);

        //initialize variables to 0???fe;oeno3b34ohfoi fb3xp3u3ixo2bu3oxrbuoi'rbuxorbubu23bu32bpooxb32pubx3op2xbuop'32burbr3obux32o'b32'xbux32obr'32bur'pb32o'bo'pr32xqbuu

        public Form1()
        {
            InitializeComponent();
        }

        private void mcTotalButton_Click(object sender, EventArgs e)
        {
            //Try-Catch (so sytem does not crash if input is not a number)
            try
            {
                //converting textbox inputs into integers and placing them in the appropriate variables
                mcBurgersPurchased = Convert.ToDouble(McBurgersTextBox.Text);
                mcFriesPurchased = Convert.ToDouble(McFriesTextBox.Text);
                mcDrinksPurchased = Convert.ToDouble(McDrinksTextBox.Text);

                //plays 'beep' sound
                beepSoundPlayer.Play();

                //making necessary labels visible
                subTotalLabel.Visible = true;
                taxLabel.Visible = true;
                totalLabel.Visible = true;
                blackLineLabel.Visible = true;
                tenderedLabel.Visible = true;
                tenderedTextBox.Visible = true;
                mcChangeButton.Visible = true;

                //makes error label invisible
                errorLabel.Visible = false;

                //Calculates costs based on how much of each item was purchased
                subTotalCost = mcBurgersPurchased * MCBURGER_COST + mcFriesPurchased * MCFRIES_COST +
                    mcDrinksPurchased * MCDRINK_COST;
                taxCost = subTotalCost * HST_TAX;
                totalCost = subTotalCost + taxCost;

                //Shows user the costs of the purchase
                subTotalLabel.Text = "Sub Total " + subTotalCost.ToString("C");
                taxLabel.Text = "Tax       " + taxCost.ToString("C");
                totalLabel.Text = "Total     " + totalCost.ToString("C");
            }

            catch
            {
                //in case a non numerical input was made
                //make the error label visible
                errorLabel.Visible = true;
                //plays 'error sound'
                errorSoundPlayer.Play();
            }
        }

        private void mcChangeButton_Click(object sender, EventArgs e)
        {
            //Try-Catch (so sytem does not crash if input is not a number)
            try
            {
                //calculates change based on tendered amount and the overall purchase cost
                tenderedAmount = Convert.ToDouble(tenderedTextBox.Text);
                changeAmount = tenderedAmount - totalCost;

                //if-else statement so that the change value cannot be a negative number
                if (changeAmount < 0)
                {
                    //make the error label visible
                    errorLabel.Visible = true;
                    //plays 'error sound'
                    errorSoundPlayer.Play();
                }

                else
                {
                    //only happens if the change value is a positive number
                    //plays 'beep'
                    beepSoundPlayer.Play();
                    //makes error label invisible
                    errorLabel.Visible = false;
                    //makes necessary label and button visible
                    changeLabel.Visible = true;
                    mcPrintButton.Visible = true;
                    //shows the user the amount of change the customer should receive
                    changeLabel.Text = "Change    " + changeAmount.ToString("C");
                }
            }

            catch
            {
                //in case a non numerical input was made
                //make the error label visible
                errorLabel.Visible = true;
                //plays 'error sound'
                errorSoundPlayer.Play();
            }
        }

        private void mcPrintButton_Click(object sender, EventArgs e)
        {
            //Anouncing Graphics Variables
            Graphics g = this.CreateGraphics();
            //pens
            Pen blackPen = new Pen(Color.Black, 4);
            //brushes
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            //fonts
            Font consolasFont = new Font("Consolas", 10, FontStyle.Bold);

            //Invisible Labels, Textboxes, and Buttons
            //so that they are not there when receipt is printed
            titleLabel.Visible = false;
            McBurgersLabel.Visible = false;
            McFriesLabel.Visible = false;
            McDrinksLabel.Visible = false;
            McBurgersTextBox.Visible = false;
            McFriesTextBox.Visible = false;
            McDrinksTextBox.Visible = false;
            mcTotalButton.Visible = false;
            mcChangeButton.Visible = false;
            mcPrintButton.Visible = false;
            subTotalLabel.Visible = false;
            taxLabel.Visible = false;
            totalLabel.Visible = false;
            blackLineLabel.Visible = false;
            tenderedLabel.Visible = false;
            tenderedTextBox.Visible = false;
            changeLabel.Visible = false;

            //Changes Form Size
            //form gradually gets smaller
            for (int i = 0; i < 55; i++)
            {
                this.Size = new Size(this.Width - 4, this.Height);
            }

            //plays 'printing' sound
            printingSoundPlayer.Play();

            //clears screen
            g.Clear(Color.White);

            //draws receipt border
            g.DrawRectangle(blackPen, 15, 15, 225, 300);
            g.FillRectangle(whiteBrush, 15, 15, 225, 300);

            //Draws the receipt
            g.DrawString("     MacDoonalds Inc." + "\n" + "\n" +
                "Order Number 02496" + "\n" +
                "October 15, 2018" + "\n" + "\n" +
                "McHamburgers  x" + mcBurgersPurchased.ToString("#") + " \t@ 6.79" + "\n" + 
                "McFries       x" + mcFriesPurchased.ToString("#") + " \t@ 1.79" + "\n" +
                "McDrinks      x" + mcDrinksPurchased.ToString("#") + " \t@ 1.29" + "\n" + "\n" +
                "Sub Total        " + subTotalCost.ToString("C") + "\n" +
                "Tax              " + taxCost.ToString("C") + "\n" +
                "Total            " + totalCost.ToString("C") + "\n" + "\n" +
                "Tendered         " + tenderedAmount.ToString("C") + "\n" +
                "Change           " + changeAmount.ToString("C") + "\n" + "\n" +
                "Have a McTastic Day!", 
                consolasFont, blackBrush, 25, 40);

            //pause so user can read
            Thread.Sleep(7000);

            //Form Size Changes
            //form gradually returns to original size while constantly refreshing the form
            for (int i = 0; i < 55; i++)
            {
                this.Size = new Size(this.Width + 4, this.Height);
                this.Refresh();
            }

            //Return To Original State
            //makes necessary labels, textboxes, and buttons visible
            titleLabel.Visible = true;
            McBurgersLabel.Visible = true;
            McFriesLabel.Visible = true;
            McDrinksLabel.Visible = true;
            McBurgersTextBox.Visible = true;
            McFriesTextBox.Visible = true;
            McDrinksTextBox.Visible = true;
            mcTotalButton.Visible = true;
            //textboxes become empty again
            McBurgersTextBox.Text = "";
            McFriesTextBox.Text = "";
            McDrinksTextBox.Text = "";
            tenderedTextBox.Text = "";
        }
    }
}