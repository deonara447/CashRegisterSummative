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

        //Anouncing Global
        double mcBurgersPurchased;
        double mcFriesPurchased;
        double mcDrinksPurchased;

        double subTotalCost;
        double taxCost;
        double totalCost;

        double tenderedAmount;

        double changeAmount;

        SoundPlayer beepSoundPlayer = new SoundPlayer(Properties.Resources.beepSound);
        SoundPlayer errorSoundPlayer = new SoundPlayer(Properties.Resources.errorSound);
        SoundPlayer printingSoundPlayer = new SoundPlayer(Properties.Resources.printingSound);

        //initialize variables to 0???

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mcTotalButton_Click(object sender, EventArgs e)
        {
            try
            {
                mcBurgersPurchased = Convert.ToDouble(McBurgersTextBox.Text);
                mcFriesPurchased = Convert.ToDouble(McFriesTextBox.Text);
                mcDrinksPurchased = Convert.ToDouble(McDrinksTextBox.Text);

                beepSoundPlayer.Play();

                subTotalLabel.Visible = true;
                taxLabel.Visible = true;
                totalLabel.Visible = true;
                blackLineLabel.Visible = true;
                tenderedLabel.Visible = true;
                tenderedTextBox.Visible = true;
                mcChangeButton.Visible = true;

                errorLabel.Visible = false;

                subTotalCost = mcBurgersPurchased * MCBURGER_COST + mcFriesPurchased * MCFRIES_COST +
                    mcDrinksPurchased * MCDRINK_COST;
                taxCost = subTotalCost * HST_TAX;
                totalCost = subTotalCost + taxCost;

                subTotalLabel.Text = "Sub Total " + subTotalCost.ToString("C");
                taxLabel.Text = "Tax       " + taxCost.ToString("C");
                totalLabel.Text = "Total     " + totalCost.ToString("C");
            }
            catch
            {
                errorLabel.Visible = true;

                errorSoundPlayer.Play();
            }
        }

        private void mcChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                tenderedAmount = Convert.ToDouble(tenderedTextBox.Text);
                changeAmount = tenderedAmount - totalCost;

                if (changeAmount < 0)
                {
                    errorLabel.Visible = true;

                    errorSoundPlayer.Play();
                }
                else
                {
                    

                    beepSoundPlayer.Play();

                    errorLabel.Visible = false;
                    changeLabel.Visible = true;
                    mcPrintButton.Visible = true;

                    changeLabel.Text = "Change    " + changeAmount.ToString("C");
                }
                
            }
            catch
            {
                errorLabel.Visible = true;

                errorSoundPlayer.Play();
            }
        }

        private void mcPrintButton_Click(object sender, EventArgs e)
        {

            Graphics g = this.CreateGraphics();
            Pen blackPen = new Pen(Color.Black, 4);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Font consolasFont = new Font("Consolas", 10, FontStyle.Bold);
            SolidBrush blackBrush = new SolidBrush(Color.Black);

            

            

            
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

            for (int i = 0; i < 55; i++)
            {
                this.Size = new Size(this.Width - 4, this.Height);
            }

            printingSoundPlayer.Play();

            g.Clear(Color.White);

            g.DrawRectangle(blackPen, 15, 15, 225, 300);
            g.FillRectangle(whiteBrush, 15, 15, 225, 300);

            g.DrawString("     MacDoonalds Inc." + "\n" + "\n" +
                "Order Number 02496" + "\n" +
                "October 15, 2018" + "\n" + "\n" +
                "McHamburgers  x" + mcBurgersPurchased.ToString("#") + " \t@ 6.79" + "\n" + 
                "McFries       x" + mcFriesPurchased.ToString("#") + " \t@ 1.79" + "\n" +
                "McDrinks      x" + mcDrinksPurchased.ToString("#") + " \t@ 1.29" + "\n" + "\n" +
                "Sub Total        " + subTotalCost.ToString("C") + "\n" +
                "Tax              " + taxCost.ToString("C") + "\n" + "\n" +
                "Tendered         " + tenderedAmount.ToString("C") + "\n" +
                "Change           " + changeAmount.ToString("C") + "\n" + "\n" +
                "Have a McTastic Day!", 
                consolasFont, blackBrush, 25, 40);

            Thread.Sleep(7000);

            for (int i = 0; i < 55; i++)
            {
                this.Size = new Size(this.Width + 4, this.Height);
                this.Refresh();

            }

            titleLabel.Visible = true;

            McBurgersLabel.Visible = true;
            McFriesLabel.Visible = true;
            McDrinksLabel.Visible = true;

            McBurgersTextBox.Visible = true;
            McFriesTextBox.Visible = true;
            McDrinksTextBox.Visible = true;

            mcTotalButton.Visible = true;
            newOrderButton.Visible = false;

            McBurgersTextBox.Text = "";
            McFriesTextBox.Text = "";
            McDrinksTextBox.Text = "";
            tenderedTextBox.Text = "";

            this.Refresh();

        }

        private void newOrderButton_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();


            
        }
    }
}