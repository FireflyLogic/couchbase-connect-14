using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
    public class Agenda : AggregateRoot
    {
        public Agenda ()
        {
			SelectedSessions = new Dictionary<DateTime, string> ();
        }

		public IDictionary<DateTime, string> SelectedSessions
		{
			get;
			set;
		}

		public bool IsSelected (DateTime startTime, string sessionId) {
			return SelectedSessions.ContainsKey (startTime) && 
				SelectedSessions [startTime] == sessionId;
		}
    }
}

