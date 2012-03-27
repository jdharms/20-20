using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace _20.Events
{
    public class TurnoverEvent : Event
    {
      
        private string committedBy;
        private string forcedBy;
        private string turnoverType;
        private Point location;

        
        public TurnoverEvent(Alpaca pac, string committedBy, string forcedBy, string turnoverType, Point location)
            : base(pac)
        {
            this.committedBy = committedBy;
            this.forcedBy = forcedBy;
            this.turnoverType = turnoverType;
            this.location = location;
            apiCall = "turnover";
        }

        public override void resolve()
        {
            //does nothing for now
        }

        public override void unresolve()
        {
            //does nothing for now
        }

        public override string serialize()
        {
            return JsonConvert.SerializeObject(new 
                { 
                    apiCall = apiCall,
                    gameId=pac.GameID,
                    commitedBy=committedBy,
                    forcedBy=forcedBy,
                    location=convertPointToArray(location),
                    context= context
                },
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public override string ToString()
        {
            Player committedBy = pac.getPlayer(this.committedBy);
            Player forcedBy = pac.getPlayer(this.forcedBy);
            string turnoverType = this.turnoverType;
            string committedByString = "";
            string forcedByString = "";

            if (committedBy == null)
            {
                committedByString = pac.getTeamById(committedBy.TeamId).Name;
            }
            else
            {
                committedByString = committedBy.DisplayName; 
            }

            if (forcedBy !=  null)
            {
                forcedByString = " Forced by " + forcedBy.DisplayName; 
            }

            return committedByString + " " + turnoverType + " turnover." + forcedByString;
        }
        
    }
}
