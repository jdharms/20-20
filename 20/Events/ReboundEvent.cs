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

        public ReboundEvent(Alpaca pac, string rebounder, string reboundType, Point location)
            : base(pac)
        {
            //empty constructor
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public string serialize()
        {
            return JsonConvert.SerializeObject(new { gameID = pac.GameID, rebounder = rebounder, reboundType = reboundType, location = location, context = pac.generateContext() });
        }

        // Nothing For the moment
        public void resolve()
        {
            // Now it's a ghost town
        }

        // You wanted something here
        public void unresolve()
        {
            // But nothing is here
        }

    }
}
