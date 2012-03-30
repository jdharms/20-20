using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;

namespace _20.Events
{
    public class JumpballEvent : Event
    {
        private string homePlayerId;
        private string awayPlayerId;
        private string winner;

        public JumpballEvent(Alpaca pac, string homePlayerId, string awayPlayerId, string winner, Point location)
            : base(pac)
        {
            this.homePlayerId = homePlayerId;
            this.awayPlayerId = awayPlayerId;
            this.winner = winner;
            this.location = location;
            apiCall = "jumpBall";
        }

        public override void resolve()
        {
            pac.Possesion = pac.getTeamById(pac.getPlayer(winner).TeamId) == pac.HomeTeam ? pac.AwayTeam : pac.HomeTeam ;
        }

        public override void unresolve()
        {
            //does not unresolve anything for now.
        }

        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameId = pac.GameID,
                homePlayer = homePlayerId,
                awayPlayer = awayPlayerId,
                winner = winner,
                location = convertPointToArray(location),
                context = context 
            });
        }

        public override string ToString()
        {
            return "Jumpball possesion goes to " + pac.getTeamById(pac.getPlayer(winner).TeamId);
        }
    }
}
