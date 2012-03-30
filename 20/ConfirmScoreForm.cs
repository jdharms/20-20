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
    public partial class ConfirmScoreForm : Form
    {
        public int homeScore;
        public int awayScore;
        private Alpaca pac;

        public ConfirmScoreForm(Alpaca pac, bool endGame)
        {
            InitializeComponent();
            if (endGame)
            {
                this.Text = "Final Game Results";
                this.confirmScoreButton.Text = "Confirm Final Score";
            }
            this.pac = pac;
        }

        private void confirmScoreButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.homeScore = int.Parse(homeScoreConfirmText.Text);
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Home Score Must Be A Number", "", MessageBoxButtons.OKCancel);
                this.homeScoreConfirmText.Text = pac.HomeTeam.Score + "";
                return;
            }
            try
            {
                this.awayScore = int.Parse(awayScoreConfirmText.Text);
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Away Score Must Be A Number", "", MessageBoxButtons.OKCancel);
                this.awayScoreConfirmText.Text = pac.AwayTeam.Score + "";
                return; 
            }
        }

        private void ConfirmScoreForm_Load(object sender, EventArgs e)
        {
            this.homeConfirmLabel.Text = pac.HomeTeam.Name;
            this.awayConfirmLabel.Text = pac.AwayTeam.Name;
            this.homeScoreConfirmText.Text = "" + pac.HomeTeam.Score;
            this.awayScoreConfirmText.Text = "" + pac.AwayTeam.Score;
        }
    }
}
