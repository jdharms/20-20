using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _20
{
    public partial class CustomTeam : Form
    {
        public Team team;
        public bool cancelled; 
        private BindingList<Player> players;
        private int currId;

        public CustomTeam()
        {
            InitializeComponent();
        }

        public CustomTeam(Team team)
        {
            InitializeComponent();
            this.team = team;
        }

        private void CustomTeam_Load(object sender, EventArgs e)
        {
            currId = 0;
            if (this.team == null)
            {
                this.players = new BindingList<Player>();
                this.team = new Team("" + System.DateTime.Today.ToString(), this.teamNameTextBox.Text, players.ToList<Player>());
            }
            else
            {
                this.players = new BindingList<Player>(team.Players);
            }
            this.playersListBox.DataSource = players;
            updateForm();
        }

        private void updateForm()
        {
            this.teamNameTextBox.Text = this.team.Name;
            if (this.playersListBox.SelectedItem == null)
            {
                this.playerNameTextBox.Enabled = false;
                this.playerNameTextBox.Text = "";
                this.playerNumberTextBox.Enabled = false;
                this.playerNumberTextBox.Text = "";
            }
            else
            {
                this.playerNameTextBox.Enabled = true;
                this.playerNameTextBox.Text = ((Player)this.playersListBox.SelectedItem).DisplayName;
                this.playerNumberTextBox.Enabled = true;
                this.playerNumberTextBox.Text = ((Player)this.playersListBox.SelectedItem).Jersey + "";
            }
            this.Refresh();
            this.Invalidate();
        }

        private void savePlayerButton_Click(object sender, EventArgs e)
        {
            try
            {
                int jersey = int.Parse(this.playerNumberTextBox.Text);
                if (this.containsNumber(jersey))
                {
                    MessageBox.Show("Player already contains that number", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Player toChange = ((Player)this.playersListBox.SelectedItem);
                this.players.Remove(toChange);
                toChange.Jersey = jersey;
                toChange.Name = this.playerNameTextBox.Text;
                this.players.Add(toChange);
                this.playersListBox.SetSelected(this.indexOf(toChange), true);
                updateForm();
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Player number must be a number", "", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void deletePlayerButton_Click(object sender, EventArgs e)
        {
            this.players.Remove(((Player)this.playersListBox.SelectedItem));
            updateForm();
        }

        private void saveTeamButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sfd.Title = "Select a File To Save To";
            sfd.Filter = "Team Files (*.team)|*.team";

            string filename = "";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(generateFileFormat());
                }
            }
        }

        private void loadTeamButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Title = "Select a File To Save To";
            ofd.Filter = "Team Files (*.team)|*.team";

            string filename = "";
            if (MessageBox.Show("Are you sure? Loading will discard any changes made.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.players.Clear();
                    filename = ofd.FileName;
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        team.Id = reader.ReadLine().TrimEnd();
                        team.Name = reader.ReadLine().TrimEnd();
                        while (true)
                        {
                            try
                            {
                                string playerId = reader.ReadLine().TrimEnd();
                                string playerName = reader.ReadLine().TrimEnd();
                                int jersey = int.Parse(reader.ReadLine().TrimEnd());
                                Player p = new Player(playerId, new string[] { "", "", "" }, jersey, team.Id, false);
                                p.Name = playerName;
                                this.players.Add(p);
                                currId++;
                            }
                            catch (Exception ex)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (this.players.Count > 0)
            {
                this.playersListBox.SetSelected(0, true);
            }
            updateForm();
        }

        private void addPlayer_Click(object sender, EventArgs e)
        {
            Player p = new Player("" + currId++, new string[] {"New", "", "Player"}, generateNextAvailableNumber(), this.team.Id, false);
            this.players.Add(p);
            this.playersListBox.SetSelected(this.indexOf(p), true);
            updateForm();
        }

        private bool containsNumber(int n)
        {
            foreach (Player p in this.players)
            {
                if (p.Jersey == n && p.Id != ((Player) this.playersListBox.SelectedItem).Id)
                    return true;
            }

            return false;
        }

        private int generateNextAvailableNumber()
        {
            if (this.players.Count == 0)
            {
                return 0;
            }

            int num = 0;
            while (true)
            {
                foreach (Player p in this.players)
                {
                    if (p.Jersey == num)
                    {
                        num++;
                        continue;
                    }
                }
                break;
            }
            return num;
        }

        private void playersListBox_Click(object sender, EventArgs e)
        {
            if (((Player) this.playersListBox.SelectedItem) == null)
            {
                return;
            }
            this.playerNameTextBox.Text = ((Player) this.playersListBox.SelectedItem).DisplayName;
            this.playerNumberTextBox.Text = ((Player) this.playersListBox.SelectedItem).Jersey + "";
            updateForm();
        }

        private void teamNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.team.Name = this.teamNameTextBox.Text;
        }

        private int indexOf(Player p)
        {
            List<Player> l = this.players.ToList<Player>();
            l.Sort();
            return l.IndexOf(p);
        }

        private string generateFileFormat()
        {
            string toSave = "";
            toSave += this.team.Id + "\n";
            toSave += this.team.Name + "\n";
            
            foreach(Player p in this.players)
            {
                if (!p.TeamPlayer)
                {
                    toSave += p.Id + "\n";
                    toSave += p.DisplayName + "\n";
                    toSave += p.Jersey + "\n";
                }
            }

            return toSave;
        }

        private void useTeamButton_Click(object sender, EventArgs e)
        {
            if (this.teamNameTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please Enter a Team Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.players.Count == 0)
            {
                MessageBox.Show("Please Create At Least 1 Player", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.team.Players = this.players.ToList<Player>();
            Player teamPlayer = new Player("" + currId, new string[] {"Team", "" , "Player"}, -1, this.team.Id, true);
            this.team.Players.Add(teamPlayer);
            this.team.teamPlayer = teamPlayer;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.cancelled = true;
            this.Close();
        }
    }
}
