using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _20.Events;
using System.Threading;

namespace _20
{
    public partial class GameForm : Form
    {
        Alpaca pac;
        Point currPoint;
        private bool pointSelected;
        private List<Label> homePlayerLabels;
        private List<Label> awayPlayerLabels;
        private List<GroupBox> homePlayerContexts;
        private List<GroupBox> awayPlayerContexts;
        private Player firstSelectedPlayer;
        private GroupBox firstSelectedContext;
        private Label firstSelectedLabel;
        private Player secondSelectedPlayer;
        private GroupBox secondSelectedContext;
        private Label secondSelectedLabel;
        private bool homeRightClicked;

        public GameForm()
        {
            InitializeComponent();
            homePlayerLabels = new List<Label>();
            homePlayerLabels.Add(homePlayer1Label);
            homePlayerLabels.Add(homePlayer2Label);
            homePlayerLabels.Add(homePlayer3Label);
            homePlayerLabels.Add(homePlayer4Label);
            homePlayerLabels.Add(homePlayer5Label);

            homePlayerContexts = new List<GroupBox>();
            homePlayerContexts.Add(homePlayer1Context);
            homePlayerContexts.Add(homePlayer2Context);
            homePlayerContexts.Add(homePlayer3Context);
            homePlayerContexts.Add(homePlayer4Context);
            homePlayerContexts.Add(homePlayer5Context);

            awayPlayerLabels = new List<Label>();
            awayPlayerLabels.Add(awayPlayer1Label);
            awayPlayerLabels.Add(awayPlayer2Label);
            awayPlayerLabels.Add(awayPlayer3Label);
            awayPlayerLabels.Add(awayPlayer4Label);
            awayPlayerLabels.Add(awayPlayer5Label);

            awayPlayerContexts = new List<GroupBox>();
            awayPlayerContexts.Add(awayPlayer1Context);
            awayPlayerContexts.Add(awayPlayer2Context);
            awayPlayerContexts.Add(awayPlayer3Context);
            awayPlayerContexts.Add(awayPlayer4Context);
            awayPlayerContexts.Add(awayPlayer5Context);

            jumpBallContextMenuStrip.Items.Clear();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            pac = new Alpaca();
            pac.OnStateChange += update;
            //GameDataResponse gameData = pac.getGameData(pac.GameID);
            for (int i = 0; i < awayPlayerLabels.Count; i++)
            {
                homePlayerLabels[i].ContextMenuStrip = subContextMenuStrip;
                awayPlayerLabels[i].ContextMenuStrip = subContextMenuStrip;
            }
            jumpBallContextMenuStrip.Items.Add("Possession to Home Team (" + pac.HomeTeam.Name + ")");
            jumpBallContextMenuStrip.Items.Add("Possession to Away Team (" + pac.AwayTeam.Name + ")");
            jumpBallContextMenuStrip.Items[0].Click += new EventHandler(this.jumpballSelect_Click);
            jumpBallContextMenuStrip.Items[1].Click += new EventHandler(this.jumpballSelect_Click);
            update();
        }

