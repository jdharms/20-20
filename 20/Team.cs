using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    class Team
    {
        private string id;
        private string name;

        List<Player> players;
        List<Player> onCourt;
        Player teamPlayer;

        private int score;
        private int teamFouls;
        private int timeOutsLeft;

        public Team(string id, string name, List<Player> players)
        {
            this.id = id;
            this.name = name;
            this.players = players;
            this.teamFouls = 0;
            this.timeOutsLeft = 5;
            this.score = 0;

            //find and set teamPlayer
            foreach(Player p in players)
            {
                if(p.isTeamPlayer())
                {
                    teamPlayer = p;
                    break;
                }
            }

        }


        /*
         * Authors:
         *          Daniel
         *    
         * Purpose:
         *          Sets the players on the court to the list of
         *          players given in argument "players".
         *          
         * Returns:
         *          true iff the operation is successful.
         */
        public bool setPlayersOnCourt(List<Player> players)
        {
            //Make sure we're putting five men on the court.
            if (players.Count() != 5)
            {
                return false;
            }

            //Make sure each player is on our team...
            foreach (Player p in players)
            {
                if (p.getTeamId() != this.id)
                {
                    return false;
                }
            }

            onCourt = players;
            return true;
        }

        public bool makeSubstitution(Player goingIn, Player comingOut)
        {
            //make sure both players are on this team
            if (goingIn.getTeamId() != id || comingOut.getTeamId() != id)
            {
                return false;
            }

            //make sure player is not already in
            if (onCourt.Contains(goingIn))
            {
                return false;
            }

            //make sure we're recalling a player on the floor
            if (!onCourt.Contains(comingOut))
            {
                return false;
            }

            onCourt.Remove(comingOut);
            onCourt.Add(goingIn);

            return true;
        }

        public bool Equals(Team team)
        {
            if (team == null)
                return false;
            return (team.id == this.id);
        }

        public int timeOutsRemaining()
        {
            return timeOutsLeft;
        }

        public bool hasTimeOutsRemaining()
        {
            return timeOutsLeft > 0;
        }

        public bool useTimeout()
        {
            return (timeOutsLeft-- > 0);
        }


    }
}
