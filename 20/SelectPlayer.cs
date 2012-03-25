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
    public partial class SelectPlayer : Form
    {
        public List<Player> home, away;

        public Player selected;

        public SelectPlayer()
        {
            InitializeComponent();
        }

        private void SelectPlayer_Load(object sender, EventArgs e)
        {
            button1.DataBindings.Add("Text", home[0], "DisplayName");
            button2.DataBindings.Add("Text", home[1], "DisplayName");
            button3.DataBindings.Add("Text", home[2], "DisplayName");
            button4.DataBindings.Add("Text", home[3], "DisplayName");
            button5.DataBindings.Add("Text", home[4], "DisplayName");
            button6.DataBindings.Add("Text", away[0], "DisplayName");
            button7.DataBindings.Add("Text", away[1], "DisplayName");
            button8.DataBindings.Add("Text", away[2], "DisplayName");
            button9.DataBindings.Add("Text", away[3], "DisplayName");
            button10.DataBindings.Add("Text", away[4], "DisplayName");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selected = home[0];
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selected = home[1];
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selected = home[2];
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selected = home[3];
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selected = home[4];
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            selected = away[0];
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            selected = away[1];
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            selected = away[2];
            Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            selected = away[3];
            Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            selected = away[4];
            Close();
        }
    }
}
