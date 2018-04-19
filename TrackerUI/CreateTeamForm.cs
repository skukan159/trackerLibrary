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

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        ITeamRequester callingForm;
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm( ITeamRequester caller)
        {
            InitializeComponent();

            //CreateSampleData();
            callingForm = caller;

            WireUpLists();
        }




        //Testing purposes Method

      /*  private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Filip", LastName = "Skukan" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Bill", LastName = "Jones" });
        }*/

        private void WireUpLists()
        {
            //TODO - Better way of refresing of the data binding
            selectTeamMembersDropdown.DataSource = null;
            selectTeamMembersDropdown.DataSource = availableTeamMembers;
            selectTeamMembersDropdown.DisplayMember = "FullName";

            tournamentPlayersListBox.DataSource = null;
            tournamentPlayersListBox.DataSource = selectedTeamMembers;
            tournamentPlayersListBox.DisplayMember = "FullName";


        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameTextBox.Text;
                p.LastName = lastNameTextBox.Text;
                p.EmailAdress = emailTextBox.Text;
                p.CellphoneNumber = cellphoneTextBox.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();


                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                emailTextBox.Text = "";
                cellphoneTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all the fields.");
            }
        }

        private bool ValidateForm()
        {
            //TODO - Add validation to the form
            if (firstNameTextBox.Text.Length == 0)
                return false;
            if (lastNameTextBox.Text.Length == 0)
                return false;
            if (emailTextBox.Text.Length == 0)
                return false;
            if (cellphoneTextBox.Text.Length == 0)
                return false;

            return true;
        }

        
        private void addMemberButton_Click(object sender, EventArgs e)
        {
            //Put the selected person from the dropdown to the p variable
            PersonModel p = (PersonModel)selectTeamMembersDropdown.SelectedItem;

            if (p != null)
            {
                //Add the person to the selected Team Members List And remove it from the dropdown
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);
                //Show the changes
                WireUpLists();
            }
          
        }


        private void removeSelectedButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)tournamentPlayersListBox.SelectedItem;

            if (p != null)
            {
                //Add the person to the dropdown and remove from the selected Team Members List
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);
                //Show the changes
                WireUpLists();
            }

        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();

            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);

            callingForm.TeamComplete(t);

            this.Close();

       
        }
    }
}
