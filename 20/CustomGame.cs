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
    public partial class CustomGame : Form
    {
        public Team homeTeam;
        public Team awayTeam;
        public bool cancelled;
        public string gameTime;
        public string gameVenue;
        private int currId;

        public CustomGame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.currId = int.MinValue + 1;
            this.homeNameLabel.Text = homeTeam != null ? homeTeam.Name : "None";
            this.awayNameLabel.Text = awayTeam != null ? awayTeam.Name : "None";
        }

        private void gameVenueTextBox_TextChanged(object sender, EventArgs e)
        {
            this.gameVenue = this.gameVenueTextBox.Text;
        }

        private void editHomeButton_Click(object sender, EventArgs e)
        {
            CustomTeam custom = new CustomTeam(this.homeTeam);
            custom.ShowDialog();
            if (custom.cancelled)
            {
                return;
            }
            this.homeTeam = custom.team;
            this.homeTeam.Id = currId++ + "";
            this.homeNameLabel.Text = this.homeTeam == null ? "None" : this.homeTeam.Name;
        }

        private void flipButton_Click(object sender, EventArgs e)
        {
            Team temp = this.homeTeam;
            this.homeTeam = this.awayTeam;
            this.awayTeam = temp;
            this.homeNameLabel.Text = this.homeTeam == null ? "None" : this.homeTeam.Name;
            this.awayNameLabel.Text = this.awayTeam == null ? "None" : this.awayTeam.Name;
        }

        private void editAwayButton_Click(object sender, EventArgs e)
        {
            CustomTeam custom = new CustomTeam(this.awayTeam);
            custom.ShowDialog();
            if (custom.cancelled)
            {
                return;
            }
            this.awayTeam = custom.team;
            this.awayTeam.Id = currId++ + "";
            this.awayNameLabel.Text = this.awayTeam == null ? "None" : this.awayTeam.Name;
        }

        private void useGameButton_Click(object sender, EventArgs e)
        {
            if (homeTeam == null || awayTeam == null)
            {
                MessageBox.Show("You must make two teams", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < homeTeam.Players.Count; i++)
            {
                homeTeam.Players[i].TeamId = homeTeam.Id;
            }

            for (int i = 0; i < awayTeam.Players.Count; i++)
            {
                awayTeam.Players[i].TeamId = awayTeam.Id;
            }
            this.gameTime = DateTime.Now.ToString();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.cancelled = true;
            this.Close();
        }
    }
}
