using System;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.Tests.Mocks
{
    public class MockTimeService : ITimeService
    {
        public DateTime Today {
            get;
            set;
        }

        public DateTime Now {
            get;
            set;
        }
    }
}

