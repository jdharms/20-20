using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle timeouts
    class TimeoutEvent : Event
    {
        private string inputTeam;
        private string inputType;


        public TimeoutEvent(Alpaca pac, string inputTeam, string inputType)
            : base(pac)
        {
            this.inputTeam = inputTeam;
            this.inputType = inputType;
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public string serialize()
        {
            return JsonConvert.SerializeObject(new { gameID = pac.GameID, timeoutTeam = inputTeam, timeoutType = inputType, context = pac.generateContext() });
        }

        // Not a soul found in these parts
        public void resolve()
        {
            if (inputType == "team")
            {
                if (inputTeam == pac.HomeTeam.Id)
                {
                    int timeoutsLeft = pac.HomeTeam.TimeOutsLeft;
                    timeoutsLeft--;
                    pac.HomeTeam.TimeOutsLeft = timeoutsLeft;
                }
                else{

                }
            }
        }

        // Not here either
        public void unresolve()
        {
            // Empty Method
        }

        // Has the values expected for the response
        private class TimeoutEventResponse
        {
            public string time;
            public string request;
            public string result;
            public Dictionary<string, string> response;
        }

        // Has the values for the error response
        private class TimeoutEventErrorResponse
        {
            public string time;
            public string request;
            public string result;
            public List<object> errors;
        }

    }
}
