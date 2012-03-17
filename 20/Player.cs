using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    class Player
    {
        private string fName;
        private string mName;
        private string lName;
        public string Name
        {
            get
            {
                return lName +", " + fName + " " + mName;
            }
        }

        private string displayName;
        public string DisplayName
        {
            get
            {
                return fName + " " + lName;
            }
        }

        private string id;
        public string Id
        {
            get
            {
                return id;
            }
        }

        private int jersey;
        public int Jersey
        {
            get
            {
                return jersey;
            }
        }

        private string teamId;
        public string TeamId
        {
            get
            {
                return teamId;
            }
        }

        private bool teamPlayer;
        public bool TeamPlayer
        {
            get
            {
                return teamPlayer;
            }
        }  

        private int fouls;
        public int Fouls
        {
            get
            {
                return fouls;
            }
        }

        private bool onCourt;
        public bool OnCourt
        {
            get
            {
                return onCourt;
            }
            set
            {
                onCourt = value;
            }
        }  

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
        }

        public bool Equals(Player p)
        {
            if (p == null)
                return false;
            return (p.Id == this.id);
        }

    }
}
