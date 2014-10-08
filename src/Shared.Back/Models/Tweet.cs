using System;
using CouchbaseConnect2014.Extensions;
using System.Windows.Input;

namespace CouchbaseConnect2014.Models
{
	public class Tweet
	{
		public string Icon {
			get;
			set;
		}

		public string Twitter 
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Time 
		{
			get
			{
				return CreatedAtDate.TimeSince();
			}
		}

		public string Content 
		{
			get;
			set;
		}

		public DateTime CreatedAtDate
		{
			get;
			set;
		}

		public string FirstEmbeddedUrl
		{
			get;
			set;
		}
	}
}

