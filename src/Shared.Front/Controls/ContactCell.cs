using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Controls
{
	public class ContactCell : ViewCell
	{
		public ContactCell ()
		{
			var contactImage = new RoundedImageView {
				HeightRequest = 60,
				WidthRequest = 60,
			};

			contactImage.SetBinding (Image.SourceProperty, "ImageUri");
			contactImage.SetBinding (Image.OpacityProperty, "IsProxy", converter: new BooleanToAlphaConverter (0.5, 1.0));

			var contactImageView = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					contactImage
				}
			};

			var firstNameLabel = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 18)
			};

			firstNameLabel.SetBinding (Label.TextProperty, "First");

			var lastNameLabel = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSans, 18)
			};

			lastNameLabel.SetBinding (Label.TextProperty, "Last");

			var companyLabel = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansBold, 9)
			};

			companyLabel.SetBinding (Label.TextProperty, "Company", converter: new ToUpperValueConverter());
			companyLabel.SetBinding (FontLabel.IsVisibleProperty, "IsProxy", converter: new NegateValueConverter ());

			/* * * * Contact Proxy Label * * * */
			var syncing = CreateSyncingLabel ();
			/* * * * * * * * * * * * * * * * * */

			var nameStack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Spacing = 3,
				Children = {
					firstNameLabel,
					lastNameLabel
				}
			};

			var labelGrid = new Grid 
			{
				Children = 
				{
					companyLabel,
					syncing
				}
			};

			var contactInfo = new StackLayout 
			{
				Padding = new Thickness(0, 5, 0, 5),
				Spacing = 2,
				Children = {
					nameStack,
					labelGrid
				}
			};
					
			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				BackgroundColor = Color.Default,
				Padding = new Thickness(5, 5, 0, 5),
				Spacing = 10,
				Children = {
					contactImageView,
					contactInfo
				}
			};
		}

		View CreateSyncingLabel ()
		{
			var syncing = new FontLabel 
			{
				Text = "Syncing...",
				TextColor = Color.FromHex ("34aadc"),
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				XAlign = TextAlignment.Center
			};
			syncing.SetBinding (FontLabel.IsVisibleProperty, "IsProxy");

			return syncing;
		}
	}
}

