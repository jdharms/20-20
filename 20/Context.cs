using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    public class Context
    {
        public string time;
        public int homeScore;
        public int awayScore;

        public Context()
        {
        }

        public Context(int homeScore, int awayScore)
        {
            this.homeScore = homeScore;
            this.awayScore = awayScore;
            time = Alpaca.generateTimestamp();
        }

        public Context(int homeScore, int awayScore, DateTime time)
        {
            this.homeScore = homeScore;
            this.awayScore = awayScore;
            this.time = Alpaca.generateTimestamp(time);
        }
    }
}
