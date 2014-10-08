using System;
using System.Globalization;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ValueConverters
{
	public class TrackValueConverter: IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
//			return (string)value == "Plenary" ? "" : String.Format ("- {0}", value);
			return (string)value == "Registration" || (string)value == "MealBreak" || (string)value == "NoTrack" 
				? "" 
					: (string)value;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}

}

