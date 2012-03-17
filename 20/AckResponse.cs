using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20
{
    class AckResponse
    {
        public string time;
        public string request;
        public string result;
        public Dictionary<string, string> response;

        //eventId can be found with:
        //string id = response["eventId"];

        //setGameData has "gameId" instead.
    }
}
