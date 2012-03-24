using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle the start of a period
    class PeriodStartEvent : Event
    {
        public PeriodStartEvent(Alpaca pac)
            : base(pac)
        {
            apiCall = "periodStart";
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameId = pac.GameID, 
                period = pac.Period + 1, 
                context = context 
            });
        }

        // Adds one to the value for period in Alpaca
        public override void resolve()
        {
            int period = pac.Period;
            if (period == null)
            {
                period = 0;
            }
            if (period <= 0)
            {
                period = 0;
            }
            period++;
            pac.Period = period;
        }

        // ASubtracts one to the value for period in Alpaca
        public override void unresolve()
        {
            int period = pac.Period;
            period--;
            pac.Period = period;
        }

        public override string ToString()
        {
            return "Period " + pac.Period + " started.";
        }
    }
}
