using System;
using Xamarin.Forms;
using System.Globalization;

namespace CouchbaseConnect2014.ValueConverters
{
    public class BooleanToColorValueConverter : IValueConverter
    {
        readonly Color _trueColor;
        readonly Color _falseColor;

        public BooleanToColorValueConverter (Color trueColor, Color falseColor)
        {
            _trueColor = trueColor;
            _falseColor = falseColor;
        }

        #region IValueConverter implementation

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? _trueColor : _falseColor;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException ();
        }

        #endregion
    }
}

