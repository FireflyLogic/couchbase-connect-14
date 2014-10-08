using System;

namespace CouchbaseConnect2014.Services
{
    public interface ITimeService
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}

