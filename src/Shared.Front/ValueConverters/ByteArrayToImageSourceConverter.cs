using System;
using System.Globalization;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;

namespace CouchbaseConnect2014
{
	public class ByteArrayToImageSourceConverter : IValueConverter
	{
		public ByteArrayToImageSourceConverter ()
		{
		}

		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			var photo = (byte[])value;

			if (photo != null) {
				return FileImageSource.FromStream (() => {
					return new MemoryStream (photo);
				});
			} else {
				//return null;
				return "add_pic.png";
			}
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

