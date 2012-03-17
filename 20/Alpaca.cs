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

namespace _20
{
    public delegate bool acceptCredentials(string username, string password);

    /// <summary>
    /// A wrapper class for the ESPN AlPS api.
    /// </summary>
    class Alpaca
    {
        string token;
        string secret = "gPIpZC5lxC9X3vEBZAydWCX4HKWHP7RPU1S2KP2U";
        string key = "bb5a7d5f935000f490320164827abba009029cd1";

        private string username;
        private string password;
        private bool authenticated;
        DateTime lastAuthed;

        private Team awayTeam;
        private Team homeTeam;
        private string gid;

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

            //Replace is a hack to make up for the fact that the ESPN ALPS api doesn't
            //accept "Z" as a valid GMT offset (it means +0000 from GMT).
            string startStamp = XmlConvert.ToString(start, XmlDateTimeSerializationMode.Utc).Replace("Z", "+0000");
            string endStamp = XmlConvert.ToString(end, XmlDateTimeSerializationMode.Utc).Replace("Z", "+0000");
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

        public bool getGameData(string gid)
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
                    Console.WriteLine(gameResponse);
                    return true;
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return false;
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

    }
}
