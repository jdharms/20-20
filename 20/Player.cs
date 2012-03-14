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
        private string displayName;
        private string id;
        private int jersey;
        private string teamId;


        private bool teamPlayer;

        private int fouls;
        private bool onCourt;

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

        public bool isTeamPlayer()
        {
            return teamPlayer;
        }

        public int getJersey()
        {
            return jersey;
        }

        public int getFouls()
        {
            return fouls;
        }

        public bool isOnCourt()
        {
            return onCourt;
        }

        public string getId()
        {
            return id;
        }

        public string getTeamId()
        {
            return teamId;
        }

        public string getDisplayName()
        {
            return fName + " " + lName;
        }

        public bool Equals(Player p)
        {
            return (p.getId() == this.id);
        }

    }
}
