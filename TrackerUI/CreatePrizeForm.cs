using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tracker_Library;
using Tracker_Library.Models;
using Tracker_Library.DataAccess;


namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;

        public CreatePrizeForm(IPrizeRequester caller)
        {

            InitializeComponent();
            callingForm = caller;
        }



        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNameTextBox.Text,
                    placeNumberTextBox.Text,
                    prizeAmmountTextBox.Text,
                    prizePercentageTextBox.Text);
               
                    GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);

                this.Close();

                //placeNameTextBox.Text = "";
                //placeNumberTextBox.Text = "";
                //prizeAmmountTextBox.Text = "0";
              //  prizePercentageTextBox.Text = "0";
            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check it and try again");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            bool placeNumberValNumber = int.TryParse(placeNumberTextBox.Text, out placeNumber);

            //  If the place number field value is not a string
            if (!placeNumberValNumber)
            {
                output = false;
            }
            //if the place number field value is less than 1
            if(placeNumber < 1)
            {
                output = false;
            }
            //If the length of the Place Name field is less than 0
            if(placeNameTextBox.Text.Length == 0)
            {
                return false;
            }
            //initialize prize values
            decimal prizeAmmount = 0;
            double prizePercentage = 0;
            //If the prize ammount can be coverted to decimal number value
            bool prizeAmmountValid = decimal.TryParse(prizeAmmountTextBox.Text, out prizeAmmount);
            //Check if the value of price percentage is an integer value
            bool prizePercentageValid = double.TryParse(prizePercentageTextBox.Text, out prizePercentage);

            if(!prizeAmmountValid || !prizePercentageValid)
            {
                output = false;
            }

            ///One of theese has to be above 0.
            ///Either the percentage of the prize
            ///Or the ammount of the prize
            if(prizeAmmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            ///We dont want the prize percentage to be less that 
            ///0 or more than 100
            if(prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }
            return output;
        }


    }
}
