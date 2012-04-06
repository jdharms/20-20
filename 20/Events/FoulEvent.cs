using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle fouls
    public class FoulEvent : Event
    {
        string committedBy;
        Player committedByPlayer;
        string drewBy;
        Player drewByPlayer;
        string foulType;
        Boolean ejected;
        string team;

        public FoulEvent(Alpaca pac, string team, string committedBy, string drewBy, string foulType, Boolean ejected, Point location)
            : base(pac)
        {
            this.committedBy = committedBy;
            this.committedByPlayer = pac.getPlayer(committedBy);
            this.drewBy = drewBy;
            this.drewByPlayer = pac.getPlayer(drewBy);
            this.foulType = foulType;
            this.ejected = ejected;
            this.location = location;
            this.team = team;
            apiCall = "foul";
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameId = pac.GameID, 
                committedBy = committedBy, 
                drewBy = drewBy, 
                foulType = foulType, 
                ejected = ejected, 
                location = this.convertPointToArray(location), 
                context = context 
            },
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        // Adds fouls to the correct person
        public override void resolve()
        {
            // If the person was a team, just adds foul
            if (pac.HomeTeam.teamPlayer.Id == committedBy)
            {
                int fouls = pac.HomeTeam.TeamFouls;
                fouls++;
                pac.HomeTeam.TeamFouls = fouls;
            }
            else if (pac.AwayTeam.teamPlayer.Id == committedBy)
            {
                int fouls = pac.AwayTeam.TeamFouls;
                fouls++;
                pac.AwayTeam.TeamFouls = fouls;
            }
            // Else it is a player
            else
            {
                // If the player is on the Away Team, add a foul
                // to the team and the player
                bool isHome = pac.getPlayer(committedBy).TeamId == pac.HomeTeam.Id;
                if (!isHome)
                {
                    int fouls = pac.AwayTeam.TeamFouls;
                    fouls++;
                    if (fouls < 0)
                        fouls = 0;
                    pac.AwayTeam.TeamFouls = fouls;
                    pac.AwayTeam.registerFoul(committedBy, ejected);
                }
                // If the player is on the Home Team, add a foul
                // to the team and the player
                else
                {
                    int fouls = pac.HomeTeam.TeamFouls;
                    fouls++;
                    if (fouls < 0)
                        fouls = 0;
                    pac.HomeTeam.TeamFouls = fouls;
                    pac.HomeTeam.registerFoul(committedBy, ejected);
                }
            }
        }

        // Remoes the fouls that were previously assigned
        public override void unresolve()
        {
            // If the foul was by the team, remove from team
            if (pac.HomeTeam.teamPlayer.Id == committedBy)
            {
                int fouls = pac.HomeTeam.TeamFouls;
                fouls--;
                if (fouls < 0)
                    fouls = 0;
                pac.HomeTeam.TeamFouls = fouls;
            }
            else if (pac.AwayTeam.teamPlayer.Id == committedBy)
            {
                int fouls = pac.AwayTeam.TeamFouls;
                fouls--;
                if (fouls < 0)
                    fouls = 0;
                pac.AwayTeam.TeamFouls = fouls;
            }
            // Then it must be a player
            else
            {
                // Removes the foul from the team and from the player
                bool isHome = team == pac.HomeTeam.Id;

                if (!isHome)
                {
                    int fouls = pac.AwayTeam.TeamFouls;
                    fouls--;
                    if (fouls < 0)
                        fouls = 0;
                    pac.AwayTeam.TeamFouls = fouls;
                    pac.AwayTeam.removeFoul(committedBy, ejected);
                            
                }
                else
                {
                    int fouls = pac.HomeTeam.TeamFouls;
                    fouls--;
                    if (fouls < 0)
                        fouls = 0;
                    pac.HomeTeam.TeamFouls = fouls;
                    pac.HomeTeam.removeFoul(committedBy, ejected);
                }

            }
        }

        public override string ToString()
        {
            string drewString = "";
            string committedString = "";
            string ejectionString = "";

            if (drewByPlayer == null)
            {
                drewString = "";
            }
            else if (drewByPlayer.TeamPlayer)
            {
                drewString = ". Drawn by " + pac.getTeamById(drewByPlayer.TeamId).Name;
            }
            else
            {
                drewString = ". Drawn by " + drewByPlayer.DisplayName;
            }

            if (committedByPlayer.TeamPlayer)
            {
                committedString = pac.getTeamById(committedByPlayer.TeamId).Name;
            }
            else
            {
                committedString = committedByPlayer.DisplayName;
            }

            if (ejected)
            {
                ejectionString = " " + committedByPlayer.DisplayName + " ejected.";
            }

                
            return char.ToUpper(foulType[0]) + foulType.Substring(1) + " foul on " + committedString + drewString + "." + ejectionString;
        }
    }
}