        void update()
        {
            //populate all form controls.    
            Console.WriteLine("Updating form...");

            homeNameLabel.Text = pac.HomeTeam.Name;
            awayNameLabel.Text = pac.AwayTeam.Name;

            homeTimeoutLabel.Text = "T.O. Left: " + pac.HomeTeam.TimeoutsLeft;
            awayTimeoutLabel.Text = "T.O. Left: " + pac.AwayTeam.TimeoutsLeft;

            homeScore.Text = pac.HomeTeam.Score.ToString();
            awayScore.Text = pac.AwayTeam.Score.ToString();

            historyBox.DataSource = pac.EventLog;
            ((CurrencyManager)historyBox.BindingContext[pac.EventLog]).Refresh();

            foreach (Control c in this.Controls)
            {
                if (c != historyBox && c != deleteEventButton)
                {
                    c.Enabled = !pac.GameEnded;
                }
            }

            List<Player> homeOnCourt = pac.HomeTeam.getOncourt();
            for (int i = 0; i < homeOnCourt.Count; i++)
            {
                homePlayerLabels[i].Text = homeOnCourt[i].Jersey + "";
                toolTip1.SetToolTip(homePlayerLabels[i], homeOnCourt[i].DisplayName);
            }

            List<Player> awayOnCourt = pac.AwayTeam.getOncourt();
            for (int i = 0; i < awayOnCourt.Count; i++)
            {
                awayPlayerLabels[i].Text = awayOnCourt[i].Jersey + "";
                toolTip1.SetToolTip(awayPlayerLabels[i], awayOnCourt[i].DisplayName);
            }
            string perOrOver = pac.Period > 2 ? "Overtime" : "Period";

            if (!pac.InsidePeriod)
            {
                periodStartButton.Text = perOrOver + " Start";
            }
            else
            {
                periodStartButton.Text = perOrOver + " End";
            }

            firstSelectedPlayer = secondSelectedPlayer = null;
            firstSelectedLabel = secondSelectedLabel = null;
            firstSelectedContext = secondSelectedContext = null;

            for (int i = 0; i < homePlayerContexts.Count; i++)
            {
                homePlayerContexts[i].Text = "";
                awayPlayerContexts[i].Text = "";
                homePlayerLabels[i].ForeColor = Color.Black;
                awayPlayerLabels[i].ForeColor = Color.Black;
            }

            if (!pointSelected)
            {
                courtBox.Refresh();
            }

            this.Invalidate();
        }

        private void courtBox_MouseDown(object sender, MouseEventArgs e)
        {
            courtBox.Refresh();
            const int imageBorder = 2;

            MouseButtons currButton = e.Button;
            Point loc = new Point(e.X, e.Y);

            /* We need to get the location of the click in "ESPN" coordinates.  That is, the top left corner is (0,0)
             * and the bottom right corner is (940, 500). */

            /* This is close enough for now... later we can make a better rescale of the image and perfect this algorithm */

            loc.X = (int)(loc.X - imageBorder);
            loc.Y = (int)(loc.Y - imageBorder);

            if (loc.X < 0) loc.X = 0;
            if (loc.Y < 0) loc.Y = 0;

            currPoint = loc;
            pointSelected = true;

            Graphics g = courtBox.CreateGraphics();
            using (Pen p = new Pen(Color.Red, 4))
            {
                g.DrawEllipse(p, e.X - 5, e.Y - 5, 10, 10);
            }
        }

        private void historyBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ////The selected value "changes" even when it goes to having no selected value.
            ////This only shows the delete button if the selected value is an actual event,
            ////and hides the box when the new selection is nothing.
            //if (historyBox.SelectedItem != null)
            //{
            //    deleteEventButton.Visible = true;
            //}
            //else
            //{
            //    deleteEventButton.Visible = false;
            //}

        }

        private void historyBox_Leave(object sender, EventArgs e)
        {
            //When we leave the box, clear the selected value so no accidental deletions are possible.
            if (ActiveControl != deleteEventButton)
            {
                historyBox.ClearSelected();
            }
        }

