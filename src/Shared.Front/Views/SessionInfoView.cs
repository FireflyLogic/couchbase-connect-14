using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014
{
	public class SessionInfoView : ScrollView 
	{
		public SessionInfoView ()
		{
			// contains the topStack and sessionDescription
			var sessionInfoStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand
			};
			sessionInfoStack.Children.Add(CreateTopStack());
			sessionInfoStack.Children.Add(CreateDescriptionStack());

			Content = sessionInfoStack;
		}

		/* TopStack: contains picture and other session info (colored part) */
		StackLayout CreateTopStack ()
		{
			var stackLayout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Spacing = 12,
				Padding = new Thickness (60, 20, 60, 10)
			};
			stackLayout.SetBinding (StackLayout.BackgroundColorProperty, "Track", converter: new TrackBackgroundColorConverter ());

			stackLayout.Children.Add (CreateSpeakerImage ());
			stackLayout.Children.Add (CreateNameTitleCompanyStack ());
			stackLayout.Children.Add (CreateSessionTitleLabel ());
			stackLayout.Children.Add (CreateLineViewStack ());
			stackLayout.Children.Add (CreateTrackLabel ());

			return stackLayout;
		}

		StackLayout CreateDescriptionStack ()
		{
			var descStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness (35, 15, 35, 10)
			};
			descStack.Children.Add (CreateDescriptionLabel ());

			return descStack;
		}

		/* SessionDescription: additional info about session (non-colored part) */
		View CreateDescriptionLabel ()
		{
			var desc = new FontLabel
			{
				LineBreakMode = LineBreakMode.WordWrap,
				Font = Font.OfSize(Fonts.OpenSans, 12)
			};

			desc.SetBinding (FontLabel.TextProperty, "Abstract");

			return desc;
		}

		View CreateSpeakerImage ()
		{
			var picture = new RoundedImageView 
			{
				WidthRequest = 125,
				HeightRequest = 125,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			picture.SetBinding (RoundedImageView.SourceProperty, "PrimarySpeakerHeadshot");

			return picture;
		}

		StackLayout CreateNameTitleCompanyStack ()
		{
			var nameTitleCompanyStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 3
			};
			nameTitleCompanyStack.Children.Add (CreateSpeakerLabel ());
			nameTitleCompanyStack.Children.Add (CreateTitleJobStack ());

			return nameTitleCompanyStack;
		}

		View CreateSpeakerLabel ()
		{
			var speakerLabel = new FontLabel 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Font = Font.OfSize(Fonts.OpenSans, 12)
			};
			speakerLabel.SetBinding (FontLabel.TextProperty, "PrimarySpeakerName");
			speakerLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return speakerLabel;
		}

		StackLayout CreateTitleJobStack ()
		{
			var titleJobStack = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			titleJobStack.Children.Add (CreateJobTitleLabel ());
			titleJobStack.Children.Add (CreateCompanyLabel ());

			return titleJobStack;
		}

		View CreateJobTitleLabel ()
		{
			var jobTitleLabel = new FontLabel 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Font = Font.OfSize(Fonts.OpenSansLight, 10)	/* NEEDS TO BE ITALIC FONT */
			};

			jobTitleLabel.SetBinding (FontLabel.TextProperty, "PrimarySpeakerRole", converter: new JobTitleConverter());
			jobTitleLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return jobTitleLabel;
		}

		View CreateCompanyLabel ()
		{
			var companyLabel = new FontLabel
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Font = Font.OfSize(Fonts.OpenSansLight, 10)	/* NEEDS TO BE ITALIC FONT */
			};

			companyLabel.SetBinding (FontLabel.TextProperty, "PrimarySpeakerCompany");
			companyLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return companyLabel;
		}

		View CreateSessionTitleLabel ()
		{
			var sessionTitleLabel = new FontLabel
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				HeightRequest = 58,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				Font = Font.OfSize(Fonts.OpenSans, 14)
			};
			sessionTitleLabel.SetBinding (FontLabel.TextProperty, "Title");
			sessionTitleLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return sessionTitleLabel;
		}

		View CreateLineView ()
		{
			var line = new BoxView 
			{
				WidthRequest = 150,
				HeightRequest = 0.5,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			line.SetBinding (BoxView.ColorProperty, "Track", converter: new TrackTextColorConverter ());

			return line;
		}

		StackLayout CreateLineViewStack ()
		{
			var lineViewStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 1
			};
			lineViewStack.Children.Add (CreateLineView ());
			lineViewStack.Children.Add (CreateLocationTimeStack ());
			lineViewStack.Children.Add (CreateLineView ());

			return lineViewStack;
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
				Font = Font.OfSize(Fonts.OpenSans, 9)	/* NEEDS TO BE SEMI-BOLD FONT */
			};
			trackLabel.SetBinding (FontLabel.TextProperty, "Track", converter: new TrackValueConverter());
			trackLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return trackLabel;
		}
	}
}
