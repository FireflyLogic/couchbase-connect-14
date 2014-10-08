using System;
using System.Globalization;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ValueConverters
{
	public class TrackBackgroundColorConverter: IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			var defaultColor = Color.White;
			var stringValue = (string)value;
			if (string.IsNullOrEmpty (stringValue) || stringValue == "None")
				return defaultColor;

			TrackColorScheme scheme;
			return App.TrackColor.TryGetValue ((string)value, out scheme)
				? scheme.Background : defaultColor;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}

}