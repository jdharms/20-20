using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace _20.Events
{
    /// <summary>
    /// The Event abstract class. Any specific Events will extend this class and have to implement 4 methods:
    ///     serialize, deserialize, resolve and unresolve.
    ///     
    /// Every Event must take in a Alpaca object.
    /// </summary>
    abstract class Event
    {
        protected string apiCall;
        public string ApiCall { get { return apiCall; } }
        protected string eventId;
        public string EventId { get { return eventId; } }
        public bool ReceivedByServer { get { return eventId != null; } }

        protected Alpaca pac;
        
        public Event(Alpaca pac)
        {
            this.pac = pac;
        }

        /// <summary>
        /// Resolves any actions that need to be performed by the event
        /// </summary>
        public abstract void resolve();
        /// <summary>
        /// Undoes any resolved events. 
        /// </summary>
        public abstract void unresolve();
        /// <summary>
        /// Serialize the event into a JSON object. 
        /// </summary>
        /// <returns> The serialized event object as a string </returns>
        public abstract string serialize();

        /// <summary>
        /// Takes in a jason response string and does one of two things.
        ///     1. Retrieves the eventId and sets it.
        ///     2. Retrieves the error string
        /// </summary>
        /// <param name="json">The response from sending the jason object</param>
        /// <returns>false if it was an error response, otherwise true</returns>
        public virtual bool deserialize(string json)
        {
            try
            {
                EventResponse response = JsonConvert.DeserializeObject<EventResponse>(json);
                eventId = response.response["eventId"];
                Console.WriteLine("Event -- " + eventId);
                return true;
            }
            catch (NullReferenceException e)
            {
                EventErrorResponse response = JsonConvert.DeserializeObject<EventErrorResponse>(json);
                foreach (object error in response.errors)
                {
                    string errorMessage = parseError(error.ToString());
                    Console.WriteLine(errorMessage);
                }

                return false;
            }
        }

        /// <summary>
        /// Takes in a error as a string, and parses it out.
        /// </summary>
        /// <param name="error">The error to parse</param>
        /// <returns>The string form of the error</returns>
        protected string parseError(string error)
        {

            try
            {
                Dictionary<string, string> fieldError = JsonConvert.DeserializeObject<Dictionary<string, string>>(error);
                return "field:" + fieldError["field"] + ", type:" + fieldError["type"] + ", message:" + fieldError["message"];
            }
            catch (Exception e)
            {
                return error;
            }
        }

        /// <summary>
        /// Converts a Point to a two-element array
        /// </summary>
        /// <param name="p">The point to convert</param>
        /// <returns>A two-element array</returns>
        protected int[] convertPointToArray(Point p)
        {
            int[] location = {p.X, p.Y};
            return location;
        }

        /****************************************************
                Reponse classes. Used to deserialize into
        ****************************************************/
        private class EventResponse
        {
            public string time;
            public string request;
            public string result;
            public Dictionary<string, string> response;
        }

        private class EventErrorResponse
        {
            public string time;
            public string request;
            public string result;
            public List<object> errors;
        }

    }
}