        private void deleteEventButton_Click(object sender, EventArgs e)
        {
            //requests user to confirm deletion of event.
            if (MessageBox.Show("Really Delete?", "Confirm Delete.", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //this branch runs if the result of the confirmation is "OK"
                Console.WriteLine("Deleting...");

                DeleteEvent del = new DeleteEvent(pac, pac.EventLog[pac.EventLog.Count - 1]);
                pac.post(del);
            }
            else
            {
                //this branch executes if the user says "Cancel".
                Console.WriteLine("Not deleting...");
                historyBox.ClearSelected();
            }
        }

        /* 
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to the rebound button.
         */
        private void reboundButton_Click(object sender, EventArgs e)
        {
            //Ask for location
            //Ask for team
            //Ask for rebound type ("offensive", "defensive", "dead-ball", "team-offensive", or "team-defensive")
            //Ask for player (optional except dead-ball rebounds must have player.)

            //send rebound event to server
        }

        /*
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to the turnover button.
         */
        private void turnoverButton_Click(object sender, EventArgs e)
        {
            //Ask for location
            //Ask for team
            //Ask for optional player on opposing team that forced turnover.
            //Ask for turnover type ( "traveling", "lost-ball", "offensive-foul", "out-of-bounds", "violation", "offensive-goaltending" or "thrown-away")

            //send turnover event to server
        }

        /*
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to the period start/end button
         */
        private void periodStartButton_Click(object sender, EventArgs e)
        {
            Event ev = null;
            if (pac.Period >= 2 && (pac.HomeTeam.Score != pac.AwayTeam.Score) && pac.InsidePeriod)
            {
                ev = new PeriodEndEvent(pac);
                pac.post(ev);
                ev = new GameEndEvent(pac);
                pac.post(ev);
                return;
            }
            else
            {
                //Determine if we're entering/exiting a period,
                //and which period.
                Button button = (Button)sender;
                string perOrOver = pac.Period > 2 ? "Overtime" : "Period";
                if (button.Text.Contains("Start"))
                {
                    ev = new PeriodStartEvent(pac);

                    button.Text = perOrOver + " End";
                }
                else
                {
                    ev = new PeriodEndEvent(pac);
                    button.Text = perOrOver + " Start";
                }
            }

            //send appropriate event to server
            pac.post(ev);

        }

        /*
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to the foul button
         */
        private void foulButton_Click(object sender, EventArgs e)
        {
            //Ask for location
            //ask for fouled player
            //ask for committing player
            //ask for type of foul
            //ejected?

            //send foul event
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void historyBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (pac != null && pac.EventLog != null)
            {
                int index = historyBox.IndexFromPoint(e.Location);

                if (index != -1 && index < pac.EventLog.Count)
                {
                    if (toolTip1.GetToolTip(historyBox) != pac.EventLog[index].ToString())
                    {
                        toolTip1.SetToolTip(historyBox, pac.EventLog[index].ToString());
                    }
                }
                else
                {
                    toolTip1.SetToolTip(historyBox, "");
                }
            }
        }

        private void playerSelect_click(object sender, EventArgs e)
        {
            List<Player> homeOnCourt = pac.HomeTeam.getOncourt();
            List<Player> awayOnCourt = pac.AwayTeam.getOncourt();
            Player thisSelected = null;
            int i = -1;
            bool isHome = false;
            for (i = 0; i < homePlayerLabels.Count; i++)
            {
                if (sender == homePlayerLabels[i] || sender == homePlayerContexts[i])
                {
                    thisSelected = homeOnCourt[i];
                    isHome = true;
                    break;
                }
                else if (sender == awayPlayerLabels[i] || sender == awayPlayerContexts[i])
                {
                    thisSelected = awayOnCourt[i];
                    isHome = false;
                    break;
                }
            }

            // if firstSelectedPlayer is null, then we have not set this and the second player
            if (firstSelectedPlayer == null)
            {
                //set this current selection
                firstSelectedPlayer = thisSelected;
                if (isHome)
                {
                    firstSelectedContext = homePlayerContexts[i];
                    firstSelectedLabel = homePlayerLabels[i];
                }
                else
                {
                    firstSelectedContext = awayPlayerContexts[i];
                    firstSelectedLabel = awayPlayerLabels[i];
                }

                firstSelectedLabel.ForeColor = Color.Red;
                firstSelectedContext.Text = "1st";
            }
            // the first player was selected, so check if we are unclicking it
            else if (firstSelectedPlayer == thisSelected)
            {
                //unselect everything
                firstSelectedPlayer = null;
                secondSelectedPlayer = null;

                if (firstSelectedContext != null)
                {
                    firstSelectedLabel.ForeColor = Color.Black;
                    firstSelectedContext.Text = "";
                    firstSelectedLabel = null;
                    firstSelectedContext = null;
                }

                if (secondSelectedContext != null)
                {
                    secondSelectedLabel.ForeColor = Color.Black;
                    secondSelectedContext.Text = "";
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }
            }
            //the first player has been selected, we are not unclicking the first, and the second has not been set
            else if (secondSelectedPlayer == null)
            {
                secondSelectedPlayer = thisSelected;
                if (isHome)
                {
                    secondSelectedLabel = homePlayerLabels[i];
                    secondSelectedContext = homePlayerContexts[i];
                }
                else
                {
                    secondSelectedLabel = awayPlayerLabels[i];
                    secondSelectedContext = awayPlayerContexts[i];
                }

                secondSelectedLabel.ForeColor = Color.Green;
                secondSelectedContext.Text = "2nd";
            }
            // we have selected the second player and we are unclicking it
            else if (secondSelectedPlayer == thisSelected)
            {
                secondSelectedPlayer = null;
                if (secondSelectedContext != null)
                {
                    secondSelectedLabel.ForeColor = Color.Black;
                    secondSelectedContext.Text = "";
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }

            }
            else
            {
                //undo previous second
                if (secondSelectedContext != null)
                {
                    secondSelectedLabel.ForeColor = Color.Black;
                    secondSelectedContext.Text = "";
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }

                secondSelectedPlayer = thisSelected;

                //applly to new selected
                if (isHome)
                {
                    secondSelectedLabel = homePlayerLabels[i];
                    secondSelectedContext = homePlayerContexts[i];
                }
                else
                {
                    secondSelectedLabel = awayPlayerLabels[i];
                    secondSelectedContext = awayPlayerContexts[i];
                }

                secondSelectedLabel.ForeColor = Color.Green;
                secondSelectedContext.Text = "2nd";
            }

            if (firstSelectedContext != null)
            {
                firstSelectedContext.Text = "1st";
            }
            if (secondSelectedContext != null)
            {
                secondSelectedContext.Text = "2nd";
            }


            this.Refresh();
            this.Invalidate();
        }

        private void playerSelect_MouseDown(object sender, MouseEventArgs e)
        {
            MouseButtons currButton = e.Button;
            if (currButton == System.Windows.Forms.MouseButtons.Right)
            {

                subContextMenuStrip.Items.Clear();
                List<Player> onCourt = null;
                List<Player> bench = null;
                Player senderPlayer = null;
                int senderNumber = int.Parse(((Label)sender).Text);

                if ((sender is Label && homePlayerLabels.Contains((Label)sender)))
                {
                    onCourt = pac.HomeTeam.getOncourt();
                    bench = pac.HomeTeam.getBench();
                    senderPlayer = pac.getPlayerByNumber(true, senderNumber);
                    homeRightClicked = true;
                }
                else
                {
                    onCourt = pac.AwayTeam.getOncourt();
                    bench = pac.AwayTeam.getBench();
                    senderPlayer = pac.getPlayerByNumber(false, senderNumber);
                    homeRightClicked = false;
                }

                ToolStripMenuItem subInItem = new ToolStripMenuItem("Sub out #" + senderNumber + " (" + senderPlayer.DisplayName + ")");
                ToolStripMenuItem[] playerMenu = new ToolStripMenuItem[bench.Count - 1];
                int toolStripInd = 0;

                for (int i = 0; i < bench.Count; i++)
                {
                    if (!bench[i].TeamPlayer)
                    {
                        playerMenu[toolStripInd] = new ToolStripMenuItem("with #" + bench[i].Jersey + " (" + bench[i].DisplayName + ")");
                        playerMenu[toolStripInd].Click += new EventHandler(subPlayer_click);
                        toolStripInd++;
                    }
                }

                subInItem.DropDownItems.AddRange(playerMenu);

                subContextMenuStrip.Items.Add(subInItem);
            }
        }

        private void subPlayer_click(object sender, EventArgs e)
        {
            ToolStripMenuItem subInItem = (ToolStripMenuItem)sender;
            string subInItemText = subInItem.Text;
            ToolStripMenuItem subOutItem = (ToolStripMenuItem)subInItem.OwnerItem;
            string subOutItemText = subOutItem.Text;

            int inBeg = subInItemText.IndexOf("#") + 1;
            int inLength = subInItemText.IndexOf(" (") - inBeg;
            int outBeg = subOutItemText.IndexOf("#") + 1;
            int outLength = subOutItemText.IndexOf(" (") - outBeg;

            int subInNumber = int.Parse(subInItemText.Substring(inBeg, inLength));
            int subOutNumber = int.Parse(subOutItemText.Substring(outBeg, outLength));

            Player subInPlayer = null;
            Player subOutPlayer = null;

            subInPlayer = pac.getPlayerByNumber(homeRightClicked, subInNumber);
            subOutPlayer = pac.getPlayerByNumber(homeRightClicked, subOutNumber);

            SubstitutionEvent subEvent = new SubstitutionEvent(pac, subInPlayer.Id, subOutPlayer.Id, subInPlayer.TeamId);

            if (MessageBox.Show("Sub in " + subInPlayer.DisplayName + " for " + subOutPlayer.DisplayName + "?",
                "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                pac.post(subEvent);
            }
        }

        private void timeout_Click(object sender, EventArgs e)
        {

            TimeoutEvent timeoutEvent = null;
            if (sender.ToString().Equals("Home Timeout"))
            {
                timeoutEvent = new TimeoutEvent(pac, pac.HomeTeam.Id, "team");
            }
            else if (sender.ToString().Equals("Away Timeout"))
            {
                Console.WriteLine("Away Timeout");
                timeoutEvent = new TimeoutEvent(pac, pac.AwayTeam.Id, "team");
            }
            else if (sender.ToString().Equals("Media Timeout"))
            {
                timeoutEvent = new TimeoutEvent(pac, null, "media");
            }
            else if (sender.ToString().Equals("Official Timeout"))
            {
                timeoutEvent = new TimeoutEvent(pac, null, "official");
            }

            if (MessageBox.Show("Call " + sender.ToString() + "?",
                "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                pac.post(timeoutEvent);
            }
        }

        private void madeShotButton_Click(object sender, EventArgs e)
        {

        }

        private void missedShotButton_Click(object sender, EventArgs e)
        {

        }

        private void jumpBallButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                if (firstSelectedPlayer == null || secondSelectedPlayer == null)
                {
                    MessageBox.Show("Please select one Home player and one Away player above");
                    return;
                }
                else if (firstSelectedPlayer.TeamId == secondSelectedPlayer.TeamId)
                {
                    MessageBox.Show("Selected players must be on different teams");
                    return;
                }
                else if (!pointSelected)
                {
                    MessageBox.Show("Please select a location on the court");
                    return;
                }
                else
                {
                    firstSelectedContext.Text = "Jump";
                    secondSelectedContext.Text = "Jump";
                }
                Button btnSender = (Button)sender;
                jumpBallContextMenuStrip.Show(this, btnSender.Location);

            }
        }

        private void jumpballSelect_Click(object sender, EventArgs e)
        {
            //requests user to confirm deletion of event.
            if (MessageBox.Show(sender + "?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string strSender = sender.ToString();
                JumpballEvent jbe = null;
                Player awayPlayer = null;
                Player homePlayer = null;

                //if the first player is the home team
                if (firstSelectedPlayer.TeamId == pac.HomeTeam.Id)
                {
                    homePlayer = firstSelectedPlayer;
                    awayPlayer = secondSelectedPlayer;
                }
                else
                {
                    homePlayer = secondSelectedPlayer;
                    awayPlayer = firstSelectedPlayer;
                }

                if (strSender.Contains("Home"))
                {
                    jbe = new JumpballEvent(pac, homePlayer.Id, awayPlayer.Id, homePlayer.Id, currPoint);
                }
                else
                {
                    jbe = new JumpballEvent(pac, homePlayer.Id, awayPlayer.Id, awayPlayer.Id, currPoint);
                }

                pointSelected = false;

                pac.post(jbe);
            }
        }

        private void madeShotButton_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                if (firstSelectedPlayer == null)
                {
                    MessageBox.Show("Please select at least one player above");
                    return;
                }
                else if (secondSelectedPlayer != null && firstSelectedPlayer.TeamId != secondSelectedPlayer.TeamId)
                {
                    MessageBox.Show("Selected players must be on the same team");
                    return;
                }
                else if (!pointSelected)
                {
                    MessageBox.Show("Please select a location on the court");
                    return;
                }
                else
                {
                    firstSelectedContext.Text = "Shooter";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "Assist";
                }
                Button btnSender = (Button)sender;
                madeShotContextMenuStrip.Show(this, btnSender.Location);
            }
        }

