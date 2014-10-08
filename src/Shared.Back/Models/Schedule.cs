using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
	public class Schedule : AggregateRoot
	{
		public IEnumerable<Day> Days { get; set; }
	}
}

