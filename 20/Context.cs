﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    class Context
    {
        public string time;
        public int homeScore;
        public int awayScore;

        public Context(int homeScore, int awayScore)
        {
            this.homeScore = homeScore;
            this.awayScore = awayScore;
            time = Alpaca.generateTimestamp();
        }
    }
}