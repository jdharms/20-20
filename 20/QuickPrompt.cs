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
        private Alpaca pac;
        private bool bizzare;
        private Dictionary<string, EventHandler> events;
        public static int sideBorderLength = 6;
        public static int topBottomBorderLength = 28;
        private float fontSize = 15.0f;
        public bool cancelled;
        public string result;

        public QuickPrompt(Alpaca pac, string type, Point loc, bool bizzare)
        {
            InitializeComponent();
            this.pac = pac; 
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
            else if (this.type.Equals("sub"))
            {
                this.Size = new Size(234, 500);
                List<Player> players = null;
                fontSize = 10f;
                if (bizzare)
                {
                    players = pac.HomeTeam.getBench();
                }
                else
                {
                    players = pac.AwayTeam.getBench();
                }
                string[] pArray = new string[players.Count-1];
                int count = 0;
                
                foreach(Player p in players)
                {
                    if (!p.TeamPlayer)
                    {
                        pArray[count++] = "with #" + p.Jersey + " (" + p.DisplayName + ")";
                    }
                }
                this.loadFormWithButtons(pArray);
            }
            else if (this.type.Equals("timeout"))
            {
                this.Text = "Select timeout type";
                loadFormWithButtons(new string[] {(bizzare ? "Home" : "Away") + " Timeout", "Media Timeout", "Official Timeout" });
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
                font = new Font(b.Font.FontFamily, fontSize);
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
