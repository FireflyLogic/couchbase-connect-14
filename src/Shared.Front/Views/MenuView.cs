using System;
using Xamarin.Forms;
using System.ComponentModel;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Views
{
    public class MenuView : ContentPage, INotifyPropertyChanged
    {
        public MenuView ()
        {
            BaseViewModel.CreateAndBind<MenuViewModel> (this);

            Title = "Couchbase Connect";
            Icon = "menu.png";
            BackgroundColor = Colors.MenuBackground;

            Content = new StackLayout {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    CreateTopRow (),
                    CreateListView ()
                }
            };
        }

        static View CreateTopRow ()
        {

			var profile = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Spacing = 18,
				HeightRequest = 50,
				Padding = new Thickness (20, 40, 10, 10),
				//BackgroundColor = Color.Green,
				Children = {
					CreateQRBox (),
					CreateNameStack ()
				}
			};
			profile.SetBinding(StackLayout.IsVisibleProperty, "HasProfile");
			var profileTapGesture = new TapGestureRecognizer();
			profileTapGesture.SetBinding (TapGestureRecognizer.CommandProperty, "AddContactCommand");
			profile.GestureRecognizers.Add (profileTapGesture);

			var noProfile = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 18,
				HeightRequest = 50,
				Padding = new Thickness (20, 40, 10, 10),
				//BackgroundColor = Color.Red,
				Children = {
					CreateNoProfileView ()
				}
			};
			noProfile.SetBinding(StackLayout.IsVisibleProperty, "HasProfile", converter: new NegateValueConverter());
			var noProfileTapGesture = new TapGestureRecognizer();
			noProfileTapGesture.SetBinding<MenuViewModel> (TapGestureRecognizer.CommandProperty, vm => vm.CreateProfileCommand);
			noProfile.GestureRecognizers.Add (noProfileTapGesture);

			var grid = new Grid {
				Children = {
					profile,
					noProfile
				}
			};

			return grid;
        }

        static View CreateQRBox ()
        {
            return new Frame {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness (7),
                HasShadow = false,
                Content = CreateQRImage ()
            };
        }

        static View CreateQRImage ()
        {

			var image = new Image {
				BackgroundColor = Color.Gray,
				WidthRequest = 27,
				HeightRequest = 27,
				Source = "qr.png"
			};

			return image;
        }

        static View CreateNameStack ()
        {
            return new StackLayout {
                Spacing = -5,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    CreateFirstName (),
                    CreateLastName ()
                }
            };
        }

        static View CreateFirstName ()
        {
            var label = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansLight, 18),
                TextColor = Colors.MenuText
            };
            label.SetBinding (Label.TextProperty, "FirstName");
            return label;
        }

        static View CreateLastName ()
        {
            var label = new FontLabel {
                Font = Font.OfSize (Fonts.OpenSans, 18),
                TextColor = Colors.MenuText
            };
            label.SetBinding (Label.TextProperty, "LastName");
            return label;
        }

		static View CreateNoProfileView () {

			var first = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 18),
				Text = "Howdy,",
				TextColor = Colors.MenuText
			};

			var last = new FontLabel {
				Font = Font.OfSize (Fonts.OpenSans, 18),
				Text = "Stranger",
				TextColor = Colors.MenuText
			};

			var stack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = {
					first,
					last
				}
			};

			var label = new FontLabel {
				//Underline = true,
				Font = Font.OfSize(Fonts.OpenSansLight, 12),
				Text = "Create your profile",
				TextColor = Colors.MenuText,
			};

			var view = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = {
					stack,
					label
				}
			};

			return view;
		}

        static View CreateListView ()
        {
            var listView = new ListView () {
                RowHeight = 46,
				BackgroundColor = Colors.MenuBackground,
                ItemTemplate = new DataTemplate (typeof(MenuCell)),
				VerticalOptions = LayoutOptions.FillAndExpand
            };
            listView.ItemTemplate.SetBinding (MenuCell.TextProperty, "Text");
            listView.ItemTemplate.SetBinding (MenuCell.IsSelectedProperty, "IsSelected");
            listView.SetBinding (ListView.ItemsSourceProperty, "Items");
            listView.SetBinding (ListView.SelectedItemProperty, "SelectedItem");
            return listView;
        }
    }
}

