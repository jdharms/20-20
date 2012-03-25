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

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            pac = new Alpaca();
            pac.OnStateChange += update;
            GameDataResponse gameData = pac.getGameData(pac.GameID);
        }

        void update()
        {
            //populate all form controls.    
            Console.WriteLine("Updating form...");

            homeNameLabel.Text = pac.HomeTeam.Name;
            awayNameLabel.Text = pac.AwayTeam.Name;

            homeScore.Text = pac.HomeTeam.Score.ToString();
            awayScore.Text = pac.AwayTeam.Score.ToString();

            historyBox.DataSource = pac.EventLog;
            ((CurrencyManager)historyBox.BindingContext[pac.EventLog]).Refresh();
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

            Graphics g = courtBox.CreateGraphics();
            using (Pen p = new Pen(Color.Red, 4))
            {
                g.DrawEllipse(p, e.X, e.Y, 10, 10);
            }
        }

        private void historyBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //The selected value "changes" even when it goes to having no selected value.
            //This only shows the delete button if the selected value is an actual event,
            //and hides the box when the new selection is nothing.
            if (historyBox.SelectedItem != null)
            {
                deleteEventButton.Visible = true;
            }
            else
            {
                deleteEventButton.Visible = false;
            }

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
         * Purpose: Handles clicks to the jumpball button.
         */
        private void jumpBallButton_Click(object sender, EventArgs e)
        {
            //Ask for location
            //Ask for home player
            //Ask for away player
            //Ask for result

            //send jumpball event to server
        }

        /*
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to the period start/end button
         */
        private void periodStartButton_Click(object sender, EventArgs e)
        {
            //Determine if we're entering/exiting a period,
            //and which period.

            //send appropriate event to server
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

        /*
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to timeout button
         */
        private void timeoutButton_Click(object sender, EventArgs e)
        {
            //ask for type of timeout
            //ask for team (if necessary)

            //send timeout event to server
        }

        /*
         * Stub author: Daniel
         * 
         * Purpose: Handles clicks to sub button
         */
        private void substitutionButton_Click(object sender, EventArgs e)
        {
            //Ask for exiting player
            SelectPlayer exiting = new SelectPlayer();
            exiting.home = pac.HomeTeam.getOncourt();
            exiting.away = pac.AwayTeam.getOncourt();
            exiting.ShowDialog();
            Player exitPlayer = exiting.selected;

            //Ask for entering player
            SelectPlayer entering = new SelectPlayer();
            entering.home = pac.HomeTeam.getBench();
            entering.away = pac.AwayTeam.getBench();
            entering.ShowDialog();
            Player enteringPlayer = entering.selected;

            //send sub event to server
            SubstitutionEvent ev = new SubstitutionEvent(pac, enteringPlayer.Id, exitPlayer.Id, enteringPlayer.TeamId);
            pac.post(ev);
        }

        private void alpacaButton_Click(object sender, EventArgs e)
        {
            //pac = new Alpaca();
            //pac.OnStateChange += update;
            //List<Game> games = pac.getGames(new DateTime(2011, 1, 1), new DateTime(2013, 1, 1));
            //GameDataResponse gameData = pac.getGameData(games[2].gameId);
            //pac.GameID = games[2].gameId;

            ////StartingLineups lineups = new StartingLineups();

            ////List<Player> hs = new List<Player>();
            ////List<Player> aw = new List<Player>();

            ////for (int i = 0; i < 5; i++)
            ////{
            ////    Player p = pac.HomeTeam.getBench()[i];
            ////    Player v = pac.AwayTeam.getBench()[i];

            ////    hs.Add(p);
            ////    aw.Add(v);

            ////    lineups.addStarter(true, p.Id);
            ////    lineups.addStarter(false, v.Id);
            ////}

            ////pac.HomeTeam.setPlayersOnCourt(hs);
            ////pac.AwayTeam.setPlayersOnCourt(aw);

            ////lineups.pack(Alpaca.generateTimestamp());
            ////pac.setGameData(lineups);

            //PeriodStartEvent e0 = new PeriodStartEvent(pac);
            //Thread.Sleep(100);
            //MadeShotEvent e1 = new MadeShotEvent(pac, "4f46b4dde4b063589e20e5c6", "4f46b4bde4b0b074044d891c", null, "dunk", 2, true, false, new Point(40, 40));
            //Thread.Sleep(100);
            //MissedShotEvent e2 = new MissedShotEvent(pac, "4f46b657e4b0b074044d8920", "4f46b620e4b0acf74eee5e21", null, "jump-shot", 3, false, new Point(100, 80));
            //Thread.Sleep(100);
            //ReboundEvent e3 = new ReboundEvent(pac, "4f46b4fde4b063589e20e5c8", "defensive", new Point(30, 30));
            //Thread.Sleep(100);
            //SubstitutionEvent e4 = new SubstitutionEvent(pac, "4f46b5a6e4b0acf74eee5e1c", "4f46b4fde4b063589e20e5c8", "4f46b4bde4b0b074044d891c");
            //Thread.Sleep(100);

            //Console.WriteLine(Alpaca.generateTimestamp());
            //Thread.Sleep(10);
            //Console.WriteLine(Alpaca.generateTimestamp());
            //Thread.Sleep(10);
            //Console.WriteLine(Alpaca.generateTimestamp());
            //Thread.Sleep(10);
            //Console.WriteLine(Alpaca.generateTimestamp());
            //Thread.Sleep(10);
            //Console.WriteLine(Alpaca.generateTimestamp());
            //Thread.Sleep(10);
            //Console.WriteLine(Alpaca.generateTimestamp());

            //SelectPlayer sp = new SelectPlayer();
            //sp.home = pac.HomeTeam.getOncourt();
            //sp.away = pac.AwayTeam.getOncourt();
            //sp.Show();

            ////pac.post(e0);
            ////pac.post(e1);
            ////pac.post(e2);
            ////pac.post(e3);
            ////pac.post(e4);

            //////Have to create delete event after event has been processed.
            ////DeleteEvent ed = new DeleteEvent(pac, e4);
            ////pac.post(ed);

        }

        private void courtBox_Click(object sender, EventArgs e)
        {

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


    }
}
