using System;
using System.Globalization;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ValueConverters
{
	public class JobTitleConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			var jobTitle = (string)value;

			if (string.IsNullOrEmpty (jobTitle))
				return "";
			else
				return jobTitle + ",";
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}

}
