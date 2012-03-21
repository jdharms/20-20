﻿using System;
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
            //empty constructor
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public string serialize()
        {
            return JsonConvert.SerializeObject(new { gameID = pac.GameID, period = pac.Period, context = pac.generateContext() });
        }

        // Adds one to the value for period in Alpaca
        public void resolve()
        {
            int period = pac.Period;
            if (period == null)
                period = 0;
            period++;
            if (period <= 0)
                period = 1;
            pac.Period = period;
        }

        // ASubtracts one to the value for period in Alpaca
        public void unresolve()
        {
            int period = pac.Period;
            period--;
            pac.Period = period;
        }

    }
}