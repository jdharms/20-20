using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _20
{
    public partial class SetupGameForm : Form
    {
        public List<Player> homeTeam;
        public List<Player> awayTeam;
        public List<Player> homeStarters;
        public List<Player> awayStarters;
        public string homeTeamName;
        public string awayTeamName;

        public SetupGameForm()
        {
            InitializeComponent();
            homeTeamGroupBox.Text = homeTeamName;
            homeStartersLabel.Text = homeTeamName + " starters";
            homeTeamListBox.DataSource = homeTeam;
            homeStartersListBox.DataSource = homeStarters;

            awayTeamGroupBox.Text = awayTeamName;
            awayStartersLabel.Text = awayTeamName + " starters";
            awayTeamListBox.DataSource = awayTeam;
            awayStartersListBox.DataSource = awayStarters;

            submitButton.Visible = false;
        }

        private void homeTeamListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player p = (Player)homeTeamListBox.SelectedItem;
            if (homeStarters.Count < 5)
            {
                homeStarters.Add(p);
                homeTeam.Remove(p);
            }

            trySubmitEnable();
        }

        private void awayTeamListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player p = (Player)awayTeamListBox.SelectedItem;
            if (awayStarters.Count < 5)
            {
                awayStarters.Add(p);
                awayTeam.Remove(p);
            }

            trySubmitEnable();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void homeStartersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player p = (Player)homeStartersListBox.SelectedItem;
            homeTeam.Add(p);
            homeStarters.Remove(p);

            trySubmitEnable();
        }

        private void awayStartersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player p = (Player)awayStartersListBox.SelectedItem;
            awayTeam.Add(p);
            awayStarters.Remove(p);

            trySubmitEnable();
        }

        private void trySubmitEnable()
        {
            if (homeStarters.Count == 5 && awayStarters.Count == 5)
            {
                submitButton.Visible = true;
            }
        }
    }
}
