using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    class JumpballEvent : Event
    {
        private string homePlayerId;
        private string awayPlayerId;
        private string winner;
        private Point location;

        public JumpballEvent(Alpaca pac, string homePlayerId, string awayPlayerId, string winner, Point location)
            : base(pac)
        {
            this.homePlayerId = homePlayerId;
            this.awayPlayerId = awayPlayerId;
            this.winner = winner;
            this.location = location;
        }

        public override void resolve()
        {
            //does not resolve anything for now.
        }

        public override void unresolve()
        {
            //does not unresolve anything for now.
        }

        public override string serialize()
        {
            int[] locationArray = { location.X, location.Y };
            return JsonConvert.SerializeObject(new
            {
                gameId = pac.GameID,
                homePlayer = homePlayerId,
                awayPlayer = awayPlayerId,
                winner = winner,
                location = locationArray,
                context = pac.generateContext()
            });
        }
    }
}
