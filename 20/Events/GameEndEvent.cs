using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    class GameEndEvent : Event 
    {
        public GameEndEvent(Alpaca pac)
            : base(pac)
        {
            //empty constructor
        }
            

        public string serialize()
        {

            string payload = JsonConvert.SerializeObject(
            return null;
        }

        public bool deserialize(string json)
        {


            return false;
        }

        public void resolve()
        {



        }

        public void unresolve()
        {



        }


    }
}
