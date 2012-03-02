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
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        /*
         * Method is executed when the court is clicked */
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            const int imageBorder = 6;
            const float xSize = 1199;
            const float ySize = 716;

            MouseButtons currButton = e.Button;
            Point loc = new Point(e.X, e.Y);

            /* We need to get the location of the click in "ESPN" coordinates.  That is, the top left corner is (0,0)
             * and the bottom right corner is (940, 500). */
            loc.X = (int)((loc.X - imageBorder) / xSize * 940);
            loc.Y = (int)((loc.Y - imageBorder) / ySize * 500);

            Console.WriteLine("Click registered:");
            Console.WriteLine("\t" + loc.ToString());
            Console.WriteLine("\t" + currButton.ToString());
        }

    }
}