        private void missedShotButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (firstSelectedPlayer == null)
                {
                    MessageBox.Show("Please select at least one player above");
                    return;
                }
                else if (secondSelectedPlayer != null && firstSelectedPlayer.TeamId == secondSelectedPlayer.TeamId)
                {
                    MessageBox.Show("Selected players must be on different teams");
                    return;
                }
                else if (!pointSelected)
                {
                    MessageBox.Show("Please select a location on the court");
                    return;
                }
                else
                {
                    firstSelectedContext.Text = "Shooter";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "Blocker";
                }
                Button btnSender = (Button)sender;
                missedShotContextMenuStrip.Show(this, ((Button)sender).Location);
            }
        }

        private void reboundButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (firstSelectedPlayer == null)
                {
                    MessageBox.Show("Please select only ONE player above");
                    return;
                }
                else if (!pointSelected)
                {
                    MessageBox.Show("Please select a location on the court");
                    return;
                }
                else
                {
                    firstSelectedContext.Text = "Reb.";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "";
                }
                Button btnSender = (Button)sender;
                reboundContextMenuStrip.Show(this, ((Button)sender).Location);
            }
        }

        private void pointScored_Click(object sender, EventArgs e)
        {
            MadeShotEvent mse = null;
            string str = sender.ToString();
            string assistId = secondSelectedPlayer != null ? secondSelectedPlayer.Id : null;
            //HAS TO BE A FREE THROW!!
            if (str.Equals("1"))
            {
                DataForm dataForm = new DataForm("madeShot", DataForm.GOALTENDING);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }
                mse = new MadeShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, null, 
                                        "free-throw", 1, false, dataForm.goaltending, currPoint);
            }
            // can be a jumpshot, layup, dunk, tip-in
            else if (str.Equals("2"))
            {
                DataForm dataForm = new DataForm("madeShot", DataForm.SHOT_TYPE);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }
                mse = new MadeShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, assistId, 
                                        dataForm.shotType, 2, dataForm.fastbreak, dataForm.goaltending, currPoint);
            }
            else if (str.Equals("3"))
            {
                DataForm dataForm = new DataForm("madeShot", DataForm.FASTBREAK);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                mse = new MadeShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, assistId, 
                                        "jump-shot", 3, dataForm.fastbreak, dataForm.goaltending, currPoint);
            }

            if (MessageBox.Show("Send event: " + mse, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                pointSelected = false;
                pac.post(mse);
            }

        }

        private void pointMissed_Click(object sender, EventArgs e)
        {
            MissedShotEvent mse = null;
            string str = sender.ToString();
            Console.WriteLine("HEY: " + str);
            string blocker = secondSelectedPlayer == null ? null : secondSelectedPlayer.Id;
            //HAS TO BE A FREE THROW!!
            if (str.Equals("1"))
            {
                mse = new MissedShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, null, 
                                          "free-throw", 1, false, currPoint);
            }
            // can be a jumpshot, layup, dunk, tip-in
            else if (str.Equals("2"))
            {
                Console.WriteLine("HEY: " + str);
                DataForm dataForm = new DataForm("missedShot", DataForm.SHOT_TYPE);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                mse = new MissedShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, blocker, 
                                          dataForm.shotType, 2, dataForm.fastbreak, currPoint);
            }
            else if (str.Equals("3"))
            {
                DataForm dataForm = new DataForm("missedShot", DataForm.FASTBREAK);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                mse = new MissedShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, blocker, 
                                          "jump-shot", 3, dataForm.fastbreak, currPoint);
            }

            if (MessageBox.Show("Send event: " + mse, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                pointSelected = false;
                pac.post(mse);
            }
        }

        private void rebound_Click(object sender, EventArgs e)
        {
            string str = sender.ToString();
            int strLength = str.Length;
            string type = str.Substring(0, strLength - str.IndexOf(" ") + 1).ToLower();

            ReboundEvent re = new ReboundEvent(pac, firstSelectedPlayer.Id, type, currPoint);

            if (MessageBox.Show("Send event: " + re, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                pointSelected = false;
                pac.post(re);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
