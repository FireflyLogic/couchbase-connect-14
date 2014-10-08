using System;

namespace CouchbaseConnect2014.Models
{
	public class ScavengerHuntCapture: AggregateRoot
	{

		// doc id = [scavengerhuntitem Id]-[UserId]
		// i.e. scavengerhuntitem-0001-[UserId]

		public string ItemId
		{
			get;
			set;
		}

		public byte[] Image 
		{
			get;
			set;
		}
	}
		
}

