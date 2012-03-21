using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace _20.Events
{
    class TurnoverEvent : Event
    {
      
        private string commitedBy;
        private string forcedBy;
        private string turnoverType;
        private Point location;

        
        public TurnoverEvent(Alpaca pac, string commitedBy, string forcedBy, string turnoverType, Point location)
            : base(pac)
        {
            this.commitedBy = commitedBy;
            this.forcedBy = forcedBy;
            this.turnoverType = turnoverType;
            this.location = location;
        }

        public override void resolve()
        {
            //does nothing for now
        }

        public override void unresolve()
        {
            //does nothing for now
        }

        public override string serialize()
        {
            return JsonConvert.SerializeObject(new 
            { 
                gameId=pac.GameID,
                commitedBy=commitedBy,
                forcedBy=forcedBy,
                location=convertPointToArray(location),
                context=pac.generateContext()
            });
        }
    }
}
