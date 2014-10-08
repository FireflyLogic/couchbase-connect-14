using System;
using Xamarin.Forms;
using System.Globalization;

namespace CouchbaseConnect2014.ValueConverters
{
    public class DateTimeValueConverter : IValueConverter
    {
        string _formatString;
        bool _upperCase;

		public DateTimeValueConverter (string formatString, bool upperCase = false)
        {
            this._formatString = formatString;
            this._upperCase = upperCase;
        }

        #region IValueConverter implementation

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = ((DateTime)value).ToString (_formatString);
			if (_upperCase) { result = result.ToUpper (); }

            return result;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException ();
        }

        #endregion
    }
}

