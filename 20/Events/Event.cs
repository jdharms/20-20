using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20
{
    /// <summary>
    /// The Event abstract class. Any specific Events will extend this class and have to implement 4 methods:
    ///     serialize, deserialize, resolve and unresolve.
    ///     
    /// Every Event must take in a Alpaca object.
    /// </summary>
    abstract class Event
    {
        private string eventId;
        public string EventId { get { return eventId; } }
        public bool ReceivedByServer { get { return eventId != null; } }

        private Alpaca pac;
        
        public Event(Alpaca pac)
        {
            this.pac = pac;
        }

        /// <summary>
        /// Serialize the event into a JSON object. 
        /// </summary>
        /// <returns> The serialized event object as a string </returns>
        public abstract string serialize();
        /// <summary>
        /// Deserializes the json string.
        /// </summary>
        /// <param name="json"> The response of a json serialized object being sent to the server</param>
        /// <returns> True if the response was an 'okay' response, otherwise false.</returns>
        public abstract bool deserialize(string json);
        /// <summary>
        /// Resolves any actions that need to be performed by the event
        /// </summary>
        public abstract void resolve();
        /// <summary>
        /// Undoes any resolved events. 
        /// </summary>
        public abstract void unresolve();

    }
}
