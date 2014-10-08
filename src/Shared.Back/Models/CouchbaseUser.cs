using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
	public class CouchbaseUser
	{
		public string Name
		{
			get; 
			set;
		}

		public IList<string> Channels
		{
			get; 
			set;
		}
	}
}

