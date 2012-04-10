using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace _20.Events
{
    // The event will handle the start of a period
    public class PeriodStartEvent : Event
    {
        int period;

        public PeriodStartEvent(Alpaca pac)
            : base(pac)
        {
            apiCall = "periodStart";
            period = pac.Period;
            this.location = new Point(-1, -1);
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                gameId = pac.GameID, 
                period = pac.Period, 
                context = context 
            });
        }

        // Adds one to the value for period in Alpaca
        public override void resolve()
        {
            pac.InsidePeriod = true;
        }

        // ASubtracts one to the value for period in Alpaca
        public override void unresolve()
        {
            if (pac.Period > 1)
            {
                pac.Period--;
            }
            pac.InsidePeriod = false;
        }

        public override string ToString()
        {
            return (period > 2 ? "Overtime " + (period - 2) : "Period " + period ) + " started.";
        }
    }
}
