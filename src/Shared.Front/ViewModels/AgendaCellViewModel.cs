using System;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.ViewModels
{
    public class AgendaCellViewModel : BaseViewModel
    {
        public AgendaCellViewModel (Slot slot)
        {
            Slot = slot;
            Time = slot.StartTime;
        }

        public string Title
		{
			get;
			set;
        }

        public string Location {
            get;
            set;
        }

		public string Track
		{
			get;
			set;
		}

        public DateTime Time {
            get;
            set;
        }

        public bool IsBooked {
            get;
            set;
        }

        public Slot Slot {
            get;
            private set;
        }
    }
}

