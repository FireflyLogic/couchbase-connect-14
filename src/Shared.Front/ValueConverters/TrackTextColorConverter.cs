using System;
using System.Globalization;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ValueConverters
{
	public class TrackTextColorConverter: IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			var defaultColor = Color.FromHex ("333333");
			var stringValue = (string)value;
			if (string.IsNullOrWhiteSpace (stringValue) || stringValue == "None")
				return defaultColor;

			TrackColorScheme scheme;
			return App.TrackColor.TryGetValue ((string)value, out scheme)
				? scheme.PrimaryText : defaultColor;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}

}