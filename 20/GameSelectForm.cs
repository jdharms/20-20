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
                selected = true;
                selectedGameId = ((Game)gameListBox.SelectedItem).gameId;
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
            games = getGames(from, to);
            if (games == null)
            {
                return;
            }
            gameListBox.DataSource = games;
            gameListBox.Refresh();
            gameListBox.Invalidate();
            gameListBox.SetSelected(0, true);
            selectGameButton.Focus();
        }

        private void GameSelectForm_Load(object sender, EventArgs e)
        {

        }
    }
}
