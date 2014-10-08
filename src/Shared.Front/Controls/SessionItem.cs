using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Controls
{
    public class SessionItem : StackLayout
    {
        public SessionItem ()
        {
            HorizontalOptions = LayoutOptions.StartAndExpand;
            VerticalOptions = LayoutOptions.EndAndExpand;
            Spacing = 5;
            Children.Add (CreateTitleLabel ());
            Children.Add (CreateLocationTrackStack ());
        }

        static StackLayout CreateSessionStack ()
        {
            var sessionStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Spacing = 5         
            };
            sessionStack.Children.Add (CreateTitleLabel ());
            sessionStack.Children.Add (CreateLocationTrackStack ());
            return sessionStack;
        }

        static FontLabel CreateTitleLabel ()
        {
            var titleLabel = new FontLabel 
            {
                Font = Font.OfSize(Fonts.OpenSansLight, 14),
                LineBreakMode = LineBreakMode.WordWrap
            };
            titleLabel.SetBinding (FontLabel.TextProperty, "Title");
            titleLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());
            return titleLabel;
        }

        static StackLayout CreateLocationTrackStack()
        {
            var locationTrackStack = new StackLayout 
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Spacing = 3
            };

            locationTrackStack.Children.Add (CreateLocationLabel ());
            locationTrackStack.Children.Add (CreateTrackLabel ());
            return locationTrackStack;
        }

        static FontLabel CreateLocationLabel ()
        {
            var locationLabel = new FontLabel 
            {
                Font = Font.OfSize(Fonts.OpenSansBold, 9),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                YAlign = TextAlignment.Center
            };
            locationLabel.SetBinding (FontLabel.TextProperty, "Location", converter: new ToUpperValueConverter());
            locationLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());
            return locationLabel;
        }

        static FontLabel CreateTrackLabel ()
        {
            var trackLabel = new FontLabel 
            {
                Font = Font.OfSize(Fonts.OpenSansLight, 9),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                YAlign = TextAlignment.Center
            };
            trackLabel.SetBinding (FontLabel.TextProperty, "Track", converter: new TrackValueConverter());
            trackLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());
            return trackLabel;
        }
    }
}

