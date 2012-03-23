using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    /// <summary>
    /// The event the will handle substitutions.
    /// </summary>
    class SubstitutionEvent : Event
    {
        private string idGoingIn;
        private string idGoingOut;
        private string teamId;

        public SubstitutionEvent(Alpaca pac, string idGoingIn, string idGoingOut, string teamId) : base(pac)
        {
            this.idGoingIn = idGoingIn;
            this.idGoingOut = idGoingOut;
            this.teamId = teamId;
            apiCall = "substitution";
        }

        /// <summary>
        /// Subs in the player going in for the player going out
        /// </summary>
        public override void resolve()
        {
            // retrieve the team given from the constructor
            Team subTeam = pac.getTeamById(teamId);

            // make the substituion for the player going in and the player going out
            subTeam.makeSubstitution(idGoingIn, idGoingOut);
        }

        /*
         * TODO: Situation -- A is on court, B and C are on bench
         *          This event encodes B going in for A.
         *          Subsequently, C goes in for B on different event.
         *          This event is deleted, and the unresolve will trigger:
         *          A going in for B, but B is not on the court.
         *  
         */
        /// <summary>
        /// Undoes a substitution.
        /// </summary>
        public override void unresolve()
        {
            // retrieve the team given from the constructor
            Team subTeam = pac.getTeamById(teamId);

            // Does the substitution by putting in the player that was originally going out, and vice versa
            subTeam.makeSubstitution(idGoingOut, idGoingIn);
        }

        /// <summary>
        /// Converts this class to a Json serialized object, with the correct fields.
        /// </summary>
        /// <returns>A Json serialized object.</returns>
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new 
            { 
                gameId=pac.GameID, 
                exitingPlayer=idGoingOut, 
                enteringPlayer=idGoingIn, 
                context=pac.generateContext()
            });
        }

        public override string ToString()
        {
            string playerIn = pac.getPlayer(this.idGoingIn).DisplayName;
            string playerOut = pac.getPlayer(this.idGoingOut).DisplayName;

            return "Substitution by " + pac.getTeamById(pac.getPlayer(this.idGoingOut).TeamId).Name + ". " + playerIn + " going in for " + playerOut;
        }
    }
}
