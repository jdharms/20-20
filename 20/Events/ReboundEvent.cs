using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle rebounds
    class ReboundEvent : Event
    {
        string rebounder;
        string reboundType;
        Point location;

        string rebounderName;

        public ReboundEvent(Alpaca pac, string rebounder, string reboundType, Point location)
            : base(pac)
        {
            apiCall = "rebound";
            this.rebounder = rebounder;
            this.reboundType = reboundType;
            this.location = location;

            rebounderName = pac.getPlayer(rebounder).DisplayName;
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new { gameId = pac.GameID, rebounder = rebounder, reboundType = reboundType, location = convertPointToArray(location), context = pac.generateContext() },
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        // Nothing For the moment
        public override void resolve()
        {
            // Now it's a ghost town
        }

        // You wanted something here
        public override void unresolve()
        {
            // But nothing is here
        }

        public override string ToString()
        {
            return rebounderName + " " + reboundType + " rebound.";
        }

    }
}
