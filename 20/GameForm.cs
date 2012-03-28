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
        // should these be private???
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
        }//end constructor

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
            jumpBallContextMenuStrip.Items[0].Click += new EventHandler(this.jumpball_Click);
            jumpBallContextMenuStrip.Items[1].Click += new EventHandler(this.jumpball_Click);
            update();
        }//end GameFormLoad

        /// <summary>
        /// Upadates all elements of the form that needs to be updated.
        /// 
        /// Authors: Daniel and Johny
        /// </summary>
        void update()
        {
            //populate all form controls.    
            Console.WriteLine("Updating form...");

            // Update the home and away team names
            homeNameLabel.Text = pac.HomeTeam.Name;
            awayNameLabel.Text = pac.AwayTeam.Name;

            // Update the number of times outs left for the home and away teams
            homeTimeoutLabel.Text = "T.O. Left: " + pac.HomeTeam.TimeoutsLeft;
            awayTimeoutLabel.Text = "T.O. Left: " + pac.AwayTeam.TimeoutsLeft;

            // Update the scores of the home and away team
            homeScore.Text = pac.HomeTeam.Score.ToString();
            awayScore.Text = pac.AwayTeam.Score.ToString();

            // Set the history boxes data source to alpacas event log should populate the history log
            historyBox.DataSource = pac.EventLog;
            // Tell the form that this has been refreshed
            ((CurrencyManager)historyBox.BindingContext[pac.EventLog]).Refresh();
            // if there is nothing in the history box, disable the delete button
            deleteEventButton.Enabled = historyBox.Items.Count > 0; 
            // if we have more than one item
            if (historyBox.Items.Count > 0)
            {
                //set the selected index to the last item in the history box
                historyBox.SetSelected(historyBox.Items.Count - 1, true);
            }

            // for every control (button, label, etc...)
            foreach (Control c in this.Controls)
            {
                // we never want to disable the history box and the delete button..in case we want to edit it after a game
                if (c != historyBox && c != deleteEventButton)
                {
                    //set the control to being "not gameEnded" (if the game has ended it's enabled is false, and vice versa
                    c.Enabled = !pac.GameEnded;
                }
            }

            //get the home players that are on the court
            List<Player> homeOnCourt = pac.HomeTeam.getOncourt();
            //for every player on the court
            for (int i = 0; i < homeOnCourt.Count; i++)
            {
                //set the label that contains the players number
                homePlayerLabels[i].Text = homeOnCourt[i].Jersey + "";
                //set the tool tip (hovering over it) to the players name
                toolTip1.SetToolTip(homePlayerLabels[i], homeOnCourt[i].DisplayName);
            }

            // do the same with the away teams on court
            List<Player> awayOnCourt = pac.AwayTeam.getOncourt();
            for (int i = 0; i < awayOnCourt.Count; i++)
            {
                awayPlayerLabels[i].Text = awayOnCourt[i].Jersey + "";
                toolTip1.SetToolTip(awayPlayerLabels[i], awayOnCourt[i].DisplayName);
            }

            // if the period is greater than 2 (which is when the game SHOULD be over), the text will become "Overtime", otherwise "Period"
            string perOrOver = pac.Period > 2 ? "Overtime" : "Period";

            // if we are in the middle of a period
            if (pac.InsidePeriod)
            {
                // the button should be used to end it
                periodStartButton.Text = perOrOver + " End";
            }
            // we are not in the middle of a period
            else
            {
                // the button should be used to start period
                periodStartButton.Text = perOrOver + " Start";
            }

            // the selected players from the player labels should become null
            firstSelectedPlayer = secondSelectedPlayer = null;
            // the selected players' labels should become null 
            firstSelectedLabel = secondSelectedLabel = null;
            // the selected players' groupboxes should become null 
            firstSelectedContext = secondSelectedContext = null;

            //**RESET THE CONTEXTS AND LABELS**//
            // for all the contexts and labels
            for (int i = 0; i < homePlayerContexts.Count; i++)
            {
                // Clear the contexts' text
                homePlayerContexts[i].Text = "";
                awayPlayerContexts[i].Text = "";
                // Change the labels color back to black
                homePlayerLabels[i].ForeColor = Color.Black;
                awayPlayerLabels[i].ForeColor = Color.Black;
            }

            // If a point was not selected
            if (!pointSelected)
            {
                // refresh the court so the point doesn't stay
                courtBox.Refresh();
            }


            this.Invalidate();
        }//end update
        
        /// <summary>
        /// Draws the point, from the clicked location...
        /// 
        /// TODO: Needs to be documented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            // the form needs to know if we have selected a point yet
            pointSelected = true;

            Graphics g = courtBox.CreateGraphics();
            using (Pen p = new Pen(Color.Red, 4))
            {
                g.DrawEllipse(p, e.X - 5, e.Y - 5, 10, 10);
            }
        }//courtBox_MouseDown

        /**************************************************************************************************************/
        /*************************************************SUBSTITUTION*************************************************/
        /**************************************************************************************************************/
        /// <summary>
        /// Handles a right click of a player (which is the substitution)
        /// </summary>
        /// <param name="sender">Should only be a label</param>
        /// <param name="e">May or may not use</param>
        private void playerSelect_MouseDown(object sender, MouseEventArgs e)
        {

            //if the click registered was a right click
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // we are going to reset everything in the right click menu
                subContextMenuStrip.Items.Clear();

                // get the oncourt and bench..
                List<Player> onCourt = null;
                List<Player> bench = null;
                // this will be the player that was right clicked on
                Player senderPlayer = null;

                // lets get the jersey nubmer that was clicked on
                int senderNumber = int.Parse(((Label)sender).Text);

                // if the sender was a label (probably dont need this), and the list of homeLabels has our sender, then it was home player
                if ((sender is Label && homePlayerLabels.Contains((Label)sender)))
                {
                    // our oncourt list was the home teams on court
                    onCourt = pac.HomeTeam.getOncourt();
                    //same with bench
                    bench = pac.HomeTeam.getBench();
                    //out player was the home team's player with the number we parsed earlier
                    senderPlayer = pac.getPlayerByNumber(true, senderNumber);
                    //the home team was clicked
                    homeRightClicked = true;
                }
                //it wasn't a home player, do it for away players
                else
                {
                    onCourt = pac.AwayTeam.getOncourt();
                    bench = pac.AwayTeam.getBench();
                    senderPlayer = pac.getPlayerByNumber(false, senderNumber);
                    homeRightClicked = false;
                }

                //First right click item
                ToolStripMenuItem subInItem = new ToolStripMenuItem("Sub out #" + senderNumber + " (" + senderPlayer.DisplayName + ")");
                //get an array of sub in players
                ToolStripMenuItem[] playerMenu = new ToolStripMenuItem[bench.Count - 1];
                //index to keep track of the player menu
                int toolStripInd = 0;

                //for every player on the bench
                for (int i = 0; i < bench.Count; i++)
                {
                    //if this player isnt a team player
                    if (!bench[i].TeamPlayer)
                    {
                        //make a new menu item with the bench player
                        playerMenu[toolStripInd] = new ToolStripMenuItem("with #" + bench[i].Jersey + " (" + bench[i].DisplayName + ")");
                        //make the menu item's click function subPlayer_click
                        playerMenu[toolStripInd].Click += new EventHandler(subPlayer_click);
                        //move to the next index
                        toolStripInd++;
                    }//end if
                }//end for

                //add the player menu to the drop down of the sub in player
                subInItem.DropDownItems.AddRange(playerMenu);

                //add the sub in player item in the context menu
                subContextMenuStrip.Items.Add(subInItem);
            }//end if
        }//end playerSelect_MouseDown

        /// <summary>
        /// Performed when a player label is clicked on. Will set the players that were selected
        /// </summary>
        /// <param name="sender">Should be a label</param>
        /// <param name="e">May or may not use</param>
        private void playerSelect_click(object sender, EventArgs e)

        {

            // Get all the players on the court
            List<Player> homeOnCourt = pac.HomeTeam.getOncourt();
            List<Player> awayOnCourt = pac.AwayTeam.getOncourt();

            // this will be used locally everywhere
            Player thisSelected = null;

            // we need to use i for later, it will be changed though for sure
            int i = -1;
            // we just use this to know if we selected a home or away player
            bool isHome = false;

            // iterate through each label/context
            for (i = 0; i < homePlayerLabels.Count; i++)
            {
                // if the sender was from the home side
                if (sender == homePlayerLabels[i] || sender == homePlayerContexts[i])
                {
                    // the selected player is indexed in the same location, so set it
                    thisSelected = homeOnCourt[i];
                    //we selected a home player
                    isHome = true;
                    // we now have the correct index...leave the loop
                    break;
                }//end if
                // the player is on the away side
                else if (sender == awayPlayerLabels[i] || sender == awayPlayerContexts[i])
                {
                    // the selected player is indexed in the same location, so set it
                    thisSelected = awayOnCourt[i];
                    //we did not select a home player
                    isHome = false;
                    // we have the correct index...dip out
                    break;
                }// end else if
            }//end for

            // if firstSelectedPlayer is null, then we have not set this and the second player
            if (firstSelectedPlayer == null)
            {
                // DEALING WITH ONLY THE FIRST PLAYER SELECTED 

                //set the first player
                firstSelectedPlayer = thisSelected;
                // if it was on the home side
                if (isHome)
                {
                    // set the first selected context to the indexed playercontext
                    firstSelectedContext = homePlayerContexts[i];
                    //do the same with label
                    firstSelectedLabel = homePlayerLabels[i];
                }//end if
                // we are on the away side
                else
                {
                    // set the first selected context to the indexed playercontext
                    firstSelectedContext = awayPlayerContexts[i];
                    //do the same with label
                    firstSelectedLabel = awayPlayerLabels[i];
                }//end else

                //when the first player is chosen, the text should turn red
                firstSelectedLabel.ForeColor = Color.Red;
                // the context will just show that its the first player that was selected
                firstSelectedContext.Text = "1st";
            }// end if
            // the first player has been selected at this point, so check if we are unclicking it
            else if (firstSelectedPlayer == thisSelected)
            {
                // IF WE GET HERE WE MUST UNSELECT EVERYTHING

                // set the selected players to null
                firstSelectedPlayer = null;
                secondSelectedPlayer = null;

                //probably dont need this if because it shouldnt be null if we have selected the first player...oh well
                if (firstSelectedContext != null && firstSelectedLabel != null)
                {
                    // set the lable to black
                    firstSelectedLabel.ForeColor = Color.Black;
                    // change the context to the empty strig
                    firstSelectedContext.Text = "";
                    //reset to null
                    firstSelectedLabel = null;
                    firstSelectedContext = null;
                }// end if

                // Definitely need to check if tis null
                if (secondSelectedContext != null && secondSelectedLabel != null)
                {
                    // set the label to balck
                    secondSelectedLabel.ForeColor = Color.Black;
                    // take out any text
                    secondSelectedContext.Text = "";

                    //reset to null
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }//end if
            }// end else if

            //the first player has been selected, we are not unclicking the first, and the second has not been set
            else if (secondSelectedPlayer == null)
            {
                // set the second player to the selected player
                secondSelectedPlayer = thisSelected;
                // if it was the home player
                if (isHome)
                {
                    // set the selected label and contexts
                    secondSelectedLabel = homePlayerLabels[i];
                    secondSelectedContext = homePlayerContexts[i];
                }//end if
                // it was an away player
                else
                {
                    //set the second playeers label and context
                    secondSelectedLabel = awayPlayerLabels[i];
                    secondSelectedContext = awayPlayerContexts[i];
                }// end else

                // Second player will be denoted with green text
                secondSelectedLabel.ForeColor = Color.Green;
                secondSelectedContext.Text = "2nd";
            }//end else if
            // we have selected the second player and we are unclicking it
            else if (secondSelectedPlayer == thisSelected)
            {
                // reset the second selected things to null, black font, and no text
                secondSelectedPlayer = null;
                if (secondSelectedContext != null)
                {
                    secondSelectedLabel.ForeColor = Color.Black;
                    secondSelectedContext.Text = "";
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }

            }//end else if

            // we are clicking a player that is not the current first or second player
            else
            {
                //undo previous second
                if (secondSelectedContext != null && secondSelectedLabel != null)
                {
                    secondSelectedLabel.ForeColor = Color.Black;
                    secondSelectedContext.Text = "";
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }

                //APPLY THE NEW SELECTED PLAYER
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

            // MAY NOT NEED THIS, but this will assure that the 1st and second will pop up
            if (firstSelectedContext != null)
            {
                firstSelectedContext.Text = "1st";
            }
            if (secondSelectedContext != null)
            {
                secondSelectedContext.Text = "2nd";
            }

            // refresh the form
            this.Refresh();
            this.Invalidate();
        }//end playerSelect_Click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }//end subPlayer_click
        /*------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------SUBSTITUTION END----------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        
        /**************************************************************************************************************/
        /***************************************************JUMPBALL***************************************************/
        /**************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jumpBall_MouseDown(object sender, MouseEventArgs e)
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
        }//end jumpButton_MouseDown

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jumpball_Click(object sender, EventArgs e)
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
        }//end jumpball_Click
        /*------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------JUMPBALL END------------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        /*************************************************************************************************************/
        /**************************************************MADE SHOT**************************************************/
        /*************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void madeShot_MouseDown(object sender, MouseEventArgs e)
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
        }//end madeButton_MouseDown

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void madeShot_Click(object sender, EventArgs e)
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

        }//end madeShot_Click
        /*-----------------------------------------------------------------------------------------------------------*/
        /*-----------------------------------------------MADE SHOT END-----------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------------------*/
        
        /*************************************************************************************************************/
        /*************************************************MISSED SHOT*************************************************/
        /*************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void missedShot_MouseDown(object sender, MouseEventArgs e)
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
        }//end missedShot_MouseDown
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void missedShot_Click(object sender, EventArgs e)
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
        }//end missedShot_Click
        /*-----------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------MISSED SHOT END----------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------------------*/

        /************************************************************************************************************/
        /************************************************REBOUND SHOT************************************************/
        /************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rebound_MouseDown(object sender, MouseEventArgs e)
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
        }//end rebound_MouseDown

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }//end rebound_Click
        /*-----------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------REBOUND END------------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------------------*/

        /************************************************************************************************************/
        /**************************************************TURNOVER**************************************************/
        /************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void turnover_MouseDown(object sender, MouseEventArgs e)
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
                    firstSelectedContext.Text = "Commit";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "Forced";
                    DataForm dataForm = new DataForm("turnover", -1);
                    dataForm.ShowDialog();
                    if (dataForm.cancelled)
                    {
                        return;
                    }

                    string forcedBy = secondSelectedPlayer == null ? null : secondSelectedPlayer.Id;

                    TurnoverEvent te = new TurnoverEvent(pac, firstSelectedPlayer.Id, forcedBy, dataForm.turnoverType, currPoint);
                    
                    if (MessageBox.Show("Send event: " + te, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        pointSelected = false;
                        pac.post(te);
                    }
                }
            }
        }//end turnoverButton_MouseDown
        /*-----------------------------------------------------------------------------------------------------------*/
        /*-----------------------------------------------TURNOVER END------------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------------------*/

        /************************************************************************************************************/
        /****************************************************FOUL****************************************************/
        /************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foul_MouseDown(object sender, MouseEventArgs e)
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
                    firstSelectedContext.Text = "Fouled";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "Drew";
                }
                Button btnSender = (Button)sender;
                foulContextMenuStrip.Show(this, ((Button)sender).Location);
            }
        }//end foul_MouseDown

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foul_Click(object sender, EventArgs e)
        {
            FoulEvent fe = null;
            string str = sender.ToString();
            string drewBy = secondSelectedPlayer == null ? null : secondSelectedPlayer.Id;
            if (str.Equals("Offensive Foul"))
            {
                DataForm dataForm = new DataForm("foul", DataForm.CHARGING);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                fe = new FoulEvent(pac, firstSelectedPlayer.TeamId, firstSelectedPlayer.Id, 
                    drewBy, dataForm.foulType, dataForm.ejected, currPoint);
            }
            else if (str.Equals("Defensive Foul"))
            {
                DataForm dataForm = new DataForm("foul", DataForm.FOUL_TYPE);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                fe = new FoulEvent(pac, firstSelectedPlayer.TeamId, firstSelectedPlayer.Id, 
                    drewBy, dataForm.foulType, dataForm.ejected, currPoint);
            }
            else if (str.Equals("Technical Foul"))
            {
                DataForm dataForm = new DataForm("foul", DataForm.EJECTED);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                fe = new FoulEvent(pac, firstSelectedPlayer.TeamId, firstSelectedPlayer.Id, 
                    drewBy, "technical" , dataForm.ejected, currPoint);
            }

            if (MessageBox.Show("Send event: " + fe, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                pointSelected = false;
                pac.post(fe);
            }
        }//end foul_Click
        /*-----------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------FOUL END--------------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Deletes the last event in the history box. 
        /// </summary>
        /// <param name="sender"> The object that is calling this method (should just be the delete button)</param>
        /// <param name="e">Some arguments that we may or may not user</param>
        private void deleteEvent_Click(object sender, EventArgs e)
        {
            //requests user to confirm deletion of event.
            if (MessageBox.Show("Really Delete?", "Confirm Delete.", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //this branch runs if the result of the confirmation is "OK"
                Console.WriteLine("Deleting...");

                // create a delete event, and tell pac to post it
                DeleteEvent del = new DeleteEvent(pac, pac.EventLog[pac.EventLog.Count - 1]);
                pac.post(del);
            }//end if
            else
            {
                //this branch executes if the user says "Cancel".
                Console.WriteLine("Not deleting...");
                // Don't really need this line...for now
                //historyBox.ClearSelected();
            }//end else
        }//end deleteEvent_Click

        /// <summary>
        /// Handles the click of the Period Start/End button
        /// </summary>
        /// <param name="sender">Should just be the periodStartButton</param>
        /// <param name="e">Some arguments that we may or may not user</param>
        private void periodChange_Click(object sender, EventArgs e)
        {
            // create a generic event. We may have either a period start or period end event
            Event ev = null;

            // if the period is 2 or later, and the teams are not tied, and we are inside a period
            if (pac.Period >= 2 && (pac.HomeTeam.Score != pac.AwayTeam.Score) && pac.InsidePeriod)
            {
                ev = new PeriodEndEvent(pac);
                // send a period end event
                if (!confirmAndSendEvent(ev))
                {
                    return;
                }//end if
                ev = new GameEndEvent(pac);
                // and send a game end event
                confirmAndSendEvent(ev);
                return;
            }//end if
            // the game should not end at this point
            else
            {
                // shit best be a button
                Button button = (Button)sender;
                // Check if its a period or an overtime
                string perOrOver = pac.Period > 2 ? "Overtime" : "Period";

                //Determine if we're entering/exiting a period//

                // if the button had "Start" in it
                if (button.Text.Contains("Start"))
                {
                    // create a period start event
                    ev = new PeriodStartEvent(pac);

                    // since we just started a period/overtime, we have to change the button to have end in it
                    button.Text = perOrOver + " End";
                }// end if
                // if the button had an "End" in it
                else
                {
                    // create a period end event
                    ev = new PeriodEndEvent(pac);
                    // since we just ended a period/overtime, we have to change the button to have start in it
                    button.Text = perOrOver + " Start";
                }// end else
            }//end else

            //send appropriate event to server
            confirmAndSendEvent(ev);
        }//end periodChange_Click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeout_Click(object sender, EventArgs e)
        {

            TimeoutEvent timeoutEvent = null;
            if (sender.ToString().Equals("Home Timeout"))
            {
                timeoutEvent = new TimeoutEvent(pac, pac.HomeTeam.Id, "team");
            }
            else if (sender.ToString().Equals("Away Timeout"))
            {
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
        }//end timeout_Click
       
        /// <summary>
        /// TODO: needs to be documented...COUGH COUGH, thats you daniel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }//end historyBox_MouseMove

        /// <summary>
        /// Confirms the user to send the an event. If they say it's okay, we tell alpaca to send it.
        /// </summary>
        /// <param name="e">The Event to send</param>
        /// <returns>True if we posted the event, otherwise false</returns>
        private bool confirmAndSendEvent(Event e)
        {
            // Send a message box that asks the 
            if (MessageBox.Show("Press OK to send the following event:\n" + e, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //our point is already used, so we have technically "not selected a point"
                pointSelected = false;
                // post the event
                pac.post(e);
                // they said yes!!!! Return true
                return true;
            } // end if

            // They did not want to send the event...awkward...return false
            return false;
        } // end confirmAndSendEvent(Event e)
    }// end GameForm
} //end using namespace