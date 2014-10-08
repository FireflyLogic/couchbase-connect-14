using System;

namespace CouchbaseConnect2014.Models
{
	public class Speaker : AggregateRoot
	{
		public Speaker ()
		{
		}

		public string First {
			get;
			set;
		}

		public string Last {
			get;
			set;
		}

		public string Company {
			get;
			set;
		}

		public string Role {
			get;
			set;
		}

		public string HeadshotUrl {
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}
	}
}

