using System;
using System.Globalization;
using Xamarin.Forms;

namespace CouchbaseConnect2014
{
	public class BooleanToAlphaConverter : IValueConverter
	{
		readonly double _trueAlpha;
		readonly double _falseAlpha;

		public BooleanToAlphaConverter (double trueAlpha, double falseAlpha)
		{
			_trueAlpha = trueAlpha;
			_falseAlpha = falseAlpha;
		}

		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? _trueAlpha : _falseAlpha;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

