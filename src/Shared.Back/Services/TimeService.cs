using System;

namespace CouchbaseConnect2014.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Now {
            get {
                return DateTime.Now;
            }
        }

        public DateTime Today {
            get {
                return DateTime.Today;
            }
        }
    }
}

