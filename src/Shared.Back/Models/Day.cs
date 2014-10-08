using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
    public class Day
    {
        public Day ()
        {
            Slots = new List<Slot> ();
        }

        public DateTime Date {
            get;
            set;
        }

        public IEnumerable<Slot> Slots {
            get;
            set;
        }
    }
}

