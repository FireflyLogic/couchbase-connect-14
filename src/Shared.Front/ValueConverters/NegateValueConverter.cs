using System;
using Xamarin.Forms;
using System.Globalization;

namespace CouchbaseConnect2014.ValueConverters
{
    public class NegateValueConverter : IValueConverter
    {
        public NegateValueConverter ()
        {
        }

        #region IValueConverter implementation

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        #endregion
    }
}

