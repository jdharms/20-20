﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace _20
{
    public class Team
    {
        private string id;
        public string Id { get { return id; } set { id = value; }  }
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private List<Player> players;
        public List<Player> Players { get { return players; } set { players = value; } }
        private List<Player> ejected;
        public List<Player> Ejected { get { return ejected; } }
        private List<Player> onCourt; 
        public Player teamPlayer;

        private int score;
        public int Score { get { return score; } set { score = value; } }

        private int teamFouls;
        public int TeamFouls { get { return teamFouls; } set { teamFouls = value; } }

        private int timeoutsUsed;
        public int TimeoutsUsed { get { return timeoutsUsed; } set { timeoutsUsed = value; } }
        public int TimeoutsLeft { get { return 5 - timeoutsUsed; } }
        private Stack<int> teamFoulsStack;
        
        public Team(string id, string name, List<Player> players)
        {
            this.id = id;
            this.name = name;
            this.players = players;
            this.teamFouls = 0;
            this.timeoutsUsed = 0;
            this.score = 0;
            this.onCourt = new List<Player>();
            this.ejected = new List<Player>();
            this.teamFoulsStack = new Stack<int>();

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
            Player toReturn = players.SingleOrDefault(player => player.Id.Equals(playerId));
            if (toReturn == null)
            {
                toReturn = ejected.SingleOrDefault(player => player.Id.Equals(playerId));
            }

            return toReturn;
        }

        public bool playerOnCourt(string playerId)
        {
            return onCourt.Contains(getPlayer(playerId));
        }

        public bool registerFoul(string playerId, bool ejected)
        {
            if (getPlayer(playerId).addFoul())
            {
                if (ejected)
                {
                    ejectPlayer(playerId);
                }
                return true;
            }
            return false;
        }

        public bool makeSubstitution(string playerIdIn, string playerIdOut)
        {
            return makeSubstitution(getPlayer(playerIdIn), getPlayer(playerIdOut));
        }

        public List<Player> getBench()
        {
            List<Player> newList = new List<Player>();
            newList.AddRange(players.Except(onCourt));
            newList.Sort();
            return newList; 
        }

        public List<Player> getOncourt()
        {
            onCourt.Sort();
            return onCourt;
        }

        public void removeFoul(string playerId, bool ejected)
        {
            Player p = getPlayer(playerId);
            p.removeFoul();
            if (ejected)
            {
                unejectPlayer(playerId);
            }
        }

        public void ejectPlayer(Player p)
        {
            ejected.Add(p);
            players.Remove(p);
        }

        public void ejectPlayer(string playerId)
        {
            this.ejectPlayer(getPlayer(playerId));
        }

        public void unejectPlayer(Player p)
        {
            players.Add(p);
            ejected.Remove(p);
        }

        public void unejectPlayer(string playerId)
        {
            this.unejectPlayer(getPlayer(playerId));
        }

        public void pushTeamFouls(bool reset)
        {
            teamFoulsStack.Push(teamFouls);
            if (reset)
            {
                teamFouls = 0;
            }
        }

        public void resetToLastKnownTeamFouls()
        {
            teamFouls = teamFoulsStack.Pop();
        }
    }
}
