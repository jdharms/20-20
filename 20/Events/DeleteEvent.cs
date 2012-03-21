using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace _20.Events
{
    class DeleteEvent : Event
    {
        private Event eventToDelete;
        private string deletionId;

        public DeleteEvent(Alpaca pac, Event eventToDelete)
            : base(pac)
        {
            this.eventToDelete = eventToDelete;
            deletionId = eventToDelete.EventId;
        }

        public override void resolve()
        {
            eventToDelete.unresolve();
        }

        public override void unresolve()
        {
            //should never be called
        }

        public override string serialize()
        {
            //delete api call has no Json payload.
            return "";
        }

        public override bool deserialize(string json)
        {
            try
            {
                DeleteEventResponse response = JsonConvert.DeserializeObject<DeleteEventResponse>(json);
                eventId = response.response["deletedEvent"];
                Console.WriteLine("DeleteEvent -- " + eventId);
                return true;
            }
            catch (NullReferenceException e)
            {
                DeleteEventErrorResponse response = JsonConvert.DeserializeObject<DeleteEventErrorResponse>(json);
                foreach (object error in response.errors)
                {
                    string errorMessage = parseError(error.ToString());
                    Console.WriteLine(errorMessage);
                }

                return false;
            }
    }

    private class DeleteEventResponse
        {
            public string time;
            public string request;
            public string result;
            public Dictionary<string, string> response;
        }

        private class DeleteEventErrorResponse
        {
            public string time;
            public string request;
            public string result;
            public List<object> errors;
        }

    }

}

