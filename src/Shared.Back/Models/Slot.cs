using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
	public class Slot
	{
        public Slot ()
        {
            SessionIds = new List<string> ();
        }
			
        public DateTime StartTime {
            get;
            set;
        }

        public DateTime EndTime {
            get;
            set;
        }

        public IEnumerable<string> SessionIds {
            get;
            set;
        }

	}
}

