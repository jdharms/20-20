using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace _20
{
    public class Game
    {
        public Dictionary<string, string> homeTeam;
        public Dictionary<string, string> awayTeam;
        public string venue;
        public string time;
        public string gameType;
        public string gameId;

        override public string ToString()
        {
            return awayTeam["teamName"] + " at " + homeTeam["teamName"] + ", " + XmlConvert.ToDateTime(time);
        }

    }
}
