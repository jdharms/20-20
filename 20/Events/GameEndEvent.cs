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
            apiCall = "gameEnd";
        }
            
        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameID = pac.GameID, 
                context = context 
            });
        }

        // NOTHING HERE
        public override void resolve()
        {
            // Empty method. Does not change model
        }

        // OR HERE
        public override void unresolve()
        {
            // Empty method. Does not change model
        }

        public override string ToString()
        {
            return "End game";
        }

    }
}
