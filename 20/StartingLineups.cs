using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20
{
    public class StartingLineups
    {
        public string time;
        public Dictionary<string, List<string>> homeTeam;
        public Dictionary<string, List<string>> awayTeam;

        [JsonIgnore]
        private List<string> homeStarters;
        [JsonIgnore]
        private List<string> awayStarters;

        public StartingLineups()
        {
            homeTeam = new Dictionary<string,List<string>>();
            awayTeam = new Dictionary<string,List<string>>();
            homeStarters = new List<string>();
            awayStarters = new List<string>();
        }

        public bool pack(string timeStamp)
        {
            homeTeam["onField"] = homeStarters;
            awayTeam["onField"] = awayStarters;
            time = timeStamp;
            return true;
        }

        /// <summary>
        /// Adds a player to the StartingLineups object.
        /// Does *not* make sure only five are set.
        /// </summary>
        /// <param name="isHome">true iff the player plays for the home team.</param>
        /// <param name="playerId">The unique id for the player</param>
        /// <returns>true on success</returns>
        public bool addStarter(bool isHome, string playerId)
        {
            if (playerId == null)
            {
                return false;
            }
            if(isHome)
            {
                homeStarters.Add(playerId);
            }
            else
            {
                awayStarters.Add(playerId);
            }
            return true;
        }
    }
}
