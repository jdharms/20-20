using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using _20.Events;

namespace _20
{
    class PlayerData
    {
        public string playerId;
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

        public List<Player> getPlayers()
        {
            List<Player> players = new List<Player>();
            foreach(PlayerData player in this.players)
            {
                Player p = new Player(player.playerId, player.nameArray(), player.jerseyNumber, teamId, player.isTeamPlayer);
                players.Add(p);
            }
            return players;
        }

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

    class GameSetupData
    {
        public Dictionary<string, List<string>> homeTeam;
        public Dictionary<string, List<string>> awayTeam;
        
        public List<Dictionary<string, Object>> gameEvents;

        public void setOnCourt(Team team, bool isHome)
        {
            List<string> lineup;
            if (isHome)
            {
                lineup = homeTeam["onField"];
            }
            else
            {
                lineup = awayTeam["onField"];
            }

            List<Player> onCourt = new List<Player>();
            foreach (string pid in lineup)
            {
                onCourt.Add(team.getPlayer(pid));
            }
            team.setPlayersOnCourt(onCourt);
        }


        public List<Event> getEvents(Alpaca pac)
        {
            List<Event> events = new List<Event>();
            if (gameEvents == null)
                return events;

            foreach(Dictionary<string, object> dict in gameEvents)
            {
                Event e = null;
                string eventType = dict["apiCall"].ToString();
                Console.WriteLine("Found eventType: " + eventType);
                Context context = JsonConvert.DeserializeObject<Context>(dict["context"].ToString());
                if (eventType.Equals("gameEnd"))
                {
                    e = new GameEndEvent(pac);
                }
                else if (eventType.Equals("periodStart"))
                {
                    Console.WriteLine("Found a periodStart event");
                    e = new PeriodStartEvent(pac);
                }
                else if (eventType.Equals("periodEnd"))
                {
                    e = new PeriodEndEvent(pac);
                }
                else if (eventType.Equals("madeShot"))
                {
                    /*
                    string shooter = dict["shooter"];
                    string assistedBy;  
                    dict.TryGetValue("assistedBy", out assistedBy); 
                    string shotType = dict["shotType"];
                    int 

                    e = new MadeShotEvent(pac, dict["shooter"], pac.getPlayer
                    */
                }
                else if (eventType.Equals("missedShot"))
                {
                    //
                }
                else if (eventType.Equals("jumpBall"))
                {
                    //
                }
                else if (eventType.Equals("rebound"))
                {
                    //
                }
                else if (eventType.Equals("substitution"))
                {
                    //
                }
                else if (eventType.Equals("turnover"))
                {
                    //
                }
                else if (eventType.Equals("timeout"))
                {
                    //
                }
                else if (eventType.Equals("foul"))
                {
                    //
                }

                if (e != null)
                {
                    e.resolve();
                    events.Add(e);
                }
            }
            return events;
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
        public Dictionary<string, object> response;

        [JsonIgnore]
        private GameSetupData gameSetupData;
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

        public Team HomeTeam()
        {
            Team home = new Team(homeTeamData.teamId, homeTeamData.teamName, homeTeamData.getPlayers());
            if (gameSetupData.homeTeam != null)
            {
                gameSetupData.setOnCourt(home, true);
            }
            return home;
        }

        public Team AwayTeam()
        {
            Team away = new Team(awayTeamData.teamId, awayTeamData.teamName, awayTeamData.getPlayers());
            if (gameSetupData.awayTeam != null)
            {
                gameSetupData.setOnCourt(away, false);
            }
            return away;
        }

        public List<Event> Events(Alpaca pac)
        {
            List<Event> events = gameSetupData.getEvents(pac);
            return events;
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
            awayTeamData = JsonConvert.DeserializeObject<TeamData>(response["awayTeam"].ToString());
            homeTeamData = JsonConvert.DeserializeObject<TeamData>(response["homeTeam"].ToString());
            gameSetupData = JsonConvert.DeserializeObject<GameSetupData>(response["gameSetupData"].ToString());
            //awayTeamData = response["awayTeam"];
            //homeTeamData = response["homeTeam"];
        }
    }
}
