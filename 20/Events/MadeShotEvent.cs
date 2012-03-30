using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    public class MadeShotEvent : Event
    {
        string shooter;
        string assist;
        string shot;
        int points;
        bool fastBreak;
        bool goaltending;
        string teamId;

        //toString variables
        string shooterName;
        string assistName;
        string shotValue;

        public MadeShotEvent(Alpaca pac, string shooterId, string teamId, string assistId, string shotType, int pointsScored, bool fastBreakOpportunity,
                        bool goaltending, Point location)
            : base(pac)
        {
            this.shooter = shooterId;
            this.assist = assistId;
            this.teamId = teamId;
            this.shot = shotType;
            this.points = pointsScored;
            this.fastBreak = fastBreakOpportunity;
            this.goaltending = goaltending;
            this.location = location;
            apiCall = "madeShot";

            shooterName = pac.getPlayer(shooterId).DisplayName;
            assistName = (assistId != null) ? pac.getPlayer(assistId).DisplayName : "";
            shotValue = (points == 3) ? "Three Point " : "";
        }

        public override void resolve()
        {
            //Add the points to the shooter's team.
            pac.getTeamById(teamId).Score += points; 
        }

        public override void unresolve()
        {
            //Take those points away.
            pac.getTeamById(teamId).Score -= points;
        }

        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameId = pac.GameID,
                shooter = shooter,
                assistedBy = assist,
                shotType = shot,
                pointsScored = points,
                fastBreakOpportunity = fastBreak,
                goaltending = goaltending,
                location = convertPointToArray(location),
                context = context 
            }, 
            Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );
        }

        public override string ToString()
        {
            string assistString;
            if (assistName.Equals(""))
            {
                assistString = "";
            }
            else
            {
                assistString = " Assisted by " + assistName;
            }
            return pac.getPlayer(shooter).DisplayName + " made " + shotValue + shot + "." + assistString;
        }

    }
}
