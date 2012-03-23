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
        string shotType;
        int points;
        bool fastBreak;
        Point location;
        string teamId;

        //variables for toString()
        string shooterName;
        string blockerName;
        string shotValue;

        public MissedShotEvent(Alpaca pac, string shooterId, string teamId, string blockerId, string shotType, int pointsAttempted, 
            bool fastBreakOpportunity, Point location)
            : base(pac)
        {
            this.shooter = shooterId;
            this.blocker = blockerId;
            this.shotType = shotType;
            this.points = pointsAttempted;
            this.fastBreak = fastBreakOpportunity;
            this.location = location;
            apiCall = "missedShot";

            shooterName = pac.getPlayer(shooterId).DisplayName;
            if (blockerId != null)
            {
                blockerName = pac.getPlayer(blockerId).DisplayName;
            }
            switch (pointsAttempted)
            {
                case 1:
                case 2:
                    shotValue = "";
                    break;
                case 3:
                    shotValue = "Three Point ";
                    break;
            }
        }

        public override string serialize()
        {
            int[] locArray = { location.X, location.Y };
            return JsonConvert.SerializeObject(new
            {
                gameId = pac.GameID,
                shooter = shooter,
                blockedBy = blocker,
                shotType = shotType,
                pointsAttempted = points,
                fastBreakOpportunity = fastBreak,
                location = locArray,
                context = pac.generateContext()
            },
            Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
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

        public override string ToString()
        {
            string blockString = "";
            if(blocker != null)
            {
                blockString = " Block by " + blockerName + ".";
            }

            return shooterName + " missed " + shotValue + shotType + "." + blockString;
        }
    }
}
