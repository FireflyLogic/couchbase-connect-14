using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014.Views
{
    public class AgendaCell : ViewCell
    {
        public AgendaCell ()
        {
			var outerRelativeLayout = new RelativeLayout ();
			outerRelativeLayout.SetBinding (RelativeLayout.BackgroundColorProperty, "Track", converter: new TrackBackgroundColorConverter ());

			var leftStack = CreateLeftStack ();
			outerRelativeLayout.Children.Add (leftStack,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToParent ((parent) => { return parent.Y; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width * 0.74; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height; })
			);

			var timeStack = CreateTimeStack ();
			outerRelativeLayout.Children.Add (timeStack,
				Constraint.RelativeToView (leftStack, (parent,sibling) => { return sibling.Width; }),
				Constraint.RelativeToParent ((parent) => { return parent.Y; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width * 0.26; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height; })
			);

			View = outerRelativeLayout;
        }

        static StackLayout CreateLeftStack ()
        {
            var leftStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Padding = new Thickness(15, 0, 0, 13)
            };
            leftStack.Children.Add (CreateSessionItem ());
			leftStack.Children.Add (CreateNoSessionStack ());
            return leftStack;
        }

        static SessionItem CreateSessionItem ()
        {
            var sessionItem = new SessionItem ();
            sessionItem.SetBinding (SessionItem.IsVisibleProperty, "IsBooked");
            return sessionItem;
        }

		static StackLayout CreateNoSessionStack ()
        {
			var noSessionStack = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Spacing = 10
			};
			noSessionStack.SetBinding (Frame.IsVisibleProperty, "IsBooked", converter: new NegateValueConverter ());
			noSessionStack.Children.Add (CreateAddIcon ());
			noSessionStack.Children.Add (CreateNoSessionLabel ());
			return noSessionStack;
        }

		static Image CreateAddIcon ()
		{
			var addIcon = new Image 
			{
				Source = "plus.png",
				WidthRequest = 20,
				HeightRequest = 20,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.EndAndExpand
			};
			return addIcon;
		}

		static FontLabel CreateNoSessionLabel ()
		{
			var noSessionLabel = new FontLabel {
				Text = "Choose a Session",
                Font = Font.OfSize(Fonts.OpenSansLight, 14),
				TextColor = Color.FromHex("333333"),
				HorizontalOptions = LayoutOptions.EndAndExpand
			};
			return noSessionLabel;
		}

        static StackLayout CreateTimeStack ()
        {
            var timeStack = new StackLayout {
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(0, 0, 5, 10),
				Spacing = 2
			};
            timeStack.Children.Add (CreateTimeLabel ());
            timeStack.Children.Add (CreateAMPMLabel ());
            return timeStack;
        }

		static FontLabel CreateTimeLabel ()
        {
			var timeLabel = new FontLabel 
			{
                Font = Font.OfSize(Fonts.OpenSansLight, 24),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};
			timeLabel.SetBinding (FontLabel.TextProperty, "Time", converter: new DateTimeValueConverter ("h:mm"));
			timeLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());
            return timeLabel;
        }

		static FontLabel CreateAMPMLabel ()
        {
			var amPMLabel = new FontLabel 
			{
                Font = Font.OfSize(Fonts.OpenSansBold, 9),
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.EndAndExpand,
				YAlign = TextAlignment.End,
				TranslationY = -4
			};
			amPMLabel.SetBinding (FontLabel.TextProperty, "Time", converter: new DateTimeValueConverter ("tt"));
			amPMLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());
            return amPMLabel;
        }
    }
}

