﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    public enum ShotType { jumpshot, layup, dunk, tipin, freethrow };

    class MadeShotEvent : Event
    {
        string shooter;
        string assist;
        string shot;
        int points;
        bool fastBreak;
        bool goaltending;
        Point location;
        string teamId;

        public MadeShotEvent(Alpaca pac, string shooterId, string teamId, string assistId, string shotType, int pointsScored, bool fastBreakOpportunity,
                        bool goaltending, Point location)
            : base(pac)
        {
            this.shooter = shooterId;
            this.assist = assistId;
            this.teamId = teamId;
            this.shot = shotType;
            this.points = pointsScored;
            this.fastBreak = fastBreakOpportunity;
            this.goaltending = goaltending;
            this.location = location;
        }

        public override void resolve()
        {
            //Add the points to the shooter's team.
            pac.getTeamById(teamId).Score += points; 
        }

        public override void unresolve()
        {
            //Take those points away.
            pac.getTeamById(teamId).Score -= points;
        }

        public override string serialize()
        {
            int[] locArray = {location.X, location.Y};
            return JsonConvert.SerializeObject(new
            {
                gameId = pac.GameID,
                shooter = shooter,
                assistedBy = assist,
                shotType = shot,
                pointsScored = points,
                fastBreakOpportunity = fastBreak,
                goaltending = goaltending,
                location = locArray,
                context = pac.generateContext()
            }
            );
        }

    }
}