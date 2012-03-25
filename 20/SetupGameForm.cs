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
        public BindingList<Player> homeTeam;
        public BindingList<Player> awayTeam;
        public BindingList<Player> homeStarters;
        public BindingList<Player> awayStarters;
        public string homeTeamName;
        public string awayTeamName;

        public SetupGameForm()
        {
            InitializeComponent();

        }

        private void SetupGameForm_Load(object sender, EventArgs e)
        {
            homeTeamGroupBox.Text = homeTeamName;
            homeStartersLabel.Text = homeTeamName + " starters";
            homeTeamListBox.DataSource = homeTeam;
            homeStartersListBox.DataSource = homeStarters;

            awayTeamGroupBox.Text = awayTeamName;
            awayStartersLabel.Text = awayTeamName + " starters";
            awayTeamListBox.DataSource = awayTeam;
            awayStartersListBox.DataSource = awayStarters;

            unselectListBoxes();
            submitButton.Enabled = false;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Submit lineups?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //this branch runs if the result of the confirmation is "OK"
                this.Close();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void homeTeamListBox_Click(object sender, EventArgs e)
        {
            Player p = (Player)homeTeamListBox.SelectedItem;
            if (p != null && homeStarters.Count < 5)
            {
                homeTeam.Remove(p);
                homeStarters.Add(p);
            }

            unselectListBoxes();
            trySubmitEnable();
        }

        private void awayTeamListBox_Click(object sender, EventArgs e)
        {
            Player p = (Player)awayTeamListBox.SelectedItem;
            if (p != null && awayStarters.Count < 5)
            {
                awayTeam.Remove(p);
                awayStarters.Add(p);
            }

            unselectListBoxes();
            trySubmitEnable();
        }

        private void homeStartersListBox_Click(object sender, EventArgs e)
        {
            Player p = (Player)homeStartersListBox.SelectedItem;
            if (p != null)
            {
                homeTeam.Add(p);
                homeStarters.Remove(p);
            }
            unselectListBoxes();

            trySubmitEnable();
        }

        private void awayStartersListBox_Click(object sender, EventArgs e)
        {
            Player p = (Player)awayStartersListBox.SelectedItem;
            if (p != null)
            {
                awayTeam.Add(p);
                awayStarters.Remove(p);
            }
            unselectListBoxes();

            trySubmitEnable();
        }

        private void trySubmitEnable()
        {

            if (homeStarters.Count == 5 && awayStarters.Count == 5)
            {
                submitButton.Enabled = true;
            }
            else
            {
                submitButton.Enabled = false;
            }
        }

        private void unselectListBoxes()
        {
            homeTeamListBox.ClearSelected();
            awayTeamListBox.ClearSelected();
            homeStartersListBox.ClearSelected();
            awayStartersListBox.ClearSelected();
        }

        
    }
}
