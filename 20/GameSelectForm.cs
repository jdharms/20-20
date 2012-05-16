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
    public partial class GameSelectForm : Form
    {
        public bool selected = false;
        public string selectedGameId = "";
        public string gameVenue;
        public string gameTime;
        public DateTime from;
        public DateTime to;
        public List<Game> games;

        public GameGetter getGames;

        public GameSelectForm()
        {
            InitializeComponent();
            gameListBox.DataSource = games;
            fromDatePicker.Value = new DateTime(2012, 1, 1);
            toDatePicker.Value = new DateTime(2012, 4, 1);
        }

        private void searchGamesButton_click(object sender, EventArgs e)
        {
            from = fromDatePicker.Value;
            to = toDatePicker.Value.AddDays(1);

            searchGames(from, to);
        }

        private void selectGameButton_Click(object sender, EventArgs e)
        {
            if ((Game)gameListBox.SelectedItem != null)
            {
                Game game = ((Game)gameListBox.SelectedItem);
                selected = true;
                selectedGameId = game.gameId;
                gameVenue = game.venue;
                gameTime = game.time;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a game"); 
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void searchTodayButton_Click(object sender, EventArgs e)
        {
            from = DateTime.Today;
            to = from.AddDays(1);
            searchGames(from, to);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData);
            if (keyData == Keys.Enter)
            {
                selectGameButton_Click(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void searchGames(DateTime from, DateTime to)
        {
            if (from.CompareTo(to) > 0)
            {
                MessageBox.Show("From date must be before the to date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            games = getGames(from, to);
            if (games == null)
            {
                return;
            }
            if (games.Count == 0)
            {
                MessageBox.Show("There are no games between " + from.ToString() + " and " + to.ToString() + ".", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            gameListBox.DataSource = games;
            gameListBox.Refresh();
            gameListBox.Invalidate();
            selectGameButton.Focus();
        }

        private void GameSelectForm_Load(object sender, EventArgs e)
        {
        }

        public CustomGame customGame;
        public bool customGameSelected;
        private void button1_Click(object sender, EventArgs e)
        {
            this.customGame = new CustomGame();
            this.customGame.ShowDialog();
            if (this.customGame.cancelled)
            {
                return;
            }

            this.gameTime = this.customGame.gameTime;
            this.gameVenue = this.customGame.gameVenue;
            this.customGameSelected = true;
            this.Close();
        }
    }
}
