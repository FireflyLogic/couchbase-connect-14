using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Controls
{
    public class DayHeader : ContentView
    {
        public DayHeader ()
        {
            HorizontalOptions = LayoutOptions.Center;

            var dayLabel = new FontLabel 
            {
                TextColor = Color.FromHex("ea2228"),
                Font = Font.OfSize(Fonts.OpenSansBold, 12)
            };

            dayLabel.SetBinding (Label.TextProperty, "Date", converter: new DateTimeValueConverter ("dddd", true));

            var dateLabel = new Label {
                TextColor = Color.FromHex("ea2228"),
                Font = Font.OfSize(Fonts.OpenSansLight, 12)
            };

            dateLabel.SetBinding (Label.TextProperty, "Date", converter: new DateTimeValueConverter("MMM d", true));

            var dateStack = new StackLayout 
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand, 
                VerticalOptions = LayoutOptions.Center,
                Spacing = 3,
                Children = {
                    dayLabel,
                    dateLabel
                }
            };

            Content = dateStack;
        }
    }
}

