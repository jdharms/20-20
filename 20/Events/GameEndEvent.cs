using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle the end of the game
    class GameEndEvent : Event 
    {
        public GameEndEvent(Alpaca pac)
            : base(pac)
        {
            //empty constructor
        }
            
        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public string serialize()
        {
            return JsonConvert.SerializeObject(new { gameID = pac.GameID, context = pac.generateContext() });
        }

        // NOTHING HERE
        public void resolve()
        {
            // Empty method. Does not change model
        }

        // OR HERE
        public void unresolve()
        {
            // Empty method. Does not change model
        }

        // Has the values expected for the response
        private class GameEndEventResponse
        {
            public string time;
            public string request;
            public string result;
            public Dictionary<string, string> response;
        }

        // Has the values for the error response
        private class GameEndEventErrorResponse
        {
            public string time;
            public string request;
            public string result;
            public List<object> errors;
        }

    }
}
