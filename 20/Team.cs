using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace _20
{
    public class Team
    {
        private string id;
        public string Id { get { return id; } }
        private string name;
        public string Name { get { return name; } }

        private List<Player> players;
        private List<Player> onCourt; 
        public Player teamPlayer;

        private int score;
        public int Score { get { return score; } set { score = value; } }

        private int teamFouls;
        public int TeamFouls { get { return teamFouls; } set { teamFouls = value; } }

        private int timeoutsUsed;
        public int TimeoutsUsed { get { return timeoutsUsed; } set { timeoutsUsed = value; } }
        public int TimeoutsLeft { get { return 5 - timeoutsUsed; } }
        
        public Team(string id, string name, List<Player> players)
        {
            this.id = id;
            this.name = name;
            this.players = players;
            this.teamFouls = 0;
            this.timeoutsUsed = 5;
            this.score = 0;
            this.onCourt = new List<Player>();

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

        public override string ToString()
        {
            return name;
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
            return (timeoutsUsed > 0);
        }

        public bool useTimeout()
        {
            return (timeoutsUsed-- > 0);
        }

        public Player getPlayer(string playerId)
        {
            return players.SingleOrDefault(player => player.Id.Equals(playerId));
        }

        public bool playerOnCourt(string playerId)
        {
            return onCourt.Contains(getPlayer(playerId));
        }

        public bool registerFoul(string playerId)
        {
            return getPlayer(playerId).addFoul();
        }

        public bool makeSubstitution(string playerIdIn, string playerIdOut)
        {
            return makeSubstitution(getPlayer(playerIdIn), getPlayer(playerIdOut));
        }

        public List<Player> getBench()
        {
            List<Player> newList = new List<Player>();
            newList.AddRange(players.Except(onCourt));
            return newList; 
        }

        public List<Player> getOncourt()
        {
            return onCourt;
        }

        public void removeFoul(string playerId)
        {
            Player p = getPlayer(playerId);
            p.removeFoul();
        }

    }
}
