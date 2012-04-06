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
    public partial class DataForm : Form
    {
        string type;
        public static int SHOT_TYPE = 0;
        public static int FASTBREAK = 1;
        public static int REBOUND = 2;
        public static int GOALTENDING = 2;
        public static int PLAYER_SELECT = 3;

        public static int FOUL_TYPE = 0;
        public static int CHARGING = 1;
        public static int EJECTED = 2;

        public static int TECHNICAL = 0;

        public static int sideBorderLength = 6;
        public static int topBottomBorderLength = 28;
        public string shotType;
        public string turnoverType;
        public string foulType;
        public bool cancelled;
        public bool goaltending;
        public bool fastbreak;
        public bool ejected;
        public bool rebounded;
        public string reboundType;
        private bool isHome;
        public Player playerRebounded;
        public Player playerShot;
        public Player committedBy;
        public Player replacingPlayer;
        private List<Player> playersOnCourt;
        private List<Player> playersOnBench;
        private Alpaca pac;
        private int iteration;
        private Dictionary<string, EventHandler> events;

        public DataForm(Alpaca pac, string type, int start, Point loc)
        {
            InitializeComponent();
            this.type = type;
            this.iteration = start;
            this.cancelled = false;
            this.Location = loc;
            this.pac = pac;
            events = new Dictionary<string, EventHandler>();
            events["madeShot"] = new EventHandler(this.madeShot_Click);
            events["missedShot"] = new EventHandler(this.missedShot_Click);
            events["turnover"] = new EventHandler(this.turnover_Click);
            events["foul"] = new EventHandler(this.foul_Click);
            events["tech"] = new EventHandler(this.tech_Click);
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
            if (type.Equals("tech"))
            {
                if (this.iteration == TECHNICAL)
                {
                    List<Player> players = null;
                    if (committedBy == null)
                    {
                        this.Text = "Home or Away?";
                        loadFormWithButtons(new string[] { "HOME (" + pac.HomeTeam.Name + ")", "AWAY (" + pac.AwayTeam.Name + ")"});
                    }
                    else if (committedBy.TeamPlayer)
                    {
                        players = committedBy.TeamId == pac.HomeTeam.Id ? pac.HomeTeam.getBench() : pac.AwayTeam.getBench();
                        string[] arr = loadArrayWithPlayers(true, true, players);
                        arr[players.Count - 1] = committedBy.TeamId == pac.HomeTeam.Id ? "Home (" + pac.HomeTeam.Name  + ")": "Away (" + pac.AwayTeam.Name + ")";
                        this.Text = "Select Player";
                        loadFormWithButtons(arr);
                    }
                    else
                    {
                        this.Text = "Ejected?";
                        this.iteration = EJECTED;
                        this.isHome = committedBy.TeamId == pac.HomeTeam.Id;
                        loadFormWithButtons(new string[] { "Yes", "No" });
                    }
                }
                else if (this.iteration == EJECTED)
                {
                    this.Text = "Ejected?";
                    loadFormWithButtons(new string[] { "Yes", "No" });
                }
            }
            else if (type.Equals("madeShot"))
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
                rebounded = true;
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
                else if (this.iteration == REBOUND)
                {
                    this.Text = "Rebounded?";
                    loadFormWithButtons(new string[] { "Offensive Rebound", "Defensive Rebound", "Dead Ball Rebound", "No Rebound" });
                }
            }
            else if (type.Equals("turnover"))
            {
                this.Text = "Turnover Type?";
                loadFormWithButtons(new string[] {"TRAVELING", "LOST BALL", "OFFENSIVE FOUL", 
                      "OUT OF BOUNDS", "VIOLATION", "OFFENSIVE GOALTENDING", "THROWN AWAY"  });
            }
            else if (type.Equals("foul"))
            {

                if (this.iteration == FOUL_TYPE)
                {
                    this.Text = "Foul Type?";
                    loadFormWithButtons(new String[] { "BLOCKING", "SHOOTING", "PERSONAL", "FLAGRANT" });
                }
                else if (this.iteration == CHARGING)
                {
                    this.Text = "Charging?";
                    loadFormWithButtons(new string[] { "Yes", "No" });
                }
                else if (this.iteration == EJECTED)
                {
                    this.Text = "Ejected?";
                    loadFormWithButtons(new string[] { "Yes", "No" });
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
                b.Click += i == cancelInd ? new EventHandler(this.cancelForm) : events[type];
                font = new Font(b.Font.FontFamily, 10.0f);
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
                this.Text = "Rebounded?";
                loadFormWithButtons(new string[] { "Offensive Rebound", "Defensive Rebound", "Dead Ball Rebound", "No Rebound", });
                iteration ++;
            }
            else if (iteration == REBOUND)
            {
                isHome = playerShot.TeamId == pac.HomeTeam.Id;
                bool isDead = false;                

                if (button.Text.Contains("Offensive"))
                {
                    reboundType = "offensive";
                }
                else if (button.Text.Contains("Defensive"))
                {
                    reboundType = "defensive";
                }
                else if (button.Text.Contains("Dead"))
                {
                    reboundType = "dead-ball";
                    isDead = true;
                }
                else
                {
                    rebounded = false;
                    this.Close();
                    return;
                }
                if ((isHome && reboundType.Equals("defensive")) || (!isHome && reboundType.Equals("offensive")))
                {
                    playersOnCourt = pac.AwayTeam.getOncourt();
                }
                else
                {
                    playersOnCourt = pac.HomeTeam.getOncourt();
                }

                string[] players = null;
                if (isDead)
                {
                    players = new string[] { "Home Possession", "Away Possession" };
                }
                else
                {
                    players = new string[playersOnCourt.Count + 1];
                    for (int i = 0; i < players.Length - 1; i++)
                    {
                        Player p = playersOnCourt[i];
                        players[i] = "#" + p.Jersey + " " + p.DisplayName;
                    }
                    players[players.Length - 1] = playersOnCourt[0].TeamId == pac.HomeTeam.Id ? "Home Team Rebound (" + pac.HomeTeam.Name + ")": "Away Team Rebound (" + pac.AwayTeam.Name + ")";
                }
                this.Text = "Select Player Coming in";

                iteration++;
                loadFormWithButtons(players);
            }
            else if (iteration == PLAYER_SELECT)
            {
                if (button.Text.Contains("Possession") || button.Text.Contains("Team Rebound"))
                {
                    playerRebounded = pac.getTeamPlayer(button.Text.Contains("Home"));
                }
                else
                {
                    playerRebounded = pac.getPlayerByNumber(playersOnCourt[0].TeamId == pac.HomeTeam.Id, int.Parse(button.Text.Substring(1, button.Text.IndexOf(" ") - 1)));
                }
                this.Close();
            }
        }

        private void turnover_Click(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            string text = button.Text.ToLower();
            text = text.Replace(" ", "-");

            turnoverType = text;
            Close();

        }

        private void tech_Click(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            if (iteration == TECHNICAL)
            {
                if (button.Text.Contains("HOME"))
                {
                    isHome = true;
                    playersOnBench = pac.HomeTeam.getBench();
                    loadFormWithButtons(loadArrayWithPlayers(true, true, playersOnBench));
                }
                else if (button.Text.Contains("AWAY"))
                {
                    isHome = false;
                    playersOnBench = pac.AwayTeam.getBench();
                    loadFormWithButtons(loadArrayWithPlayers(true, true, playersOnBench));
                }
                else
                {
                    if (button.Text.Contains(pac.HomeTeam.Name) || button.Text.Contains(pac.AwayTeam.Name))
                    {
                        committedBy = pac.getTeamPlayer(isHome);
                    }
                    else
                    {
                        committedBy = pac.getPlayerByNumber(isHome, int.Parse(button.Text.Substring(1, button.Text.IndexOf(" ") - 1)));
                    }

                    if (committedBy.TeamPlayer)
                    {
                        this.ejected = false;
                        this.Close();
                        return;
                    }
                    iteration = EJECTED;
                }
            }
            else if (iteration == EJECTED)
            {
                this.ejected = button.Text.Equals("Yes");
                if (!this.ejected && !this.committedBy.TeamPlayer)
                {
                    this.Close();
                    return;
                }

                isHome = committedBy.TeamId == pac.HomeTeam.Id;
                playersOnBench = isHome ? pac.HomeTeam.getBench() : pac.AwayTeam.getBench();

                string[] players = null;
                if(committedBy.TeamPlayer)
                {
                    players = new string[playersOnBench.Count];
                    players[playersOnBench.Count - 1] = pac.getTeamById(committedBy.TeamId).Name;
                }
                else
                {
                    players = new string[playersOnBench.Count - 1];
                }

                int playersInd = 0;
                for (int i = 0; i < playersOnBench.Count; i++)
                {
                    if (!playersOnBench[i].TeamPlayer)
                    {
                        players[playersInd++] = "#" + playersOnBench[i].Jersey + " " + playersOnBench[i].DisplayName;
                    }
                }
                iteration++;
                loadFormWithButtons(players);
            }
            else if (iteration == PLAYER_SELECT)
            {
                replacingPlayer = pac.getPlayerByNumber(playersOnBench[0].TeamId == pac.HomeTeam.Id, int.Parse(button.Text.Substring(1, button.Text.IndexOf(" ") - 1)));
                this.Close();
            }
        }

        private string[] loadArrayWithPlayers(bool isBench, bool withTeam, List<Player> players)
        {
            string[] toReturn = null;
            if (isBench)
            {
                if (withTeam)
                {
                    toReturn = new string[players.Count];
                    toReturn[players.Count - 1] = (players[0].TeamId == pac.HomeTeam.Id ? "Home (" : "Away (") + pac.getTeamById(players[0].TeamId).Name + ")";
                }
                else
                {
                    toReturn = new string[players.Count - 1];
                }
            }
            else
            {
                toReturn = new string[players.Count];
            }

            int arrInd = 0;
            foreach (Player p in players)
            {
                if (!p.TeamPlayer)
                {
                    toReturn[arrInd++] = "#" + p.Jersey + " " + p.DisplayName;
                }
            }

            return toReturn;
        }

        private void foul_Click(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            if (iteration == FOUL_TYPE) 
            {
                string lower = button.Text.ToLower();
                if(lower.IndexOf(" ") >= 0)
                {
                    lower = lower.Equals("tip in") ? "tip-in" : "jump-shot";
                }
                this.foulType = lower;
                loadFormWithButtons(new string[] { "Yes", "No" });
                this.Text = "Ejected?";
                iteration += 2;
            }
            else if (iteration == CHARGING)
            {
                this.foulType = button.Text.Equals("Yes") ? "charging" : "offensive";
                this.Text = "Ejected?";
                iteration++;
            }
            else if (iteration == EJECTED)
            {
                this.ejected = button.Text.Equals("Yes");
                if (!this.ejected && !this.committedBy.TeamPlayer)
                {
                    this.Close();
                    return;
                }

                isHome = committedBy.TeamId == pac.HomeTeam.Id;
                playersOnBench = isHome ? pac.HomeTeam.getBench() : pac.AwayTeam.getBench();

                string[] players = null;
                if(committedBy.TeamPlayer)
                {
                    players = new string[playersOnBench.Count];
                    players[playersOnBench.Count - 1] = pac.getTeamById(committedBy.TeamId).Name;
                }
                else
                {
                    players = new string[playersOnBench.Count - 1];
                }

                int playersInd = 0;
                for (int i = 0; i < playersOnBench.Count; i++)
                {
                    if (!playersOnBench[i].TeamPlayer)
                    {
                        players[playersInd++] = "#" + playersOnBench[i].Jersey + " " + playersOnBench[i].DisplayName;
                    }
                }
                iteration++;
                loadFormWithButtons(players);
            }
            else if (iteration == PLAYER_SELECT)
            {
                replacingPlayer = pac.getPlayerByNumber(playersOnBench[0].TeamId == pac.HomeTeam.Id, int.Parse(button.Text.Substring(1, button.Text.IndexOf(" ") - 1)));
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
