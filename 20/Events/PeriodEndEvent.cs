using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle the end of a period
    class PeriodEndEvent : Event
    {
        public PeriodEndEvent(Alpaca pac)
            : base(pac)
        {
            apiCall = "periodEnd";
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public string serialize()
        {
            return JsonConvert.SerializeObject(new { gameID = pac.GameID, period = pac.Period, context = pac.generateContext() });
        }

        // Not a soul found in these parts
        public void resolve()
        {
            // Empty Method
        }

        // Not here either
        public void unresolve()
        {
            // Empty Method
        }

    }
}
