using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20.Events
{
    public class EventFilter
    {
        /*
        private static Dictionary<string, Func<Event, bool>> isA;

        private static void init()
        {
            isA = new Dictionary<string, Func<Event, bool>>();
            isA.Add("DeleteEvent", x => x is DeleteEvent);
            isA.Add("FoulEvent", x => x is FoulEvent);
            isA.Add("GameEndEvent", x => x is GameEndEvent);
            isA.Add("JumpballEvent", x => x is JumpballEvent);
            isA.Add("MadeShotEvent", x => x is MadeShotEvent);
            isA.Add("MissedShotEvent", x => x is MissedShotEvent);
            isA.Add("PeriodEndEvent", x => x is PeriodEndEvent);
            isA.Add("PeriodStartEvent", x => x is PeriodStartEvent);
            isA.Add("ReboundEvent", x => x is ReboundEvent);
            isA.Add("SubstitutionEvent", x => x is SubstitutionEvent);
            isA.Add("TimeoutEvent", x => x is TimeoutEvent);
            isA.Add("TurnoverEvent", x => x is TurnoverEvent);
        }

        private static List<Event> filterByEventType(List<string> types, List<Event> events)
        {
            List<Event> filtered = new List<Event>();
            foreach (Event e in events)
            {
                foreach (string s in types)
                {
                    if (isA[s](e))
                    {
                        filtered.Add(e);
                        break;
                    }
                }
            }

            return filtered;
        }

        private static List<Event> filterByTeams(List<Team> teams, List<Event> events)
        {
            List<Event> filtered = new List<Event>();
            foreach (Event e in events)
            {
                foreach (Team team in teams)
                {
                    if (e.involvesId(team.Id))
                    {
                        filtered.Add(e);
                        break;
                    }
                }
            }
            return filtered;
        }

        private static List<Event> filterByPlayers(List<Player> players, List<Event> events)
        {
            List<Event> filtered = new List<Event>();
            foreach (Event e in events)
            {
                foreach (Player p in players)
                {
                    if (e.involvesId(p.Id))
                    {
                        filtered.Add(e);
                        break;
                    }
                }
            }
            return filtered;
        }

        public static List<Event> filterByString(string s, List<Event> events)
        {
            List<Event> filtered = new List<Event>();
            foreach (Event e in events)
            {
                if (e.ToString().ToLower().Contains(s.ToLower()))
                {
                    filtered.Add(e);
                }
            }
            return filtered;
        }

        public static List<Event> applyFilter(Filter filter, List<Event> events)
        {
            if(isA == null)
            {
                init();
            }
            List<Event> filtered = events;
            if(filter.ByTeam)
            {
                filtered = filterByTeams(filter.Teams, filtered);
            }

            if (filter.ByPlayers)
            {
                filtered = filterByPlayers(filter.Players, filtered);
            }

            if (filter.ByTypes)
            {
                filtered = filterByEventType(filter.Types, filtered);
            }

            return filtered;
        }
    }

    public class Filter
    {
        private List<Team> teams;
        public List<Team> Teams { get { return teams; } }
        private List<Player> players;
        public List<Player> Players { get { return players; } }
        private List<string> types;
        public List<string> Types { get { return types; } }
        public bool ByTeam { get { return teams.Count > 0; } }
        public bool ByPlayers { get { return players.Count > 0; } }
        public bool ByTypes { get { return types.Count > 0; } }

        public Filter()
        {
            this.teams = new List<Team>();
            this.players = new List<Player>();
            this.types = new List<string>();
        }

        public void addTeamToFilter(Team team)
        {
            if (!teams.Contains(team))
            {
                teams.Add(team);
            }
        }

        public void removeTeamFromFilter(Team team)
        {
            if (teams.Contains(team))
            {
                teams.Remove(team);
            }
        }

        public void clearTeamsFilter()
        {
            teams.Clear();
        }

        public void addPlayerToFilter(Player player)
        {
            if (!players.Contains(player))
            {
                players.Add(player);
            }
        }

        public void removePlayerFromFilter(Player player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }

        public void clearPlayersFilter()
        {
            players.Clear();
        }

        public void addTypeToFilter(string type)
        {
            if (!types.Contains(type))
            {
                types.Add(type);
            }
        }
        
        public void removePlayerFromFilter(string type)
        {
            if (types.Contains(type))
            {
                types.Remove(type);
            }
        }

        public void clearTypesFilter()
        {
            types.Clear(); 
        }

        public void clearAllFilters()
        {
            this.clearTeamsFilter();
            this.clearPlayersFilter();
            this.clearTypesFilter();
        }
        */
    }
}
