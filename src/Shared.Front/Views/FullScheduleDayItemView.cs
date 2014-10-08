using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Views
{
    public class FullScheduleDayItemView : ContentView
    {
        public FullScheduleDayItemView ()
        {
            Content = CreateDateStack ();
        }

        View CreateDateStack ()
        {
            return new StackLayout {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Children = {
                    CreateDayLabel (),
                    CreateDateLabel ()
                }
            };
        }

        View CreateDayLabel ()
        {
            var label = new Label {

            };
            label.SetBinding (Label.TextProperty, "Date",
                converter: new DateTimeValueConverter ("dddd", true));
            return label;
        }

        View CreateDateLabel ()
        {
            var label = new Label {

            };
            label.SetBinding (Label.TextProperty, "Date",
                converter: new DateTimeValueConverter ("MMM d", true));
            return label;
        }
    }
}

