using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class ContactView : ContentPage
	{
		Contact _contact;

		public ContactView (Contact contact)
		{
			BaseViewModel.CreateAndBind<ContactViewModel> (this, contact);

			Title = "Contact";

			_contact = contact;

			Content = CreateView ();
		}

		View CreateView() {
			var view = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Spacing = 0
			};

			view.Children.Add (CreateHeader ());
			view.Children.Add (CreateSeparator ());
			view.Children.Add (CreateDetails ());

			return view;
		}

		View CreateHeader() {

			var view = new StackLayout {
				Spacing = 0,
				BackgroundColor = Color.Default,
				Padding = new Thickness(0, 15, 0, 15),
				Children = {
					CreateImage (),
					CreateNameLabel (),
					CreateTitleLabel (),
				}
			};

			return view;
		}

		View CreateDetails() {

			var view = new StackLayout {
				Spacing = 10,
				Padding = new Thickness(20, 20, 20, 0),
				Children = {
					CreateEmailHeader (),
					CreatePhoneHeader (),
					CreateTwitterHeader (),
				}
			};

			return view;
		}

		View CreateImage ()
		{
			var image = new RoundedImageView {
				Source = _contact.Email,
				HeightRequest = 100,
				WidthRequest = 100
			};
			image.SetBinding (Image.SourceProperty, "ImageUri");

			var view = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness(0, 0, 0, 15),
				Children = {
					image
				}
			};

			return view;
		}

		View CreateNameLabel ()
		{
			var first = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansLight, 18),
				Text = _contact.First
			};

			var last = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 18),
				Text = _contact.Last
			};

			var name = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					first,
					last
				}
			};

			return name;
		}

		View CreateTitleLabel ()
		{
			var label = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansBold, 10),
				Text = _contact.Role + ", " + _contact.Company,
				HorizontalOptions = LayoutOptions.Center,
			};

			return label;
		}

		View CreateSeparator ()
		{
			return new BoxView {
				Color = Color.FromHex ("c8c7cc"),
				HeightRequest = 1,
				WidthRequest = this.Width
			};
		}

		View CreateEmailHeader ()
		{
			var title = new FontLabel {
				Text = "Email",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreateEmailLabel ());
			stack.SetBinding (StackLayout.IsVisibleProperty, "HasEmail");

			return stack;
		}

		View CreateEmailLabel ()
		{

			var label = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Text = _contact.Email
			};

			return label;
		}

		View CreatePhoneHeader ()
		{

			var title = new FontLabel {
				Text = "Phone",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreatePhoneLabel ());
			stack.SetBinding (StackLayout.IsVisibleProperty, "HasPhone");

			return stack;
		}

		View CreatePhoneLabel ()
		{
			var label = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Text = _contact.Phone
			};

			return label;
		}

		View CreateTwitterHeader ()
		{

			var title = new FontLabel {
				Text = "Twitter",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreateTwitterLabel ());
			stack.SetBinding (StackLayout.IsVisibleProperty, "HasTwitter");

			return stack;
		}

		View CreateTwitterLabel ()
		{
			var label = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Text = _contact.Twitter
			};

			return label;
		}
	}
}

