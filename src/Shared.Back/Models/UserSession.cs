using System;

namespace CouchbaseConnect2014.Models
{
    public class UserSession
    {
        public string SlotId {
            get;
            set;
        }

        public string SessionId {
            get;
            set;
        }

		public int ContentRating { get; set; }

		public int SpeakerRating { get; set; }
    }
}

