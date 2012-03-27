﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _20
{
    public partial class DataForm : Form
    {
        string type;
        public static int SHOT_TYPE = 0;
        public static int FASTBREAK = 1;
        public static int GOALTENDING = 2;

        int sideBorderLength = 6;
        int topBottomBorderLength = 28;
        public string shotType;
        public bool cancelled;
        public bool goaltending;
        public bool fastbreak;
        private int iteration;
        private Dictionary<string, EventHandler> events;

        public DataForm(string type, int start)
        {
            InitializeComponent();
            this.type = type;
            this.iteration = start;
            this.cancelled = false;
            events = new Dictionary<string, EventHandler>();
            events["madeShot"] = new EventHandler(this.madeShot_Click);
            events["missedShot"] = new EventHandler(this.missedShot_Click);
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
            if (type.Equals("madeShot"))
            {
                if (this.iteration == SHOT_TYPE)
                {
                    this.Text = "Shot Type";
                    loadFormWithButtons(new string[] { "JUMP SHOT", "LAYUP", "DUNK", "TIP IN" });
                }
                else if (this.iteration == FASTBREAK)
                {
                    this.Text = "Fastbreak?";
                    loadFormWithButtons(new string[] { "Yes", "No" });
                }
                else if (this.iteration == GOALTENDING)
                {
                    this.Text = "Goaltending?";
                    loadFormWithButtons(new string[] { "Yes", "No" });
                }
            }
            else if (type.Equals("missedShot"))
            {
                if (this.iteration == SHOT_TYPE)
                {
                    this.Text = "Shot Type";
                    loadFormWithButtons(new string[] { "JUMP SHOT", "LAYUP", "DUNK", "TIP IN"});
                }
                else if (this.iteration == FASTBREAK)
                {
                    this.Text = "Fastbreak?";
                    loadFormWithButtons(new string[] { "Yes", "No" });
                }
            }
            
        }

        private void loadFormWithButtons(string[] types)
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
                b.Click += i == cancelInd ? new EventHandler(this.cancelForm) : events[type];
                font = new Font(b.Font.FontFamily, 20.0f);
                b.Font = font;
                location += b.Size.Height;
                this.Controls.Add(b);
            }
            this.Refresh();
            this.Invalidate();
        }

        private void madeShot_Click(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            if (iteration == SHOT_TYPE)
            {
                string lower = button.Text.ToLower();
                if(lower.IndexOf(" ") >= 0)
                {
                    lower = lower.Equals("tip in") ? "tip-in" : "jump-shot";
                }
                this.shotType = lower;
                loadFormWithButtons(new string[] { "Yes", "No" });
                this.Text = "Fastbreak?";
                iteration++;
            }
            else if (iteration == FASTBREAK)
            {
                this.fastbreak = button.Text.Equals("Yes");
                loadFormWithButtons(new string[] { "Yes", "No" });
                this.Text = "Goaltending?";
                iteration++;
            }
            else if (iteration == GOALTENDING)
            {
                this.goaltending = button.Text.Equals("Yes");
                this.Close();
            }

        }

        private void missedShot_Click(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            if (iteration == SHOT_TYPE)
            {
                string lower = button.Text.ToLower();
                if(lower.IndexOf(" ") >= 0)
                {
                    lower = lower.Equals("tip in") ? "tip-in" : "jump-shot";
                }
                this.shotType = lower;
                loadFormWithButtons(new string[] { "Yes", "No" });
                this.Text = "Fastbreak?";
                iteration++;
            }
            else if (iteration == FASTBREAK)
            {
                this.fastbreak = button.Text.Equals("Yes");
                loadFormWithButtons(new string[] { "Yes", "No" });
                this.Close();
            }
        }

        public void cancelForm(object sender, EventArgs e)
        {
            this.cancelled = true;
            this.Close();
        }
    }
}
