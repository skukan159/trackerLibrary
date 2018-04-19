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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();

        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        

        public CreateTournamentForm()
        {
            InitializeComponent();

            InitializeLists();
        }
        private void InitializeLists()
        {
            selectTeamDropdown.DataSource = null;

            selectTeamDropdown.DataSource = availableTeams;
            selectTeamDropdown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            //Put the selected team from the dropdown to the p variable
            TeamModel t = (TeamModel)selectTeamDropdown.SelectedItem;

            if (t != null)
            {
                //Add the team to the tournamentListBox List And remove it from the dropdown
                availableTeams.Remove(t);
                selectedTeams.Add(t);
                //Show the changes
                InitializeLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //Call the create prize form
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
           
        }

        public void PrizeComplete(PrizeModel model)
        {
            //Go back here when it is filled out
            //Add the prize to the PrizeListBox
            selectedPrizes.Add(model);
            InitializeLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            InitializeLists();
        }

        private void createNewLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Call the create prize form
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void removeTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (t != null)
            {
                //Add the person to the dropdown and remove from the selected Team Members List
                selectedTeams.Remove(t);
                availableTeams.Add(t);
                //Show the changes
                InitializeLists();
            }
        }

        private void removePrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                //Add the person to the dropdown and remove from the selected Team Members List
                selectedPrizes.Remove(p);
                //Show the changes
                InitializeLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            //Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if(!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid entry fee.", "Invalid fee", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //Create our tourament model
            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;


            // TODO - Wire up Matchups
            TouramentLogic.CreateRounds(tm);
            // Order our list randomly
            //Check if it is big enough
            //If not add in Bye Teams
            //2 teams to the power of n ---> 2*2*2*2 ---> 2^4

            //Create our first round of matchups
            //Create every round after that...now we are dividing by two.
            //8 - 4 matchups. 
            //4 -4 winners. 
            //4 - 2 maychups
            //2 - 1 matchup




            //Create the tournament entry
            //Create all of the prizes entries
            //Create all of the team entries
            GlobalConfig.Connection.CreateTournament(tm);

        }
    }
}
