using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace _20
{
    public partial class LoginForm : Form
    {
        private string username;
        private string password;
        public bool failed { get; set; }

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

        public acceptCredentials login;

        //TODO: Make login threaded to prevent form-lag.
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

                failedLoginLabel.Text = "Attempting login...";
                failedLoginLabel.Visible = true;
                failedLoginLabel.Invalidate();
                this.Refresh();

                login(username, password);

                Close();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.userNameBox.Text = "jnguyenjr";
            this.passwordBox.Text = "Espnalps!";
            submitButton_Click(submitButton, null);
            if (failed)
            {
                failedLoginLabel.Text = "Bad username or password.";
                failedLoginLabel.Visible = true;
                userNameBox.Text = "";
                passwordBox.Text = "";
                userNameBox.Focus();
            }
        }

        private void closeButton_click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
