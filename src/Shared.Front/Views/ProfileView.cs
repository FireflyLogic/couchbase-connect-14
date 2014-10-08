using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014.Views
{
    public class ProfileView : ContentPage
    {
        public ProfileView ()
        {
            BaseViewModel.CreateAndBind<ProfileViewModel> (this);
			SetBinding (ProfileView.NavigationProperty, new Binding("Navigation"));

            Title = "Profile";
			NavigationPage.SetBackButtonTitle (this, "");

			var edit = new ToolbarItem ();
			edit.Name = "Edit";
			edit.SetBinding<ProfileViewModel> (ToolbarItem.CommandProperty, vm => vm.EditProfileCommand);

			this.ToolbarItems.Add(edit);

            Content = CreateStack ();
        }

        View CreateStack ()
        {
            var stack = new StackLayout {
                Orientation = StackOrientation.Vertical
            };

			var header = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(5, 15, 5, 5),
				BackgroundColor = Color.Default,
				Children = {
					CreateImage(),
					CreateNameLabel(),
					CreateTitleLabel()
				}

			};

			var details = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (5, 0, 0, 0),
				Children = {
					CreateEmailStack (),
					CreatePhoneStack (),
					CreateTwitterStack ()
				}
			};

			stack.Children.Add (header);
            stack.Children.Add (details);
            return stack;
        }

        View CreateImage ()
        {

			var image = new RoundedImageView {
                HeightRequest = 100,
				WidthRequest = 100
            };
            image.SetBinding (Image.SourceProperty, "ImageUri");

			var view = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					image
				}
			};

            return view;
        }

        View CreateNameLabel ()
        {
            var label = new FontLabel {

                HorizontalOptions = LayoutOptions.Center,
				Font = Font.OfSize(Fonts.OpenSans, 18)
            };
            label.SetBinding (Label.TextProperty, "FullName");
            return label;
        }

        View CreateTitleLabel ()
        {
			var label = new FontLabel {
                HorizontalOptions = LayoutOptions.Center,
				Font = Font.OfSize(Fonts.OpenSansLight, 9)
            };
            label.SetBinding (Label.TextProperty, "Title");
            return label;
        }

        View CreateEmailStack ()
        {
			var title = new FontLabel {
				Text = "email",
				Font = Font.OfSize(Fonts.OpenSans, 12),
				TextColor = Color.FromHex("26ade6")
			};

            var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (title);
            stack.Children.Add (CreateEmailLabel ());
            return stack;
        }

        View CreateEmailLabel ()
        {
			var label = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 12)
			};

            label.SetBinding (Label.TextProperty, "Email");
            return label;
        }

		View CreatePhoneStack ()
		{
			var title = new FontLabel {
				Text = "mobile",
				Font = Font.OfSize(Fonts.OpenSans, 12),
				TextColor = Color.FromHex("26ade6")
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (title);
			stack.Children.Add (CreatePhoneLabel ());
			return stack;
		}

		View CreatePhoneLabel ()
		{
			var label = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 12)
			};

			label.SetBinding (Label.TextProperty, "Phone");
			return label;
		}

		View CreateTwitterStack ()
		{

			var title = new FontLabel {
				Text = "twitter",
				Font = Font.OfSize(Fonts.OpenSans, 12),
				TextColor = Color.FromHex("26ade6")
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (title);
			stack.Children.Add (CreateTwitterLabel ());
			return stack;
		}

		View CreateTwitterLabel ()
		{
			var label = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 12)
			};

			label.SetBinding (Label.TextProperty, "Twitter");
			return label;
		}
    }
}

