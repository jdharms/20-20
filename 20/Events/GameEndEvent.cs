﻿using System;
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

    }
}