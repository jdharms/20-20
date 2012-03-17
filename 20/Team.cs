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
        public int Score
        {
            get
            {
                return score;
            }
        }

        private int teamFouls;
        public int TeamFouls
        {
            get
            {
                return teamFouls;
            }
        }

        private int timeOutsLeft;
        public int TimeOutsLeft
        {
            get
            {
                return timeOutsLeft;
            }
        }
        
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
                if(p.TeamPlayer)
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
                if (p.TeamId != this.id)
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
            if (goingIn.TeamId != id || comingOut.TeamId != id)
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

        public bool hasTimeOutsRemaining()
        {
            return (timeOutsLeft > 0);
        }

        public bool useTimeout()
        {
            return (timeOutsLeft-- > 0);
        }

        //TODO:
        //Add the following:
        //public bool playerOnCourt(string playerId);
        //public Player getPlayer(string playerId);
        //public bool registerFoul(string playerId); return false if it is 5th foul.
        //public Player getTeamPlayer();
        //public List<string> getBenchId();
        //public List<string> getCourtId();

    }
}
