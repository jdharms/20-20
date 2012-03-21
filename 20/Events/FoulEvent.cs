using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle fouls
    class FoulEvent : Event
    {
        string committedBy;
        string drewBy;
        string foulType;
        Boolean ejected;
        Point location;
        string team;

        public FoulEvent(Alpaca pac, string team, string committedBy, string drewBy, string foulType, Boolean ejected, Point location)
            : base(pac)
        {
            this.committedBy = committedBy;
            this.drewBy = drewBy;
            this.foulType = foulType;
            this.ejected = ejected;
            this.location = location;
            this.team = team;
            apiCall = "foul";
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public string serialize()
        {
            return JsonConvert.SerializeObject(new { gameID = pac.GameID, committedBy = committedBy, drewBy = drewBy, foulType = foulType, ejected = ejected, location = location, context = pac.generateContext() });
        }

        // Adds fouls to the correct person
        public void resolve()
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
                if (team == pac.AwayTeam.Id)
                {
                    int fouls = pac.AwayTeam.TeamFouls;
                    fouls++;
                    if (fouls < 0)
                        fouls = 0;
                    pac.AwayTeam.TeamFouls = fouls;
                    pac.AwayTeam.registerFoul(committedBy);
                }
                // If the player is on the Home Team, add a foul
                    // to the team and the player
                else if (team == pac.HomeTeam.Id)
                {
                    int fouls = pac.HomeTeam.TeamFouls;
                    fouls++;
                    if (fouls < 0)
                        fouls = 0;
                    pac.HomeTeam.TeamFouls = fouls;
                    pac.HomeTeam.registerFoul(committedBy);
                }
            }
        }

        // Remoes the fouls that were previously assigned
        public void unresolve()
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
                if (team == pac.AwayTeam.Id)
                {
                    int fouls = pac.AwayTeam.TeamFouls;
                    fouls--;
                    if (fouls < 0)
                        fouls = 0;
                    pac.AwayTeam.TeamFouls = fouls;
                    pac.AwayTeam.removeFoul(committedBy);
                }
                else if (team == pac.HomeTeam.Id)
                {
                    int fouls = pac.HomeTeam.TeamFouls;
                    fouls--;
                    if (fouls < 0)
                        fouls = 0;
                    pac.HomeTeam.TeamFouls = fouls;
                    pac.HomeTeam.removeFoul(committedBy);
                }

            }
        }

    }
}
