using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class HomeCurrentSessionView : ContentView 
	{
		public HomeCurrentSessionView ()
		{

			SetBinding (HomeCurrentSessionView.NavigationProperty, new Binding ("Navigation"));

			Content = CreateStack ();
            Content.SetBinding (HomeCurrentSessionView.BackgroundColorProperty, "Track", 
                converter: new TrackBackgroundColorConverter ());
		}

		View CreateStack ()
		{
			var stackLayout = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 0,
				Padding = new Thickness (10, 10, 10, 30)
			};
			stackLayout.Children.Add (CreateNextNowLabel ());
			stackLayout.Children.Add (CreateInfoStack ());

			return stackLayout;
		}

		View CreateNextNowLabel ()
		{
			var nextNowLabel = new FontLabel 
			{
				Text = "NEXT",
				Font = Font.OfSize (Fonts.OpenSansBold, 9),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand,
				XAlign = TextAlignment.End
			};
			nextNowLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return nextNowLabel;
		}

		StackLayout CreateInfoStack ()
		{
			var stackLayout = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (50, 0, 50, 0)
			};
			stackLayout.Children.Add (CreateSessionTitleLabel ());
			stackLayout.Children.Add (CreateLineViewStack ());
			stackLayout.Children.Add (CreateTrackLabel ());

			var tapGestureRecognizer = new TapGestureRecognizer ();
			tapGestureRecognizer.SetBinding (TapGestureRecognizer.CommandProperty, "NavigateToSession");
			stackLayout.GestureRecognizers.Add (tapGestureRecognizer);

			return stackLayout;
		}

		View CreateSessionTitleLabel()
		{
			var sessionTitleLabel = new FontLabel
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.End,
				Font = Font.OfSize(Fonts.OpenSans, 14)
			};
			sessionTitleLabel.SetBinding (FontLabel.TextProperty, "Title");
			sessionTitleLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return sessionTitleLabel;
		}

		StackLayout CreateLineViewStack ()
		{
			var lineViewStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Spacing = 1
			};
			lineViewStack.Children.Add (CreateLineView ());
			lineViewStack.Children.Add (CreateLocationTimeStack ());
			lineViewStack.Children.Add (CreateLineView ());

			return lineViewStack;
		}

		View CreateLineView ()
		{
			var line = new BoxView 
			{
				WidthRequest = 150,
				HeightRequest = 0.5,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};
			line.SetBinding (BoxView.ColorProperty, "Track", converter: new TrackTextColorConverter ());

			return line;
		}

		StackLayout CreateLocationTimeStack ()
		{
			var locationTimeStack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 4
			};

			locationTimeStack.Children.Add (CreateLocationLabel ());
			locationTimeStack.Children.Add (CreateBulletPoint());
			locationTimeStack.Children.Add (CreateTimeLabel ());

			return locationTimeStack;
		}

		View CreateBulletPoint()
		{
			var bulletPoint = new FontLabel 
			{ 
				Text = "\u2022",
				YAlign = TextAlignment.Center
			};
			bulletPoint.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return bulletPoint;
		}

		View CreateLocationLabel ()
		{
			var locationLabel = new FontLabel 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Font = Font.OfSize(Fonts.OpenSansBold, 9),
				YAlign = TextAlignment.Center
			};
			locationLabel.SetBinding (FontLabel.TextProperty, "Location", converter: new ToUpperValueConverter());
			locationLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return locationLabel;
		}

		View CreateTimeLabel ()
		{
			var timeLabel = new FontLabel
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Font = Font.OfSize(Fonts.OpenSansBold, 9),
				YAlign = TextAlignment.Center
			};
			timeLabel.SetBinding (FontLabel.TextProperty, "Time", converter: new DateTimeValueConverter("hh:mmtt"));
			timeLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return timeLabel;
		}

		View CreateTrackLabel ()
		{
			var trackLabel = new FontLabel
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Font = Font.OfSize(Fonts.OpenSans, 9)	/* NEEDS TO BE SEMI-BOLD FONT */
			};
			trackLabel.SetBinding (FontLabel.TextProperty, "Track");
			trackLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return trackLabel;
		}
	}
}

