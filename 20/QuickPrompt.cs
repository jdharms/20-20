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
    public partial class QuickPrompt : Form
    {
        private string type;
        private bool bizzare;
        private Dictionary<string, EventHandler> events;
        public static int sideBorderLength = 6;
        public static int topBottomBorderLength = 28;
        public bool cancelled;
        public string result;

        public QuickPrompt(Alpaca pac, string type, Point loc, bool bizzare)
        {
            InitializeComponent();
            this.type = type;
            this.bizzare = bizzare;
            this.Location = loc;
        }

        private void QuickPrompt_Load(object sender, EventArgs e)
        {
            if(this.type.Equals("Jump Ball"))
            {
                this.Text = "Jumpball -- Select winner";
                this.loadFormWithButtons(new string[] {"Home Possession", "Away Possesion"});
            }
            else if(this.type.Equals("Made Shot"))
            {
                if (this.bizzare)
                {
                    this.Text = "Goaltending -- Select point amount";
                    this.loadFormWithButtons(new string[] { "1", "2", "3" });
                }
                else
                {
                    this.Text = "Made Shot -- Select point amount";
                    this.loadFormWithButtons(new string[] { "1", "2", "3", "Goaltending" });
                }
            }
            else if(this.type.Equals("Missed Shot"))
            {
                this.Text = "Missed Shot -- Select point amount";
                this.loadFormWithButtons(new string[] { "1", "2", "3" });
            }
            else if(this.type.Equals("Foul"))
            {
                if (this.bizzare)
                {
                    this.Text = "Ejection -- Select foul type";
                    this.loadFormWithButtons(new string[] { "Offensive", "Defensive", "Technical"});
                }
                else
                {
                    this.Text = "Foul -- Select foul type";
                    this.loadFormWithButtons(new string[] { "Offensive", "Defensive", "Technical", "Ejection"});
                }
            }
        }

        public void loadFormWithButtons(string[] types)
        {
            this.Controls.Clear();
            int location = 0;
            int cancelInd = types.Count<string>();
            Font font = null;
            for (int i = 0; i < cancelInd + 1; i++)
            {
                Button b = new Button();
                b.Text = i == cancelInd ? "Cancel" : types[i];
                b.Size = new Size((this.Width - sideBorderLength), (this.Height - topBottomBorderLength) / (types.Count<string>() + 1));
                b.Location = new Point(0, location);
                b.Click += i == cancelInd ? new EventHandler(this.cancelForm) : new EventHandler(this.allPurposeButtonClick);
                font = new Font(b.Font.FontFamily, 15.0f);
                b.Font = font;
                location += b.Size.Height;
                this.Controls.Add(b);
            }
            this.Refresh();
            this.Invalidate();
        }

        private void allPurposeButtonClick(object sender, EventArgs e)
        {
            this.result = ((Button)sender).Text;
            this.Close();
        }

        public void cancelForm(object sender, EventArgs e)
        {
            this.cancelled = true;
            this.Close();
        }
    }
}
