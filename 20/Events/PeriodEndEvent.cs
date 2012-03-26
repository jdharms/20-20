using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle the end of a period
    public class PeriodEndEvent : Event
    {
        int period;

        public PeriodEndEvent(Alpaca pac)
            : base(pac)
        {
            apiCall = "periodEnd";
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameId = pac.GameID, 
                period = pac.Period, 
                context = context 
            });
        }

        // Not a soul found in these parts
        public override void resolve()
        {
            period = pac.Period;
            pac.Period++;
            pac.InsidePeriod = false;
        }

        // Not here either
        public override void unresolve()
        {
            pac.InsidePeriod = true;
        }

        public override string ToString()
        {
            return (period > 2 ? "Overtime " + (period - 2) : "Period " + period) + " ended.";
        }
    }
}
