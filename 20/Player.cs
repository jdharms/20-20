using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    public class Player : IComparable<Player>
    {
        private string fName;
        private string mName;
        private string lName;
        public string Name { get { return lName + ", " + fName + " " + mName; } set { setPlayerName(value);}}

        public string DisplayName { get { return teamPlayer ? "Team Player" : fName + " " + lName; } }

        private string id;
        public string Id { get { return id; } set { id = value; } }

        private int jersey;
        public int Jersey { get { return jersey; } set{jersey = value;} }

        private string teamId;
        public string TeamId { get { return teamId; } }

        private bool teamPlayer;
        public bool TeamPlayer { get { return teamPlayer; } }

        private int fouls;
        public int Fouls { get { return fouls; } }

        private bool ejected;
        public bool Ejected { get { return ejected; } }

        public Player(string id, string[] name, int jersey, string teamId, bool isTeamPlayer)
        {
            this.id = id;
            setPlayerName(name);
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

        private void setPlayerName(string[] name)
        {
            fName = name[0];
            mName = name[1];
            lName = name[2];
        }
        private void setPlayerName(string value)
        {
            string[] name = new string[3]; 
            string[] split = value.Split();
            name[0] = split[0];
            if(split.Length == 2)
            {
                name[1] = "";
                name[2] = split[1];
            }
            else if(split.Length == 3)
            {
                name[1] = split[1];
                name[2] = split[2]; 
            }

            setPlayerName(name);
        }


        public override string ToString()
        {
            return (jersey < 10 ? "0" : "" ) + jersey + "\t" + DisplayName;
        }


        public int CompareTo(Player other)
        {
            int me = jersey;
            int them = other.jersey;

            if (me < them)
                return -1;
            else if (me == them)
                return 0;
            return 1;
        }
    }
}
