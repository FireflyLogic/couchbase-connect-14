using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014.Views
{
    public class FullScheduleCell : ViewCell
    {
        public FullScheduleCell ()
        {
            View = CreateRootStack ();
        }

        View CreateRootStack ()
        {
            var stack = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Padding = 12,
                Children =  {
                    new SessionItem (),
                    CreateSelectionIndicator ()
                }
            };
            stack.SetBinding (StackLayout.BackgroundColorProperty, "Track",
                converter: new TrackBackgroundColorConverter ());

            return stack;
        }

        View CreateSelectionIndicator ()
        {
            var button = new Button {
                Font = Font.OfSize (Fonts.OpenSansLight, 22),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 50,
                WidthRequest = 40,
                BorderWidth = 0,
                BackgroundColor = Color.Transparent
            };
            button.SetBinding (Button.TextColorProperty, "Track",
                converter: new TrackTextColorConverter ());
            button.SetBinding (Button.TextProperty, "IsSelected",
                converter: new BooleanToTextValueConverter ("✓", "○"));
            button.SetBinding (Button.IsVisibleProperty, "IsOptional");
            button.SetBinding (Button.CommandProperty, "ToggleSelection");
            return button;
        }
    }
}

