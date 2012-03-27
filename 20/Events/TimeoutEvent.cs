using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    // The event will handle timeouts
    class TimeoutEvent : Event
    {
        private string inputTeam;
        private string inputType;

        private string teamName;

        // The input is an alpaca, the team, and the type of timeout (can "team", "offical", or "media")
        public TimeoutEvent(Alpaca pac, string inputTeam, string inputType)
            : base(pac)
        {
            this.inputTeam = inputTeam;
            this.inputType = inputType;
            apiCall = "timeout";
            if (inputTeam != null)
            {
                teamName = pac.getTeamById(inputTeam).Name;
            }
            else
            {
                teamName = inputType;
            }
        }

        // Converts this class to a Json serialized 
        // returns a Json serialized object
        public override string serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                apiCall = apiCall,
                gameId = pac.GameID, 
                timeoutTeam = inputTeam, 
                timeoutType = inputType, 
                context = context 
            },
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        public override void resolve()
        {
            // The only place timeouts are being tracked is in the team
            if (inputType == "team")
            {
                // If the team is the home id, change their timeouts
                if (inputTeam == pac.HomeTeam.Id)
                {
                    // Sets a variable equal to the timeouts
                    int timeoutsUsed = pac.HomeTeam.TimeoutsUsed;
                    // Adds one more to it
                    timeoutsUsed++;
                    // And sets the team's value to the new value
                    pac.HomeTeam.TimeoutsUsed = timeoutsUsed;
                }
                // else change the Away Team's timeouts
                else{
                    // Sets a variable equal to the timeouts
                    int timeoutsUsed = pac.AwayTeam.TimeoutsUsed;
                    // Adds one more to it
                    timeoutsUsed++;
                    // And sets the team's value to the new value
                    pac.AwayTeam.TimeoutsUsed = timeoutsUsed;
                }
            }
        }

        public override void unresolve()
        {
            // Only change it back if it was team. As only values from team
            // Are used
            if (inputType == "team")
            {
                // If it was the Home Team
                if (inputTeam == pac.HomeTeam.Id)
                {
                    // sets a variable to the timeouts
                    int timeoutsUsed = pac.HomeTeam.TimeoutsUsed;
                    // decriment it
                    timeoutsUsed--;
                    // If it is less than zero, set it to zero
                    if (timeoutsUsed < 0)
                        timeoutsUsed = 0;
                    // Sets the value to the new value
                    pac.HomeTeam.TimeoutsUsed = timeoutsUsed;
                }
                // If it was the Away Team
                else
                {
                    // Sets a variable to the timeouts
                    int timeoutsUsed = pac.AwayTeam.TimeoutsUsed;
                    //decriment it
                    timeoutsUsed--;
                    // If it is less than zero, set it to zero
                    if (timeoutsUsed < 0)
                        timeoutsUsed = 0;
                    // Sets the value to the new value
                    pac.AwayTeam.TimeoutsUsed = timeoutsUsed;
                }
            }
        }

        public override string ToString()
        {
            return char.ToUpper(teamName[0]) + teamName.Substring(1) + " timeout.";
        }

    }
}
