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
        public ListBox gameBox;

        public GameSelectForm()
        {
            InitializeComponent();
            gameBox = this.gameListBox;
            gameBox.DataSource = new List<Game>();
        }

        private void searchGamesButton_click(object sender, EventArgs e)
        {
            from = fromDatePicker.Value;
            to = toDatePicker.Value;
            Close();
        }

        private void selectGameButton_Click(object sender, EventArgs e)
        {
            selected = true;
            selectedGameId = ((Game) gameBox.SelectedItem).gameId;
            Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
