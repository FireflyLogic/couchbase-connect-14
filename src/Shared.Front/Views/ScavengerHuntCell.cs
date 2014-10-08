using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014
{
	public class ScavengerHuntCell : ViewCell
	{
		public ScavengerHuntCell ()
		{
			View = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				Spacing = 15,
				Padding = new Thickness (15, 15, 15, 15),
				Children = 
				{
					CreateScavengerHuntImage(),
					CreateScavengerHuntDescription()
				}
			};
		}

		View CreateScavengerHuntImage ()
		{
			var huntImage = new RoundedImageView 
			{
				WidthRequest = 75,
				HeightRequest = 75
			};
			huntImage.SetBinding (RoundedImageView.SourceProperty, "CapturedImage", converter: new ByteArrayToImageSourceConverter());

			var huntImageView = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = {
					huntImage
				}
			};

			return huntImageView;
		}

		View CreateScavengerHuntDescription ()
		{
			var huntDescription = new FontLabel 
			{
				TextColor = Color.FromHex ("333333"),
				Font = Font.OfSize(Fonts.OpenSansLight, 18),
				WidthRequest = 250,
				YAlign = TextAlignment.Center
			};
			huntDescription.SetBinding (FontLabel.TextProperty, "HuntItemDescription");

			return huntDescription;
		}
	}
}

