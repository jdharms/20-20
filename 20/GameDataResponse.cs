using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace _20
{
    class PlayerData
    {
        public string playerId;
        public string teamId;
        public Dictionary<string, string> name;
        public int jerseyNumber;
        public bool isTeamPlayer;

        public override string ToString()
        {
            if (isTeamPlayer)
            {
                return "Team Player";
            }
            return name["lastName"] + ", " + name["firstName"] +
                " -- " + jerseyNumber;
        }

        public string[] nameArray()
        {
            string[] nameAry = {name["firstName"], name["middleName"], name["lastName"] };
            return nameAry;
        }
    }

    class TeamData
    {
        public string teamName;
        public string teamId;
        public List<PlayerData> players;

        public override string ToString()
        {
            var ret = "";
            ret = ret + teamName + "\n";
            foreach (PlayerData player in players)
            {
                ret = ret + "\t" + player + "\n";
            }
            return ret;
        }
    }


    class GameDataResponse
    {
        public string time;
        public string request;
        public string result;

        //TODO: Response needs to be changed from a Dictionary<string, TeamData>
        //to a Dictionary<string, string>.  Individual teams can be parsed,
        //followed by any gameSetupData.
        public Dictionary<string, TeamData> response;
        private Dictionary<string, string> gameSetupData;

        [JsonIgnore]
        private TeamData awayTeamData;
        [JsonIgnore]
        private TeamData homeTeamData;

        public TeamData AwayTeamData 
        {
            get { return awayTeamData; }
        }

        public TeamData HomeTeamData
        {
            get { return homeTeamData; }
        }


        public override string ToString()
        {
            flatten();
            return homeTeamData.ToString() + awayTeamData.ToString();
        }

        //This method takes the two TeamData objects out of the 
        //rather inconvenient dictionary that they're packaged in.
        public void flatten()
        {
            //awayTeamData = JsonConvert.DeserializeObject<TeamData>(response["awayTeam"]);
            //homeTeamData = JsonConvert.DeserializeObject<TeamData>(response["homeTeam"]);
            awayTeamData = response["awayTeam"];
            homeTeamData = response["homeTeam"];

            //if (response.ContainsKey("gameSetupData"))
           // {
            //    gameSetupData = JsonConvert.DeserializeObject<Dictionary<string, string>>(response["gameSetupData"]);
            //}

        }
    }
}
