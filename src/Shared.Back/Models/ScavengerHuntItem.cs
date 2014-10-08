using System;

namespace CouchbaseConnect2014.Models
{
	public class ScavengerHuntItem: AggregateRoot
	{
		public ScavengerHuntItem ()
		{
		}

		// doc id = scavengerhuntitem-<Id>
		// i.e. scavengerhuntitem-0001

		public int Order
		{
			get;
			set;
		}
			
		public byte[] Image
		{
			get;
			set;
		}

		public string Description 
		{
			get;
			set;
		}
	}
}

