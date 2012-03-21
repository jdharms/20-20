using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    class MissedShotEvent : Event
    {
        string shooter;
        string blocker;
        string shot;
        int points;
        bool fastBreak;
        Point location;
        string teamId;

        public MissedShotEvent(Alpaca pac, string shooterId, string teamId, string blockerId, string shotType, int pointsAttempted, 
            bool fastBreakOpportunity, Point location)
            : base(pac)
        {
            this.shooter = shooterId;
            this.blocker = blockerId;
            this.shot = shotType;
            this.points = pointsAttempted;
            this.fastBreak = fastBreakOpportunity;
            this.location = location;
        }

        public override string serialize()
        {
            int[] locArray = { location.X, location.Y };
            return JsonConvert.SerializeObject(new
            {
                gameId = pac.GameID,
                shooter = shooter,
                blockedBy = blocker,
                shotType = shot,
                pointsAttempted = points,
                fastBreakOpportunity = fastBreak,
                location = locArray,
                context = pac.generateContext()
            }
            );
        }

        public override void resolve()
        {
            //No state changes for a missed shot.
        }

        public override void unresolve()
        {
            //No state changes for a missed shot.
        }
    }
}
