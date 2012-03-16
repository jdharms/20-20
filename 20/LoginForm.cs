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
    public partial class LoginForm : Form
    {
        private string username;
        private string password;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (userNameBox.TextLength == 0 || passwordBox.TextLength == 0)
            {
                MessageBox.Show("Missing username or password!");
            }
            else
            {
                username = userNameBox.Text;
                password = passwordBox.Text;

                Close();
            }
        }
    }
}
