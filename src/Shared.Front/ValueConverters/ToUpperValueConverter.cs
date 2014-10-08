using System;
using System.Globalization;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ValueConverters
{
    public class ToUpperValueConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null
                ? value
                : ((string)value).ToUpper ();
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException ();
        }

        #endregion
    }
}

