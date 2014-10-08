using System;
using Xamarin.Forms;
using System.ComponentModel;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014.Controls
{
    public class MenuCell : ViewCell {
        readonly FontLabel _label;

        public MenuCell ()
        {
            _label = CreateLabel ();

			var content = new ContentView {
				BackgroundColor = Colors.MenuBackground,
				Padding = new Thickness (20, 0),
				Content = _label
			};

            content.GestureRecognizers.Add (CreateTapGestureRecognizer ());

			View = content;
        }

        IGestureRecognizer CreateTapGestureRecognizer ()
        {
            var recognizer = new TapGestureRecognizer ();
            recognizer.SetBinding (TapGestureRecognizer.CommandProperty, "ItemTapped");
            return recognizer;
        }

        static FontLabel CreateLabel ()
        {
            var label = new FontLabel {
                TextColor = Colors.MenuText,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Font = Font.OfSize (Fonts.OpenSansLight, 18)
            };
            label.SetBinding (Label.TextProperty, "Text");
            return label;
        }

        public static BindableProperty TextProperty =
            BindableProperty.Create<MenuCell, string> (
                b => b.Text,
                null);

        public string Text {
            get {
                return (string)GetValue (TextProperty);
            }
            set {
                SetValue (TextProperty, value);
            }
        }

        public static BindableProperty IsSelectedProperty =
            BindableProperty.Create<MenuCell, bool> (
                b => b.IsSelected,
                false,
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((MenuCell)bindable)._label.Font = newValue
                        ? Font.OfSize(Fonts.OpenSans, 18)
                        : Font.OfSize(Fonts.OpenSansLight, 18);
                });

        public bool IsSelected {
            get { 
                return (bool)GetValue (IsSelectedProperty);
            }
            set {
                SetValue (IsSelectedProperty, value);
            }
        }
    }
}
