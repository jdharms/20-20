using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using _20.Events;
using System.Drawing;

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
                    
                    string shooter = dict["shooter"].ToString();
                    object assisted;  
                    dict.TryGetValue("assistedBy", out assisted);

                    string assistedBy = null;
                    if (assisted != null) 
                        assistedBy = assisted.ToString();
                    string shotType = dict["shotType"].ToString();
                    int pointsScored = int.Parse(dict["pointsScored"].ToString());
                    bool fastBreak = bool.Parse(dict["fastBreakOpportunity"].ToString());
                    bool goaltending = bool.Parse(dict["goaltending"].ToString());
                    int[] location = JsonConvert.DeserializeObject<int[]>(dict["location"].ToString());
                    Point locPt = new Point(location[0], location[1]);

                    Console.WriteLine("made shot");
                    Console.WriteLine(shooter);
                    Console.WriteLine(location[0] + " " + location[1]);

                    e = new MadeShotEvent(pac, shooter, pac.getPlayer(shooter).TeamId, assistedBy, shotType, pointsScored, fastBreak,
                        goaltending, locPt);
                }
                else if (eventType.Equals("missedShot"))
                {
                    string shooter = dict["shooter"].ToString();
                    object blocked;
                    dict.TryGetValue("blockedBy", out blocked);

                    string blockedBy = null;
                    if (blocked != null)
                        blockedBy = blocked.ToString();

                    string shotType = dict["shotType"].ToString();
                    int pointsAttempted = int.Parse(dict["pointsAttempted"].ToString());
                    bool fastBreak = bool.Parse(dict["fastBreakOpportunity"].ToString());
                    int[] location = JsonConvert.DeserializeObject<int[]>(dict["location"].ToString());
                    Point locPt = new Point(location[0], location[1]);

                    e = new MissedShotEvent(pac, shooter, pac.getPlayer(shooter).TeamId, blockedBy, shotType, pointsAttempted, fastBreak, locPt);

                }
                else if (eventType.Equals("jumpBall"))
                {
                    string homePlayer = dict["homePlayer"].ToString();
                    string awayPlayer = dict["awayPlayer"].ToString();
                    string winner = dict["winner"].ToString();
                    int[] location = JsonConvert.DeserializeObject<int[]>(dict["location"].ToString());
                    Point locPt = new Point(location[0], location[1]);

                    e = new JumpballEvent(pac, homePlayer, awayPlayer, winner, locPt);

                }
                else if (eventType.Equals("rebound"))
                {
                    object rebound;
                    dict.TryGetValue("rebounder", out rebound);

                    string rebounder = null;
                    if (rebound != null)
                    {
                        rebounder = rebound.ToString();
                    }

                    string reboundType = dict["reboundType"].ToString();

                    int[] location = JsonConvert.DeserializeObject<int[]>(dict["location"].ToString());
                    Point locPt = new Point(location[0], location[1]);

                    e = new ReboundEvent(pac, rebounder, reboundType, locPt);

                }
                else if (eventType.Equals("substitution"))
                {
                    string exitingPlayer = dict["exitingPlayer"].ToString();
                    string enteringPlayer = dict["enteringPlayer"].ToString();

                    e = new SubstitutionEvent(pac, enteringPlayer, exitingPlayer, pac.getPlayer(enteringPlayer).TeamId);
                }
                else if (eventType.Equals("turnover"))
                {
                    string committedBy = dict["committedBy"].ToString();
                    object forced;
                    dict.TryGetValue("forcedBy", out forced);

                    string forcedBy = null;
                    if (forced != null)
                    {
                        forcedBy = forced.ToString();
                    }

                    string turnoverType = dict["turnoverType"].ToString();
                    int[] location = JsonConvert.DeserializeObject<int[]>(dict["location"].ToString());
                    Point locPt = new Point(location[0], location[1]);

                    e = new TurnoverEvent(pac, committedBy, forcedBy, turnoverType, locPt);
                }
                else if (eventType.Equals("timeout"))
                {
                    object team;

                    dict.TryGetValue("timeoutTeam", out team);

                    string timeoutTeam = null;
                    if (team != null)
                    {
                        timeoutTeam = team.ToString();
                    }

                    string timeoutType = dict["timeoutType"].ToString();

                    e = new TimeoutEvent(pac, timeoutTeam, timeoutType);

                }
                else if (eventType.Equals("foul"))
                {
                    string committedBy = dict["committedBy"].ToString();

                    object drew;
                    dict.TryGetValue("drewBy", out drew);

                    string drewBy = null;
                    if (drew != null)
                    {
                        drewBy = drew.ToString();
                    }

                    string foulType = dict["foulType"].ToString();
                    bool ejected = bool.Parse(dict["ejected"].ToString());
                    int[] location = JsonConvert.DeserializeObject<int[]>(dict["location"].ToString());
                    Point locPt = new Point(location[0], location[1]);

                    e = new FoulEvent(pac, pac.getPlayer(committedBy).TeamId, committedBy, drewBy, foulType, ejected, locPt);

                }

                if (e != null)
                {
                    e.setContext(context);
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
