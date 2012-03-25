using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    public class Player
    {
        private string fName;
        private string mName;
        private string lName;
        public string Name { get { return lName + ", " + fName + " " + mName; } }

        public string DisplayName { get { return fName + " " + lName; } }

        private string id;
        public string Id { get { return id; } }

        private int jersey;
        public int Jersey { get { return jersey; } }

        private string teamId;
        public string TeamId { get { return teamId; } }

        private bool teamPlayer;
        public bool TeamPlayer { get { return teamPlayer; } }

        private int fouls;
        public int Fouls { get { return fouls; } }

        private bool ejected;
        public bool Ejected { get { return ejected; } }

        public bool OnCourt { get; set; }

        public Player(string id, string[] name, int jersey, string teamId, bool isTeamPlayer)
        {
            this.id = id;
            fName = name[0];
            mName = name[1];
            lName = name[2];
            this.jersey = jersey;
            this.teamId = teamId;
            this.teamPlayer = isTeamPlayer;

            fouls = 0;
            this.ejected = false;
        }

        public bool Equals(Player p)
        {
            if (p == null)
                return false;
            return (p.Id == this.id);
        }

        /// <summary>
        /// Adds a foul to the player.
        /// </summary>
        /// <returns>false if the player this was the player's 5th foul.</returns>
        public bool addFoul()
        {
            if (++fouls == 5)
            {
                return false;
            }
            return true;
        }

        public void removeFoul()
        {
            fouls--;
        }

        public override string ToString()
        {
            return DisplayName;
        }

    }
}
