using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
	public class Contact : AggregateRoot
	{
		public string UserId {
			get;
			set;
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

		public string Email {
			get;
			set;
		}

		public string Phone {
			get;
			set;
		}

		public string Twitter {
			get;
			set;
		}
			
	}
}

