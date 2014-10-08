using System;
using Xamarin.Forms;

namespace CouchbaseConnect2014.Controls
{
  public class RoundedImageView : Image
	{
		public RoundedImageView ()
		{

		}

		public static readonly BindableProperty SideLengthProperty =
			BindableProperty.Create<RoundedImageView, double>(
				p => p.SideLength, default(double));

		public double SideLength {
			get { return (double)GetValue (SideLengthProperty); }
			set { SetValue (SideLengthProperty, value); WidthRequest = SideLength; HeightRequest = SideLength; }
		}
	}
}
