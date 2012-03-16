using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.IO;

namespace _20
{
    class Alpaca
    {
        string token;
        string secret = "gPIpZC5lxC9X3vEBZAydWCX4HKWHP7RPU1S2KP2U";
        string signature;
        string key = "bb5a7d5f935000f490320164827abba009029cd1";

        private string username;
        private string password;

        public Alpaca()
        {
            LoginForm child = new LoginForm();
            child.ShowDialog();

            username = child.Username;
            password = child.Password;

            Console.WriteLine(login(username, password));
        }

        private string login(string username, string password)
        {
            var url = "https://api.espnalps.com/login?signature=" + generateSignature(key, secret) + "&key=" + key;
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string payload = JsonConvert.SerializeObject(new { username = username, password = password });
                streamWriter.Write(payload);
            }

            try
            {
                var response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    return responseText;
                }
            }
            catch (WebException)
            {
                Console.WriteLine("Authentication failed.");
                return null;
            }

        }


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
    }
}
