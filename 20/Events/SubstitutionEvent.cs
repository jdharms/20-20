using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20
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
            return JsonConvert.SerializeObject(new { gameId=pac.GameID, exitingPlayer=idGoingOut, enteringPlayer=idGoingIn, context=pac.generateContext()});
        }

        /// <summary>
        /// Takes in a jason response string and does one of two things.
        ///     1. Retrieves the eventId and sets it.
        ///     2. Retrieves the error string
        /// </summary>
        /// <param name="json">The response from sending the jason object</param>
        /// <returns>false if it was an error response, otherwise true</returns>
        public override bool deserialize(string json)
        {
            // try to convert it into a SubstitutionEventResponse...
            try
            {
                //create the deserialized object into a SubstitutionEventResponse
                SubstitutionEventResponse response = JsonConvert.DeserializeObject<SubstitutionEventResponse>(json);
                Console.WriteLine("substution -- " + response.response["eventId"]);
                //set the eventId from the response
                eventId = response.response["eventId"];
                //NO ERROR!!!! :) let the user of this function know
                return true;
            }
            // if we got a NullReferenceException, that means that response.response["eventId"] did not exist
            // so treat it as an error
            catch (NullReferenceException e)
            {
                //create the deserialized object into a SubstitutionEventErrorResponse
                SubstitutionEventErrorResponse response = JsonConvert.DeserializeObject<SubstitutionEventErrorResponse>(json);
                //we now have a list of errors, go through each one
                foreach (object error in response.errors)
                {
                    // call our own parseError(string) method, and let it handle the error
                    string errorMessage = parseError(error.ToString());
                    // pac.receiveError(errorMessage)
                    Console.WriteLine("Error package -- " + errorMessage);
                }
                //too bad we got an error, tell the user of this function
                return false;
            }

        }

        /****************************************************
            Reponse classes. Used to deserialize into
        ****************************************************/

        private class SubstitutionEventResponse
        {
            public string time;
            public string request;
            public string result;
            public Dictionary<string, string> response;
        }
        
        private class SubstitutionEventErrorResponse
        {
            public string time;
            public string request;
            public string result;
            public List<object> errors;
        }
    }
}
