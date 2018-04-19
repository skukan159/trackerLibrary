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
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        BindingList<int> rounds = new BindingList<int>();
        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            WireUpLists();

            LoadFormData();

            LoadRounds();
        }

        private void LoadFormData()
        {
            tournamentName.Text = tournament.TournamentName;
        }

        private void WireUpLists()
        {
            //roundDropdown.DataSource = null;
            roundDropdown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }


        private void LoadRounds()
        {

            rounds.Clear();

            rounds.Add(1);
            int currentRound = 1;

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currentRound)
                {
                    currentRound = matchups.First().MatchupRound;
                    rounds.Add(currentRound);

                }
            }

            LoadMatchups(1);
        }

        private void roundDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropdown.SelectedItem);
        }

        private void LoadMatchups(int round)
        {

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckbox.Checked)
                        {
                            selectedMatchups.Add(m);
                        }
                    }
                }
            }

            if(selectedMatchups.Count > 0)
            {
                LoadAMatchup(selectedMatchups.First());
            }

            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = (selectedMatchups.Count > 0);

            teamOneLabel.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;

            teamTwoLabel.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;

            vsLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
        }

        private void LoadAMatchup(MatchupModel m)
        {
            if(m != null)
            { 
                for (int i = 0; i < m.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        if (m.Entries[0].TeamCompeting != null)
                        {
                            teamOneLabel.Text = m.Entries[0].TeamCompeting.TeamName;
                            teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                            teamTwoLabel.Text = "<bye>";
                            teamTwoScoreValue.Text = "0";
                        }
                        else
                        {
                            teamOneLabel.Text = "Not yet set!";
                            teamOneScoreValue.Text = "";
                        }
                    }

                    if (i == 1)
                    {
                        if (m.Entries[1].TeamCompeting != null)
                        {
                            teamTwoLabel.Text = m.Entries[1].TeamCompeting.TeamName;
                            teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                        }
                        else
                        {
                            teamTwoLabel.Text = "Not yet set!";
                            teamTwoScoreValue.Text = "";
                        }
                    }
                }

            }

            
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAMatchup((MatchupModel)matchupListBox.SelectedItem);
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropdown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            
            // Whichever matchup has been selected, put it into m
            //The ID's
            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;


            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {

                        //double scoreVal = 0;
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please  enter a valid score for team 1");
                            return;
                        }

                    }

                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {

                        //double scoreVal = 0;
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please  enter a valid score for team 2");
                            return;
                        }
                    }
                }
            }

            if(teamOneScore > teamTwoScore)
            {
                //Team one wins
                m.Winner = m.Entries[0].TeamCompeting; //It says team members = 0... wtf?
            }
            else if (teamOneScore < teamTwoScore)
            {
                m.Winner = m.Entries[1].TeamCompeting; //It says team members = 0... wtf?
            }
            else
            {
                MessageBox.Show("I do not handle tied games.");
            }

            LoadMatchups((int)roundDropdown.SelectedItem);

            GlobalConfig.Connection.UpdateMatchup(m);

        }
    }
}
