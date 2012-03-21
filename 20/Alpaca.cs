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

namespace _20
{
    public delegate bool acceptCredentials(string username, string password);

    /// <summary>
    /// A wrapper class for the ESPN AlPS api.
    /// Here's a comment for a sample commit/push/pull.
    /// </summary>
    class Alpaca
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

        public string GameID
        {
            get { return gid; }
            set { gid = value; }
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
                child.ShowDialog();

                username = child.Username;
                password = child.Password;
                
                /*
                token = login(username, password);
                */
                child.failed = true;
            }
            lastAuthed = DateTime.Now;

            Console.WriteLine(token);
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
                    var responseText = streamReader.ReadToEnd();
                    authenticated = true;
                    GameDataResponse gameResponse = JsonConvert.DeserializeObject<GameDataResponse>(responseText);
                    gameResponse.flatten();
                    //gameResponse is the useful data from this call.  It has team names, player names, player numbers.
                    Console.WriteLine(gameResponse);
                    Console.WriteLine(responseText);
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
            var url = generateUrl(e.ApiCall, GameID);
            WebRequest request = WebRequest.Create(url);

            string payload = e.serialize();

            request.Method = "POST";
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
            return XmlConvert.ToString(now, XmlDateTimeSerializationMode.Utc).Replace("Z", "+0000");
        }

        private string generateTimestamp(DateTime time)
        {
            return XmlConvert.ToString(time, XmlDateTimeSerializationMode.Utc).Replace("Z", "+0000");
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

        public Context generateContext()
        {
            return new Context(HomeTeam.Score, AwayTeam.Score);
        }
    }
}
