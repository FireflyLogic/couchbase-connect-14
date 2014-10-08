using System;

namespace CouchbaseConnect2014.Extensions
{
	public static class DateTime_TimeSince
	{
		public static string TimeSince(this DateTime dateTime)
		{	
			string prettyDate = ":(";

			TimeSpan diff = DateTime.Now - dateTime;

			if (diff.Seconds <= 0)
			{
				prettyDate = "now";
			} 
			else if (diff.Days > 30)
			{
				prettyDate = diff.Days / 30 + "m";
			}
			else if (diff.Days > 7)
			{
				prettyDate = diff.Days / 7 + "w";
			}
			else if (diff.Days >= 1)
			{
				prettyDate = diff.Days + "d";
			}
			else if (diff.Hours >= 1)
			{
				prettyDate = diff.Hours + "h";
			}
			else if (diff.Minutes >= 1)
			{
				prettyDate = diff.Minutes + "m";
			}
			else
			{
				prettyDate = diff.Seconds + "s";
			}

			return prettyDate;
		}	
	}
}

