using System;
using System.Globalization;
using Xamarin.Forms;
using System.Collections.Generic;

namespace CouchbaseConnect2014
{
	public class ScavengerHuntHeaderConverter : IValueConverter
	{
		string [] phrases;

		public ScavengerHuntHeaderConverter ()
		{
			phrases = new string[] 
			{
				"Yawn…you might want to get started.",
				"Congratulations gorgeous.  Thanks for showing up.",
				"Sometimes you amaze me.  But then again sometimes you don’t.",
				"Your favorite thing to do seems to be nothing.",
				"Oh…checking in huh!?",
				"That’s all day.",
				"You’re smart, funny, and gosh darn it people like you.",
				"I’m going to say this as calmly as possible.  YOU ARE AWESOME!!!",
			};
		}

		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			// String.Format("{0} item{1} to go. {2}", itemsRemaining, itemsRemaining > 1 ? "s" : "", uniquePhrase);
			var stats = (CaptureStatistics)value;

			if (stats.TotalItems == 0)
			{
				return "Still hiding the eggs... come back soon.";
			}

			if ( stats.ItemsRemaining == 0)
			{
				return "#nailedit now go claim your prize!";
			}

			if (stats.ItemsRemaining == 1)
			{
				return "Crush or be crushed.  You only have one left.";
			}

			var completed = stats.TotalItems - stats.ItemsRemaining;

			string message = "You can do it!";

			if (completed < phrases.Length)
			{
				message = phrases [completed];
			}

			return string.Format ("{0} item{1} to go. {2}", 
				stats.ItemsRemaining, 
				stats.ItemsRemaining > 1 ? "s" : "",
				message
			);
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

