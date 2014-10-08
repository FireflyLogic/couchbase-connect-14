using System;
using Xamarin.Forms;
using System.Globalization;

namespace CouchbaseConnect2014.ValueConverters
{
	public class BooleanToImageConverter : IValueConverter
	{
		readonly string _trueValue;
		readonly string _falseValue;

		public BooleanToImageConverter (string trueValueImage, string falseValueImage)
		{
			_falseValue = falseValueImage;
			_trueValue = trueValueImage;
		}

		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? _trueValue : _falseValue;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

