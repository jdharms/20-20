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
        private List<Keys> homePlayerKeys;
        private List<Keys> awayPlayerKeys;
        private List<Button> eventButtons;
        private bool waitingForReboundClick;
        private bool postRebound;
        private Event savedEvent;
        private Player savedPlayer;
        private string savedReboundType;
        private Size panelSize = new Size(234, 290);
        //private DataFormPanel panel = new DataFormPanel();

        public GameForm()
        {
            InitializeComponent();
            homePlayerKeys = new List<Keys>();
            homePlayerKeys.Add(Keys.D1);
            homePlayerKeys.Add(Keys.D2);
            homePlayerKeys.Add(Keys.D3);
            homePlayerKeys.Add(Keys.D4);
            homePlayerKeys.Add(Keys.D5);

            awayPlayerKeys = new List<Keys>();
            awayPlayerKeys.Add(Keys.Q);
            awayPlayerKeys.Add(Keys.W);
            awayPlayerKeys.Add(Keys.E);
            awayPlayerKeys.Add(Keys.R);
            awayPlayerKeys.Add(Keys.T);

            homePlayerLabels = new List<Label>();
            homePlayerLabels.Add(homePlayer1Label);
            homePlayerLabels.Add(homePlayer2Label);
            homePlayerLabels.Add(homePlayer3Label);
            homePlayerLabels.Add(homePlayer4Label);
            homePlayerLabels.Add(homePlayer5Label);
            homePlayerLabels.Add(homeNameLabel);

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
            awayPlayerLabels.Add(awayNameLabel);

            awayPlayerContexts = new List<GroupBox>();
            awayPlayerContexts.Add(awayPlayer1Context);
            awayPlayerContexts.Add(awayPlayer2Context);
            awayPlayerContexts.Add(awayPlayer3Context);
            awayPlayerContexts.Add(awayPlayer4Context);
            awayPlayerContexts.Add(awayPlayer5Context);

            eventButtons = new List<Button>();
            eventButtons.Add(madeShotButton);
            eventButtons.Add(missedShotButton);
            eventButtons.Add(foulButton);
            eventButtons.Add(jumpBallButton);
            eventButtons.Add(turnoverButton);

            jumpBallContextMenuStrip.Items.Clear();
            //this.Controls.Add(panel);
            //this.Controls.SetChildIndex(panel, 0);
        }//end constructor

        private void GameForm_Load(object sender, EventArgs e)
        {
            pac = new Alpaca();
            confirmScore(false);
            pac.OnStateChange += update;
            waitingForReboundClick = false;
            GameDataResponse gameData = pac.getGameData(pac.GameID);
            for (int i = 0; i < awayPlayerLabels.Count; i++)
            {
                if (i < 5)
                {
                    homePlayerContexts[i].Click += new EventHandler(this.playerSelect_click);
                    awayPlayerContexts[i].Click += new EventHandler(this.playerSelect_click);
                }
                homePlayerLabels[i].Click += new EventHandler(this.playerSelect_click);
                awayPlayerLabels[i].Click += new EventHandler(this.playerSelect_click);
            }
            //jumpBallContextMenuStrip.Items.Add("Possession to Home Team (" + pac.HomeTeam.Name + ")");
            //jumpBallContextMenuStrip.Items.Add("Possession to Away Team (" + pac.AwayTeam.Name + ")");
            //jumpBallContextMenuStrip.Items[0].Click += new EventHandler(this.jumpball_Click);
            //jumpBallContextMenuStrip.Items[1].Click += new EventHandler(this.jumpball_Click);
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
            homeNameLabel.Text = (pac.Possesion == pac.HomeTeam ? ">> " : "") + pac.HomeTeam.Name;
            awayNameLabel.Text = (pac.Possesion == pac.AwayTeam ? ">> " : "") + pac.AwayTeam.Name;

            venueLabel.Text = pac.GameTime + ", " + pac.GameVenue;

            if (pac.HomeTeam.TeamFouls >= 10)
            {
                label1.Text = "Double Bonus";
            }
            else if (pac.HomeTeam.TeamFouls >= 7)
            {
                label1.Text = "1-and-1";
            }
            else
            {
                label1.Text = "";
            }

            if (pac.AwayTeam.TeamFouls >= 10)
            {
                homeBonus.Text = "Double Bonus";
            }
            else if (pac.AwayTeam.TeamFouls >= 7)
            {
                homeBonus.Text = "1-and-1";
            }
            else
            {
                homeBonus.Text = "";
            }

            // Update the number of times outs left for the home and away teams
            homeTimeoutLabel.Text = "T.O. Left: " + pac.HomeTeam.TimeoutsLeft + ", Team Fouls: " + pac.HomeTeam.TeamFouls;
            awayTimeoutLabel.Text = "T.O. Left: " + pac.AwayTeam.TimeoutsLeft + ", Team Fouls: " + pac.AwayTeam.TeamFouls;


            // Disable the ability to call timeouts if a team has no timeouts left
            if (pac.HomeTeam.TimeoutsLeft <= 0)
            {
                homeTimeoutContextMenuStrip.Items[0].Enabled = false;
            }
            else
            {
                homeTimeoutContextMenuStrip.Items[0].Enabled = true;
            }


            if (pac.AwayTeam.TimeoutsLeft <= 0)
            {
                awayTimeoutContextMenuStrip.Items[0].Enabled = false;
            }
            else
            {
                awayTimeoutContextMenuStrip.Items[0].Enabled = true;
            }


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

            resetFirstSecondSelected();
            //if (firstSelectedContext != null)
            //{
            //    if (firstSelectedContext == homeBox)
            //    {
            //        firstSelectedContext.Text = "Home";
            //    }
            //    else if (firstSelectedContext == awayBox)
            //    {
            //        firstSelectedContext.Text = "Away";
            //    }
            //    else
            //    {
            //        firstSelectedContext.Text = "";
            //    }
            //}
            //if (secondSelectedContext != null)
            //{
            //    if (secondSelectedContext == homeBox)
            //    {
            //        secondSelectedContext.Text = "Home";
            //    }
            //    else if (secondSelectedContext == awayBox)
            //    {
            //        secondSelectedContext.Text = "Away";
            //    }
            //    else
            //    {
            //        secondSelectedContext.Text = "";
            //    }
            //}
            //if (firstSelectedLabel != null)
            //{
            //    firstSelectedLabel.ForeColor = Color.Black;
            //}
            //if (secondSelectedLabel != null)
            //{
            //    secondSelectedLabel.ForeColor = Color.Black;
            //}
            //// the selected players from the player labels should become null
            //firstSelectedPlayer = secondSelectedPlayer = null;
            //// the selected players' labels should become null 
            //firstSelectedLabel = secondSelectedLabel = null;
            //// the selected players' groupboxes should become null 
            //firstSelectedContext = secondSelectedContext = null;

            // If a point was not selected
            if (!pointSelected)
            {
                // refresh the court so the point doesn't stay
                courtBox.Refresh();
            }


            this.Invalidate();
        }

        private void resetFirstSecondSelected()
        {
            if (firstSelectedContext != null)
            {
                if (firstSelectedContext == homeBox)
                {
                    firstSelectedContext.Text = "Home";
                }
                else if (firstSelectedContext == awayBox)
                {
                    firstSelectedContext.Text = "Away";
                }
                else
                {
                    firstSelectedContext.Text = "";
                }
            }
            if (secondSelectedContext != null)
            {
                if (secondSelectedContext == homeBox)
                {
                    secondSelectedContext.Text = "Home";
                }
                else if (secondSelectedContext == awayBox)
                {
                    secondSelectedContext.Text = "Away";
                }
                else
                {
                    secondSelectedContext.Text = "";
                }
            }
            if (firstSelectedLabel != null)
            {
                firstSelectedLabel.ForeColor = Color.Black;
            }
            if (secondSelectedLabel != null)
            {
                secondSelectedLabel.ForeColor = Color.Black;
            }
            // the selected players from the player labels should become null
            firstSelectedPlayer = secondSelectedPlayer = null;
            // the selected players' labels should become null 
            firstSelectedLabel = secondSelectedLabel = null;
            // the selected players' groupboxes should become null 
            firstSelectedContext = secondSelectedContext = null;
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
            buttonPanel.Location = new Point(this.Width, this.Height);
            buttonPanel.Visible = false;
            courtBox.Refresh();
            historyBox.ClearSelected();
            const int imageBorder = 2;

            MouseButtons currButton = e.Button;
            Point loc = new Point(e.X, e.Y);

            if (!pac.InsidePeriod)
            {
                MessageBox.Show("You must start a period before continuing", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currButton == System.Windows.Forms.MouseButtons.Right)
            {
                buttonPanel.Visible = false;
                update();
                return;
            }


            /* We need to get the location of the click in "ESPN" coordinates.  That is, the top left corner is (0,0)
             * and the bottom right corner is (940, 500). */

            /* This is close enough for now... later we can make a better rescale of the image and perfect this algorithm */

            loc.X = (int)(loc.X - imageBorder);
            loc.Y = (int)(loc.Y - imageBorder);

            if (loc.X < 0) loc.X = 0;
            if (loc.Y < 0) loc.Y = 0;

            currPoint = loc;
            if (waitingForReboundClick)
            {
                ReboundEvent re = new ReboundEvent(pac, savedPlayer.Id, savedReboundType, currPoint);
                confirmAndSendEvent(savedEvent);
                confirmAndSendEvent(re);
                savedPlayer = null;
                savedEvent = null;
                savedReboundType = null;
                waitingForReboundClick = false;
                postRebound = true;
                return;
            }
            // the form needs to know if we have selected a point yet
            pointSelected = true;

            Graphics g = courtBox.CreateGraphics();
            using (Pen p = new Pen(Color.Red, 4))
            {
                g.DrawEllipse(p, e.X - 5, e.Y - 5, 10, 10);
            }
            Point courtLoc = this.courtBox.Location;
            int bLocX = Math.Max(courtLoc.X + e.X + 8, 8);
            bLocX = Math.Min(bLocX, courtLoc.X + courtBox.Width);
            int bLocY = Math.Max(courtLoc.Y, courtLoc.Y + e.Y - (buttonPanel.Height / 2));
            bLocY = Math.Min(bLocY, courtLoc.Y + courtBox.Height - buttonPanel.Height);
            Point buttonPanelLoc = new Point(bLocX, bLocY);
            buttonPanel.Location = buttonPanelLoc;
        }//courtBox_MouseDown


        private void courtBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (postRebound)
            {
                postRebound = false;
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!doubleClicked)
                {
                    buttonPanel.Visible = true;
                }
                else
                {
                    buttonPanel.Visible = false;
                    doubleClicked = false;
                }
            }


        }

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

        private void historyBox_Click(object sender, EventArgs e)
        {
            this.buttonPanel.Visible = false;
            if (historyBox.SelectedIndex < 0 || historyBox.SelectedIndex >= pac.EventLog.Count)
                return;
            try
            {
                Event ev = pac.EventLog[historyBox.SelectedIndex];
                Point toDraw = ev.Location;
                courtBox.Refresh();
                if (toDraw.X >= 0)
                {
                    using (Pen p = new Pen(Color.Blue, 4))
                    {
                        Graphics g = courtBox.CreateGraphics();
                        g.DrawRectangle(p, toDraw.X, toDraw.Y, 10, 10);
                        g.DrawString(ev.ToString(), homePlayer5Label.Font, Brushes.SaddleBrown, 2, 2);
                    }
                }
            }
            catch (IndexOutOfRangeException ie)
            {
                return;
            }
        }
        /**************************************************************************************************************/
        /*************************************************SUBSTITUTION*************************************************/
        /**************************************************************************************************************/

        /// <summary>
        /// Performed when a player label is clicked on. Will set the players that were selected
        /// </summary>
        /// <param name="sender">Should be a label</param>
        /// <param name="e">May or may not use</param>
        private void playerSelect_click(object sender, EventArgs e)
        {

            if (buttonPanel.Visible)
            {
                buttonPanel.Visible = false;
            }
            // Get all the players on the court
            List<Player> homeOnCourt = pac.HomeTeam.getOncourt();
            List<Player> awayOnCourt = pac.AwayTeam.getOncourt();

            // this will be used locally everywhere
            Player thisSelected = null;

            // we need to use i for later, it will be changed though for sure
            int i = -1;
            // we just use this to know if we selected a home or away player
            bool isHome = false;

            if (sender == homeNameLabel || sender == awayNameLabel)
            {
                thisSelected = pac.getTeamPlayer(sender == homeNameLabel);
                isHome = sender == homeNameLabel;
                i = 5;
            }
            else
            {
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
            }
            
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
                    firstSelectedContext = i == 5 ? homeBox : homePlayerContexts[i];
                    //do the same with label
                    firstSelectedLabel = homePlayerLabels[i];
                }//end if
                // we are on the away side
                else
                {
                    // set the first selected context to the indexed playercontext
                    firstSelectedContext = i == 5 ? awayBox : awayPlayerContexts[i];
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
                    if (firstSelectedContext == homeBox || firstSelectedContext == awayBox)
                    {
                        firstSelectedContext.Text = firstSelectedContext == homeBox ? "Home" : "Away";
                    }
                    else
                    {
                        firstSelectedContext.Text = "";
                    }
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
                    if (secondSelectedContext == homeBox || secondSelectedContext == awayBox)
                    {
                        secondSelectedContext.Text = secondSelectedContext == homeBox ? "Home" : "Away";
                    }
                    else
                    {
                        secondSelectedContext.Text = "";
                    }

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
                    secondSelectedContext = i == 5 ? homeBox : homePlayerContexts[i];
                }//end if
                // it was an away player
                else
                {
                    //set the second playeers label and context
                    secondSelectedLabel = awayPlayerLabels[i];
                    secondSelectedContext = i == 5 ? awayBox : awayPlayerContexts[i];
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
                    if (secondSelectedContext == homeBox || secondSelectedContext == awayBox)
                    {
                        secondSelectedContext.Text = secondSelectedContext == homeBox ? "Home" : "Away";
                    }
                    else
                    {
                        secondSelectedContext.Text = "";
                    }
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
                    if (secondSelectedContext == homeBox || secondSelectedContext == awayBox)
                    {
                        secondSelectedContext.Text = secondSelectedContext == homeBox ? "Home" : "Away";
                    }
                    else
                    {
                        secondSelectedContext.Text = "";
                    }
                    secondSelectedLabel = null;
                    secondSelectedContext = null;
                }

                //APPLY THE NEW SELECTED PLAYER
                secondSelectedPlayer = thisSelected;

                if (isHome)
                {
                    secondSelectedLabel = homePlayerLabels[i];
                    secondSelectedContext = sender == homeNameLabel ? homeBox : homePlayerContexts[i];
                }
                else
                {
                    secondSelectedLabel = awayPlayerLabels[i];
                    secondSelectedContext = sender == awayNameLabel ? awayBox : awayPlayerContexts[i];
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

        // Handle the KeyDown event to determine the type of character entered into the control.
        private void playerSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.Write(e.KeyChar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subPlayer_click(object sender, EventArgs e)
        {
            if (buttonPanel.Visible)
            {
                buttonPanel.Visible = false;
            }
            ToolStripMenuItem subInItem = (ToolStripMenuItem)sender;
            string subInItemText = subInItem.Text;
            ToolStripMenuItem subOutItem = (ToolStripMenuItem)subContextMenuStrip.Items[0];
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

            confirmAndSendEvent(subEvent);
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
        private void jumpball_Click(object sender, EventArgs e)
        {
            if (firstSelectedPlayer == null || secondSelectedPlayer == null)
            {
                MessageBox.Show("Please select one Home player and one Away player above", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (firstSelectedPlayer.TeamId == secondSelectedPlayer.TeamId)
            {
                MessageBox.Show("Selected players must be on different teams", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!pointSelected)
            {
                MessageBox.Show("Please select a location on the court", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                firstSelectedContext.Text = "Jump";
                secondSelectedContext.Text = "Jump";
            }

            string str = getQuickPromptResult(sender, true);
            if (str == null)
            {
                return;
            }

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

            if (str.Contains("Home"))
            {
                jbe = new JumpballEvent(pac, homePlayer.Id, awayPlayer.Id, homePlayer.Id, currPoint);
            }
            else
            {
                jbe = new JumpballEvent(pac, homePlayer.Id, awayPlayer.Id, awayPlayer.Id, currPoint);
            }

            confirmAndSendEvent(jbe);
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
        private void madeShot_Click(object sender, EventArgs e)
        {
            if (firstSelectedPlayer == null)
            {
                MessageBox.Show("Please select at least one player above", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (secondSelectedPlayer != null && firstSelectedPlayer.TeamId != secondSelectedPlayer.TeamId)
            {
                MessageBox.Show("Selected players must be on the same team", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!pointSelected)
            {
                MessageBox.Show("Please select a location on the court", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                firstSelectedContext.Text = "Shooter";
                if (secondSelectedContext != null)
                    secondSelectedContext.Text = "Assist";
            }

            bool goaltending = false;

            string str = getQuickPromptResult(sender, false);
            if (str == null)
            {
                return;
            }
            else if (str.ToLower().Equals("goaltending"))
            {
                goaltending = true;
                str = getQuickPromptResult(sender, true);
                if (str == null)
                {
                    return;
                }
            }

            MadeShotEvent mse = null;
            string assistId = secondSelectedPlayer != null ? secondSelectedPlayer.Id : null;
            //HAS TO BE A FREE THROW!!
            if (str.Equals("1"))
            {
                DataForm dataForm = new DataForm(pac, "madeShot", DataForm.GOALTENDING, generateDataFormLocation(madeShotButton));
                dataForm.Location = generateDataFormLocation(madeShotButton);
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }
                mse = new MadeShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, null,
                                        "free-throw", 1, false, goaltending, currPoint);
            }
            // can be a jumpshot, layup, dunk, tip-in
            else if (str.Equals("2"))
            {
                DataForm dataForm = new DataForm(pac, "madeShot", DataForm.SHOT_TYPE, generateDataFormLocation(madeShotButton));
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }
                mse = new MadeShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, assistId,
                                        dataForm.shotType, 2, dataForm.fastbreak, goaltending, currPoint);
            }
            else if (str.Equals("3"))
            {
                DataForm dataForm = new DataForm(pac, "madeShot", DataForm.FASTBREAK, generateDataFormLocation(madeShotButton)); 
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                mse = new MadeShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, assistId,
                                        "jump-shot", 3, dataForm.fastbreak, goaltending, currPoint);
            }

            confirmAndSendEvent(mse);

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
        private void missedShot_Click(object sender, EventArgs e)
        {
            if (firstSelectedPlayer == null)
            {
                MessageBox.Show("Please select at least one player above", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (secondSelectedPlayer != null && firstSelectedPlayer.TeamId == secondSelectedPlayer.TeamId)
            {
                MessageBox.Show("Selected players must be on different teams", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!pointSelected)
            {
                MessageBox.Show("Please select a location on the court", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                firstSelectedContext.Text = "Shooter";
                if (secondSelectedContext != null)
                    secondSelectedContext.Text = "Blocker";
            }

            MissedShotEvent mse = null;
            string str = this.getQuickPromptResult(sender, false);

            if (str == null)
            {
                return;
            }

            string blocker = secondSelectedPlayer == null ? null : secondSelectedPlayer.Id;
            DataForm dataForm = null;
            //HAS TO BE A FREE THROW!!
            if (str.Equals("1"))
            {
                dataForm = new DataForm(pac, "missedShot", DataForm.REBOUND, generateDataFormLocation(missedShotButton));
                dataForm.playerShot = firstSelectedPlayer;
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }
                mse = new MissedShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, null,
                                          "free-throw", 1, false, currPoint);
            }
            // can be a jumpshot, layup, dunk, tip-in
            else if (str.Equals("2"))
            {
                Console.WriteLine("HEY: " + str);
                dataForm = new DataForm(pac, "missedShot", DataForm.SHOT_TYPE, generateDataFormLocation(missedShotButton));
                dataForm.playerShot = firstSelectedPlayer;
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
                dataForm = new DataForm(pac, "missedShot", DataForm.FASTBREAK, generateDataFormLocation(missedShotButton));
                dataForm.playerShot = firstSelectedPlayer;
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                mse = new MissedShotEvent(pac, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId, blocker,
                                          "jump-shot", 3, dataForm.fastbreak, currPoint);
            }

            if (dataForm.rebounded)
            {
                waitingForReboundClick = true;
                savedReboundType = dataForm.reboundType;
                savedEvent = mse;
                savedPlayer = dataForm.playerRebounded;
                courtBox.Refresh();
                courtBox.CreateGraphics().DrawString("Please Select A Rebound Location", 
                    new Font(madeShotButton.Font.FontFamily, 20.0f), Brushes.SaddleBrown, new Point(2, 2));
                buttonPanel.Visible = false;
                return;
            }
            confirmAndSendEvent(mse);
        }//end missedShot_Click
        /*-----------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------MISSED SHOT END----------------------------------------------*/
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
                    MessageBox.Show("Please select at least one player above", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (secondSelectedPlayer != null && firstSelectedPlayer.TeamId == secondSelectedPlayer.TeamId)
                {
                    MessageBox.Show("Selected players must be on different teams", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!pointSelected)
                {
                    MessageBox.Show("Please select a location on the court", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    firstSelectedContext.Text = "Commit";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "Forced";
                    DataForm dataForm = new DataForm(pac, "turnover", -1, generateDataFormLocation(turnoverButton));
                    dataForm.ShowDialog();
                    if (dataForm.cancelled)
                    {
                        return;
                    }

                    string forcedBy = secondSelectedPlayer == null ? null : secondSelectedPlayer.Id;

                    TurnoverEvent te = new TurnoverEvent(pac, firstSelectedPlayer.Id, forcedBy, dataForm.turnoverType, currPoint);

                    confirmAndSendEvent(te);
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
        private void foul_Click(object sender, EventArgs e)
        {
            string str = this.getQuickPromptResult(sender, false);

            bool ejected = false;
            if (str == null)
            {
                return;
            }
            else if (str.ToLower().Equals("ejection"))
            {
                ejected = true;
                str = this.getQuickPromptResult(sender, true);
                if (str == null)
                {
                    return;
                }
            }
            print(str);
            FoulEvent fe = null;
            SubstitutionEvent se = null;
            if (!str.Equals("Technical"))
            {
                if (firstSelectedPlayer == null)
                {
                    MessageBox.Show("Please select at least one player above", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (secondSelectedPlayer != null && firstSelectedPlayer.TeamId == secondSelectedPlayer.TeamId)
                {
                    MessageBox.Show("Selected players must be on different teams", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!pointSelected)
                {
                    MessageBox.Show("Please select a location on the court", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    firstSelectedContext.Text = "Commit";
                    if (secondSelectedContext != null)
                        secondSelectedContext.Text = "Drew";
                }
            }
            string drewBy = secondSelectedPlayer == null ? null : secondSelectedPlayer.Id;
            if (str.Equals("Offensive"))
            {
                DataForm dataForm = new DataForm(pac, "foul", DataForm.CHARGING, generateDataFormLocation(foulButton));
                dataForm.ejected = ejected;
                dataForm.committedBy = firstSelectedPlayer;
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                if (dataForm.ejected)
                {
                    se = new SubstitutionEvent(pac, dataForm.replacingPlayer.Id, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId);
                }

                fe = new FoulEvent(pac, firstSelectedPlayer.TeamId, firstSelectedPlayer.Id, 
                    drewBy, dataForm.foulType, ejected, currPoint);
            }
            else if (str.Equals("Defensive"))
            {
                DataForm dataForm = new DataForm(pac, "foul", DataForm.FOUL_TYPE, generateDataFormLocation(foulButton));
                dataForm.ejected = ejected;
                dataForm.committedBy = firstSelectedPlayer;
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                if (dataForm.ejected)
                {
                    se = new SubstitutionEvent(pac, dataForm.replacingPlayer.Id, firstSelectedPlayer.Id, firstSelectedPlayer.TeamId);
                }
                fe = new FoulEvent(pac, firstSelectedPlayer.TeamId, firstSelectedPlayer.Id, 
                    drewBy, dataForm.foulType, ejected, currPoint);
            }
            else if (str.Equals("Technical"))
            {
                DataForm dataForm = new DataForm(pac, "tech", DataForm.TECHNICAL, generateDataFormLocation(foulButton));
                dataForm.ejected = ejected;
                dataForm.committedBy = firstSelectedPlayer;
                dataForm.ShowDialog();
                if (dataForm.cancelled)
                {
                    return;
                }

                if (dataForm.cancelled)
                {
                    return;
                }

                if (dataForm.ejected)
                {
                    se = new SubstitutionEvent(pac, dataForm.replacingPlayer.Id, dataForm.committedBy.Id, dataForm.committedBy.TeamId);
                }
                fe = new FoulEvent(pac, dataForm.committedBy.TeamId, dataForm.committedBy.Id, 
                    null, "technical", ejected, currPoint);
                
            }

            confirmAndSendEvent(fe);
            if (se != null)
            {
                confirmAndSendEvent(se);
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
            if (MessageBox.Show("Really Delete?", "Confirm Delete.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
            Button button = (Button)sender;
            // Check if its a period or an overtime
            string perOrOver = pac.Period > 2 ? "Overtime" : "Period";
            Event ev = null;

            // if the button had "Start" in it
            if (button.Text.Contains("Start"))
            {
                ev = new PeriodStartEvent(pac);
                pac.post(ev);
                perOrOver += " End";
                return;
            }// end if
            //PERIOD END
            else
            {
                ev = new PeriodEndEvent(pac);
                bool sameScore = confirmScore(false);
                if (pac.Period >= 2)
                {
                    // going into overtime
                    if (sameScore)
                    {
                        pac.post(ev);
                        perOrOver = "Overtime";
                    }
                    //we are trying to end the game
                    else
                    {
                        pac.post(ev);
                        //the score is different, we are ending NOW

                        if (!confirmScore(true))
                        {
                            ev = new GameEndEvent(pac);
                            pac.post(ev);
                        }
                        // the user changed the score so we are going into another overtime
                        else
                        {
                            perOrOver = "Overtime";
                        }
                    }
                }
                else
                {
                    pac.post(ev);
                }
                perOrOver += " Start";
            }// end else
            button.Text = perOrOver;
        }//end periodChange_Click

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endGame"></param>
        /// <returns>True if the teams score are the same after confirming</returns>
        private bool confirmScore(bool endGame)
        {
            ConfirmScoreForm conf = new ConfirmScoreForm(pac, endGame);
            if (endGame)
            {
                conf.Text = "Final Game Results";
            }
            conf.ShowDialog();
            pac.HomeTeam.Score = conf.homeScore;
            pac.AwayTeam.Score = conf.awayScore;
            return pac.HomeTeam.Score == pac.AwayTeam.Score;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeout_Click(object sender, EventArgs e)
        {

            
        }//end timeout_Click
       
        /// <summary>
        /// Confirms the user to send the an event. If they say it's okay, we tell alpaca to send it.
        /// </summary>
        /// <param name="e">The Event to send</param>
        /// <returns>True if we posted the event, otherwise false</returns>
        private bool confirmAndSendEvent(Event e)
        {
            bool send = true;
            // Send a message box that asks the 
            if (confirmCheckBox.Checked)
            {
                send = MessageBox.Show("Press OK to send the following event:\n" + e, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
            } // end if

            if (send)
            {
                //our point is already used, so we have technically "not selected a point"
                pointSelected = false;
                // post the event
                if (!pac.post(e))
                {
                    return false;
                }
                this.buttonPanel.Visible = false;
                historyBox_Click(null, null);
                // they said yes!!!! Return true
                return true;
            }
            // They did not want to send the event...awkward...return false
            return false;
        }

        private void GameForm_MouseDown(object sender, MouseEventArgs e)
        {
            buttonPanel.Visible = false;
        }

        private void courtBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                courtBox_MouseDown(sender, e);
            }
        }

        private bool doubleClicked;
        private void courtBox_DoubleClick(object sender, EventArgs e)
        {
            doubleClicked = true;
            courtBox.Refresh();

            //if (buttonPanel.Visible)
            //{
            //    buttonPanel.Visible = false;
            //}
        } // end confirmAndSendEvent(Event e);

        private Point generateDataFormLocation(Button button)
        {
            return new Point(this.Location.X + buttonPanel.Location.X, this.Location.Y +
                buttonPanel.Location.Y);
        }

        private Point generateContextMenuLocation(object sender)
        {
            Control b = (Control)sender;
            return new Point(buttonPanel.Location.X + b.Location.X,  buttonPanel.Location.Y + b.Location.Y);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (homePlayerKeys.Contains(keyData))
            {
                playerSelect_click(homePlayerLabels[homePlayerKeys.IndexOf(keyData)], null);
            }
            else if(awayPlayerKeys.Contains(keyData))
            {
                playerSelect_click(awayPlayerLabels[awayPlayerKeys.IndexOf(keyData)], null);
            }
            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void flipSides_Click(object sender, EventArgs e)
        {
            Point home = homeBox.Location;
            homeBox.Location = awayBox.Location;
            awayBox.Location = home;

            this.update();
        }

        private Player lastPlayer = null;
        private Point lastClick = new Point(-1, -1);
        public string getQuickPromptResult(object sender, bool bizzare)
        {
            QuickPrompt prompt = null;
            if (sender is Button)
            {
                prompt = new QuickPrompt(pac, ((Button)sender).Text, generateContextMenuLocation(sender), bizzare);
            }
            else
            {
                if (sender == homeNameLabel || sender == awayNameLabel)
                {
                    Point p = sender == homeNameLabel ? new Point(homeBox.Location.X, homeBox.Location.Y) : new Point(awayBox.Location.X, awayBox.Location.Y);
                    prompt = new QuickPrompt(pac, "timeout", p, bizzare);
                }
                else
                {
                    GroupBox context = null;
                    Point loc = new Point(-1, -1);
                    if (sender is Label)
                    {
                        if (bizzare)
                        {
                            context = homePlayerContexts[homePlayerLabels.IndexOf((Label)sender)];
                            loc.X = homeBox.Location.X + context.Location.X;
                            loc.Y = homeBox.Location.Y + context.Location.Y;
                        }
                        else
                        {
                            context = awayPlayerContexts[awayPlayerLabels.IndexOf((Label)sender)];
                            loc.X = awayBox.Location.X + context.Location.X;
                            loc.Y = awayBox.Location.Y + context.Location.Y;
                        }
                    }
                    else
                    {
                        context = (GroupBox)sender;
                        if (bizzare)
                        {
                            loc.X = homeBox.Location.X + context.Location.X;
                            loc.Y = homeBox.Location.Y + context.Location.Y;
                        }
                        else
                        {
                            loc.X = awayBox.Location.X + context.Location.X;
                            loc.Y = awayBox.Location.Y + context.Location.Y;
                        }

                    }
                    prompt = new QuickPrompt(pac, "sub", loc, bizzare);
                    prompt.Text = "Sub out #" + lastPlayer.Jersey + " (" + lastPlayer.DisplayName + ")";
                }
            }

            prompt.ShowDialog();
            return prompt.cancelled ? null : prompt.result;
        }

        public void print(object o)
        {
            Console.WriteLine(o); 
        }

        private void sub_DoubleClick(object sender, EventArgs e)
        {
            string result = null;
            if (sender == homeNameLabel || sender == awayNameLabel)
            {
                resetFirstSecondSelected();
                result = getQuickPromptResult(sender, sender == homeNameLabel);
                if (result == null)
                {
                    return;
                }
                TimeoutEvent timeoutEvent = null;
                if (result.Equals("Home Timeout"))
                {
                    timeoutEvent = new TimeoutEvent(pac, pac.HomeTeam.Id, "team");
                }
                else if (result.Equals("Away Timeout"))
                {
                    timeoutEvent = new TimeoutEvent(pac, pac.AwayTeam.Id, "team");
                }
                else if (result.Equals("Media Timeout"))
                {
                    timeoutEvent = new TimeoutEvent(pac, null, "media");
                }
                else if (result.Equals("Official Timeout"))
                {
                    timeoutEvent = new TimeoutEvent(pac, null, "official");
                }

                confirmAndSendEvent(timeoutEvent);
                return;
            }

            // lets get the jersey nubmer that was clicked on
            int senderNumber = -1;
            if (sender is Label)
            {
                homeRightClicked = homePlayerLabels.Contains(sender);
                senderNumber = int.Parse(((Label)sender).Text);
            }
            else
            {
                homeRightClicked = homePlayerContexts.Contains(sender);
                senderNumber = homeRightClicked ? int.Parse(homePlayerLabels[homePlayerContexts.IndexOf((GroupBox)sender)].Text) 
                                                : int.Parse(awayPlayerLabels[awayPlayerContexts.IndexOf((GroupBox)sender)].Text);
            }
            lastPlayer = pac.getPlayerByNumber(homeRightClicked, senderNumber);

            resetFirstSecondSelected();
            result = getQuickPromptResult(sender, homeRightClicked);
            if (result == null)
            {
                return;
            }

            int outBeg = result.IndexOf("#") + 1;
            int outLength = result.IndexOf(" (") - outBeg;

            int subOutNumber = int.Parse(result.Substring(outBeg, outLength));

            Player subOutPlayer = lastPlayer;
            Player subInPlayer = null;

            subInPlayer = pac.getPlayerByNumber(homeRightClicked, subOutNumber);

            SubstitutionEvent subEvent = new SubstitutionEvent(pac, subInPlayer.Id, subOutPlayer.Id, subInPlayer.TeamId);

            confirmAndSendEvent(subEvent);
        }
    }// end GameForm
} //end using namespace