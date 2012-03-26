using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using _20.Events;
using System.Windows.Forms;
using System.ComponentModel;

namespace _20
{
    public delegate bool acceptCredentials(string username, string password);
    public delegate List<Game> GameGetter(DateTime start, DateTime end);

    /// <summary>
    /// A wrapper class for the ESPN AlPS api.
    /// Here's a comment for a sample commit/push/pull.
    /// </summary>
    public class Alpaca
    {
        string token;
        private string secret = "gPIpZC5lxC9X3vEBZAydWCX4HKWHP7RPU1S2KP2U";
        private string key = "bb5a7d5f935000f490320164827abba009029cd1";

        private string username;
        public string Username { get { return username; } }
        private string password;
        private bool authenticated;
        DateTime lastAuthed;

        public int Period { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        private string gid;
        private List<Event> eventLog;
        public List<Event> EventLog { get { return eventLog; } }

        public string GameID
        {
            get { return gid; }
            set { gid = value; }
        }

        public delegate void StateChangeHandler();

        private event StateChangeHandler onChange;

        //OnStateChange allows a way for a Form to be notified when Alpaca changes the state of the game.
        //forms can register a method with pac.OnStateChange += methodName.
        public event StateChangeHandler OnStateChange
        {
            add
            {
                onChange += value;
            }
            remove
            {
                onChange -= value;
            }
        }

        private void Notify()
        {
            if (onChange != null)
            {
                onChange();
            }
        }

        /// <summary>
        /// Currently, the constructor for Alpaca loads the LoginForm and asks for
        /// username/passsword pairs until it finds a good one.  It would be nice to
        /// de-couple this behavior, and have the username/password be passed in.
        /// 
        /// One idea would be to have the constructor be empty, and have the bool state
        /// variable "authenticated" default to false.  Have a public method in Alpaca 
        /// "authenticate" that takes a username and password and returns true if login
        /// succeeds.  Save username/password and note authentication.
        /// </summary>
        public Alpaca()
        {
            LoginForm child = new LoginForm();
            while (!authenticated)
            {
                child.login = new acceptCredentials(this.login);
                child.StartPosition = FormStartPosition.CenterScreen;
                child.ShowDialog();

                username = child.Username;
                password = child.Password;
                
                /*
                token = login(username, password);
                */
                child.failed = true;
            }
            lastAuthed = DateTime.Now;

            eventLog = new List<Event>();

            GameSelectForm selectForm = new GameSelectForm();
            selectForm.getGames = new GameGetter(this.getGames);
            while (true)
            {
                selectForm.StartPosition = FormStartPosition.CenterScreen;
                selectForm.ShowDialog();
                if (selectForm.selected)
                {
                    gid = selectForm.selectedGameId;
                    break;
                }

                List<Game> games = getGames(selectForm.from, selectForm.to);
                Console.Write(selectForm.from);
                Console.Write(selectForm.to);
                Console.Write(games.Count);
                //selectForm.gameBox.DataSource = games;
            }

            getGameData(gid);

            // if one is empty, both are empty
            if (HomeTeam.getOncourt() == null || HomeTeam.getOncourt().Count == 0)
            {
                SetupGameForm setup = new SetupGameForm();
                setup.homeTeam = new BindingList<Player>();
                foreach (Player p in HomeTeam.getBench())
                {
                    if (!p.TeamPlayer)
                    {
                        setup.homeTeam.Add(p);
                    }
                }

                setup.awayTeam = new BindingList<Player>();
                foreach (Player p in AwayTeam.getBench())
                {
                    if (!p.TeamPlayer)
                    {
                        setup.awayTeam.Add(p);
                    }
                }
                setup.homeStarters = new BindingList<Player>(); 
                setup.awayStarters = new BindingList<Player>(); 
                setup.homeTeamName = HomeTeam.Name;
                setup.awayTeamName = AwayTeam.Name;
                setup.StartPosition = FormStartPosition.CenterScreen;
                setup.ShowDialog();

                HomeTeam.setPlayersOnCourt(setup.homeStarters.ToList<Player>());
                AwayTeam.setPlayersOnCourt(setup.awayStarters.ToList<Player>());

                StartingLineups s = new StartingLineups();

                foreach (Player p in HomeTeam.getOncourt())
                {
                    s.addStarter(true, p.Id);
                }

                foreach (Player p in AwayTeam.getOncourt())
                {
                    s.addStarter(false, p.Id);
                }

                setGameData(s);
            }

            
            Console.WriteLine(token);
        }

        public Player getPlayer(string playerId)
        {
            if (HomeTeam == null || AwayTeam == null)
            {
                return null;
            }
            if (HomeTeam.getPlayer(playerId) != null)
            {
                return HomeTeam.getPlayer(playerId);
            }
            else if (AwayTeam.getPlayer(playerId) != null)
            {
                return AwayTeam.getPlayer(playerId);
            }
            return null;
        }



        /// <summary>
        /// Submits a login request to the ESPN ALPS server.
        /// Stores the authorization token in state variable
        /// "token" upon success.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>True iff login is successful.</returns>
        private bool login(string username, string password)
        {
            var url = "https://api.espnalps.com/login?signature=" + generateSignature(key, secret) + "&key=" + key;
            WebRequest request = WebRequest.Create(url);

            string payload = JsonConvert.SerializeObject(new { username = username, password = password });

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;
            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    authenticated = true;
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseText);
                    loginResponse.response.TryGetValue("token", out token);
                    lastAuthed = DateTime.Now;
                    return true;
                }
            }
            //TODO: Deserialize error response and print/display useful information.
            catch (WebException e)
            {
                using (var response = e.Response)
                {
                    Console.WriteLine(e.Message);
                    using (Stream data = response.GetResponseStream())
                    {
                        string responseText = new StreamReader(data).ReadToEnd();
                        Console.WriteLine(responseText);
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Queries the ESPN ALPS administration module for a listing of games in a time range.
        /// </summary>
        /// <param name="start">Beginning of time range.</param>
        /// <param name="end">End of range.</param>
        /// <returns>A list of games between <paramref name="start"/> and <paramref name="end"/>. List may be empty.</returns>
        public List<Game> getGames(DateTime start, DateTime end)
        {
            List<Game> games = new List<Game>();

            var url = generateUrl("games");
            WebRequest request = WebRequest.Create(url);

            string startStamp = generateTimestamp(start);
            string endStamp = generateTimestamp(end);
            string payload = JsonConvert.SerializeObject(new { start = startStamp, end = endStamp });

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    authenticated = true;
                    GamesResponse gameResponse = JsonConvert.DeserializeObject<GamesResponse>(responseText);
                    gameResponse.response.TryGetValue("games", out games);

                    Console.WriteLine(responseText);
                    foreach(Game game in games)
                    {
                        Console.WriteLine(game);
                    }

                    return games;
                }
            }
            //TODO: Deserialize error response and print/display useful information.
            catch (WebException e)
            {
                using (var response = e.Response)
                {
                    Console.WriteLine(e.Message);
                    using (Stream data = response.GetResponseStream())
                    {
                        string responseText = new StreamReader(data).ReadToEnd();
                        Console.WriteLine(responseText);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Retrieves teams/players for given game id.
        /// @TODO: Change to return some useful type...
        /// </summary>
        /// <param name="gid">Id of game to get data.</param>
        /// <returns>Nothing useful.</returns>
        public GameDataResponse getGameData(string gid)
        {
            string url = generateUrl("getGameData", gid);
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = streamReader.ReadToEnd();
                    //Console.WriteLine(responseText);

                    /* We need to deserialize & parse any game setup data into events. */
                    GameDataResponse gameResponse = JsonConvert.DeserializeObject<GameDataResponse>(responseText);
                    gameResponse.flatten();
                    //gameResponse is the useful data from this call.  It has team names, player names, player numbers.
                    Console.WriteLine(gameResponse);

                    HomeTeam = gameResponse.HomeTeam();
                    AwayTeam = gameResponse.AwayTeam();
                    eventLog = gameResponse.Events(this);

                    foreach (Event e in eventLog)
                    {
                        e.resolve();
                    }
                    Notify();


                    //this is a lot of information... printing it all slows down the thread!  turn on only if necessary.
                    //Console.WriteLine(responseText);
                    return gameResponse;
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return null;
            }
        }

        public bool post(Event e)
        {
            var url = generateUrl(e.ApiCall);
            WebRequest request = WebRequest.Create(url);

            string payload = e.serialize();

            Console.WriteLine("-------------------");
            Console.WriteLine(url);
            Console.WriteLine(payload);
            Console.WriteLine("------------------");

            request.Method = "POST";
            if (e is DeleteEvent)
            {
                request.Method = "DELETE";
            }
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;
            using ( var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
            }

            string responseText;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                using (var response = we.Response)
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        responseText = new StreamReader(data).ReadToEnd();
                    }
                }
            }
            if (e.deserialize(responseText))
            {
                e.resolve();
                if (e is DeleteEvent)
                {
                    //We need to remove the deleted event.
                    Notify();
                }
                else
                {
                    eventLog.Add(e);
                    eventLog.Sort();
                    Notify();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public string setGameData(StartingLineups lineups)
        {
            var url = generateUrl("setGameData", GameID);

            WebRequest request = WebRequest.Create(url);

            lineups.pack(generateTimestamp());
            string payload = JsonConvert.SerializeObject(lineups);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = payload.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    AckResponse ack = JsonConvert.DeserializeObject<AckResponse>(responseText);
                    //TODO: Need to change this to a "try for key".  Error messages will 
                    //make it crash.
                    string gameId = ack.response["gameId"];

                    Console.WriteLine(responseText);
                    return gameId;
                }
            }
            //TODO: deserialize error response
            catch (WebException e)
            {
                using (var response = e.Response)
                {
                    Console.WriteLine(e.Message);
                    using (Stream data = response.GetResponseStream())
                    {
                        string responseText = new StreamReader(data).ReadToEnd();
                        Console.WriteLine(responseText);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Generates a signature from the shared secret and private key.
        /// Signature is good for five minutes, but should be recalculated at
        /// every use to ensure a valid signature.
        /// </summary>
        /// <param name="key">Access key</param>
        /// <param name="secret">Shared secret</param>
        /// <returns></returns>
        private static string generateSignature(string key, string secret)
        {
            //need current unix time for generating signature
            int unixTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            string cat = key + secret + unixTime.ToString();
            using (MD5 md5hash = MD5.Create())
            {
                return GetMd5Hash(md5hash, cat);
            }
        }

        /*
         * Shamelessly ripped from http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx
         */
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        //helper methods to generate urls for different api calls.
        private string generateUrl(string method)
        {
            string ret = "http://api.espnalps.com/v0/cbb/" + method +
            "?token=" + token + "&signature=" + generateSignature(key, secret) + "&key=" + key;

            return ret;
        }

        private string generateUrl(string method, string gameID)

        {
            string ret = "http://api.espnalps.com/v0/cbb/" + method + "/" + gameID +
            "?token=" + token + "&signature=" + generateSignature(key, secret) + "&key=" + key;

            return ret;
        }

        public static string generateTimestamp()
        {
            DateTime now = DateTime.Now;
           // return XmlConvert.ToString(now, XmlDateTimeSerializationMode.Utc).Replace("Z", "-0000");
            string str = XmlConvert.ToString(now, XmlDateTimeSerializationMode.Local);
            str = XmlConvert.ToString(now, "yyyy-MM-ddTHH:mm:ss.fffzzz");

            return str.Remove(str.Length - 3, 1);

        }

        public static string generateTimestamp(DateTime time)
        {
            string str = XmlConvert.ToString(time, "yyyy-MM-ddTHH:mm:ss.fffzzz");

            return str.Remove(str.Length - 3, 1);
        }

        public Team getTeamById(string teamId)
        {
            if(HomeTeam.Id.Equals(teamId))
            {
                return HomeTeam;
            }
            else
            {
                return AwayTeam;
            }
        }

        public Player getPlayerByNumber(bool isHome, int number)
        {
            Team team = null;
            if(isHome)
            {
                team = HomeTeam;
            }
            else
            {
                team = AwayTeam;
            }
           
            foreach (Player p in team.Players) 
            {
                if (p.Jersey == number)
                    return p;
            }

            return null;
        }

        public Context generateContext()
        {
            return new Context(HomeTeam.Score, AwayTeam.Score);
        }
    }
}
