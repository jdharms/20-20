using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    class GamesResponse
    {
        public string time;
        public string request;
        public string result;
        public Dictionary<string, List<Game>> response;
        string gameId;
    }
}
