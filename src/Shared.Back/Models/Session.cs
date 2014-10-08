using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
	public class Session : AggregateRoot
	{
		public Session()
		{
			SpeakerIds = new List<string> ();
		}

        public string Title {
            get;
            set;
        }

		public string Abstract {
			get;
			set;
		}

        public string Location {
            get;
            set;
        }

        // TODO : Try to get rid of this and use StartTime on Slot (only used in data load)
        public DateTime Time {
            get;
            set;
        }

        public string Track {
            get;
            set;
        }

		public IEnumerable<string> SpeakerIds
		{ 
			get; 
			set;
		}
	}
}

