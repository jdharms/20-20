using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Dictionary<string, TeamData> response;

        public override string ToString()
        {
            return response["homeTeam"].ToString() + response["awayTeam"].ToString();
        }
    }
}
