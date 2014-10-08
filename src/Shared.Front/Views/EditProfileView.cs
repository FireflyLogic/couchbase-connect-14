using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class EditProfileView : ContentPage
	{

		public string Alert { 
			get {
				return (string)GetValue (AlertProperty);
			}
			set {
				SetValue (AlertProperty, value);
			}
		}

		public static readonly BindableProperty AlertProperty = 
			BindableProperty.Create<EditProfileView, string>(b => b.Alert, null, 
				propertyChanged: async (bindable, oldValue, newValue) => {
					if(newValue == null) 
						return;
					var view = (EditProfileView)bindable;
					await view.DisplayAlert ("", newValue, null, "Ok");
					((EditProfileViewModel)view.BindingContext).AlertMessage = null;
				});

//		public string Success { 
//			get {
//				return (string)GetValue (SuccessProperty);
//			}
//			set {
//				SetValue (SuccessProperty, value);
//			}
//		}
//
//		public static readonly BindableProperty SuccessProperty = 
//			BindableProperty.Create<EditProfileView, string>(b => b.Success, null, 
//				propertyChanged: async (bindable, oldValue, newValue) => {
//					if(newValue == null) 
//						return;
//					var view = (EditProfileView)bindable;
//					await view.DisplayAlert ("Success", newValue, null, "Okay");
//					((EditProfileViewModel)view.BindingContext).SuccessMessage = null;
//				});

		public EditProfileView ()
		{
			BaseViewModel.CreateAndBind<EditProfileViewModel> (this);
			SetBinding (EditProfileView.NavigationProperty, new Binding("Navigation"));
			SetBinding(AlertProperty, new Binding("AlertMessage"));
//			SetBinding(SuccessProperty, new Binding("SuccessMessage"));

			Title = "Profile";

			NavigationPage.SetBackButtonTitle (this, "");

			var save = new ToolbarItem ();
			save.Name = "Save";
			save.SetBinding<EditProfileViewModel> (ToolbarItem.CommandProperty, vm => vm.SaveProfileCommand);

			this.ToolbarItems.Add(save);

			Content = CreateStack ();
		}

		View CreateStack ()
		{

			var stack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Spacing = 0
			};

			var header = new StackLayout {
				Orientation = StackOrientation.Vertical, 
				Padding = new Thickness(5, 0, 5, 0),
				BackgroundColor = Color.Default,
				Children = {
					CreateBanner(),
				}

			};

			// holds fields for first and last name
			var name = new StackLayout {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = {
					CreateFirstNameStack(),
					CreateLastNameStack()
				}
			};

			var details = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (5, 5, 5, 0),
				Children = {
					name,
					CreateTitleStack(),
					CreateCompanyStack(),
					CreateEmailStack (),
					CreatePhoneStack (),
					CreateTwitterStack ()
				}
			};

			stack.Children.Add (header);
			stack.Children.Add (CreateSeparator ());
			stack.Children.Add (details);

			// to make fields move up as keyboard comes up
			var scrollview = new ScrollView {
				Orientation = ScrollOrientation.Vertical,
				Content = stack
			};

			return scrollview;
		}

		View CreateBanner ()
		{
			var detailStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 10,
				Padding = new Thickness(5, 0, 0, 0),
				Children = 
				{
					CreateGravatarInfoLabel (),
					CreateGravatarButton ()
				}
			};

			var bannerView = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (5, 10, 0, 10),
				Children = 
				{
					CreateProfileImage (),
					detailStack
				}
			};

			return bannerView;
		}

		View CreateSeparator ()
		{
			return new BoxView {
				Color = Color.FromHex ("c8c7cc"),
				HeightRequest = 1,
				WidthRequest = this.Width
			};
		}

		View CreateProfileImage ()
		{
			var image = new RoundedImageView 
			{
				HeightRequest = 90,
				WidthRequest = 90
			};
			image.SetBinding (Image.SourceProperty, "ImageUri");

			var view = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					image
				}
			};

			return view;
		}

		View CreateGravatarInfoLabel ()
		{
			var infoLabel = new FontLabel {
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Font = Font.OfSize (Fonts.OpenSansBold, 10),
				Text = "Profile image pulled from Gravatar using your email address",
				TextColor = Color.FromHex ("26ade6"),
				WidthRequest = 200
			};

			return infoLabel;
		}

		View CreateGravatarButton ()
		{
			// what is gravatar infomation button
			var gravatarButton = new RateButton 
			{
				Text = "What is gravatar?",
				TextColor = Color.FromHex ("26ade6"),
				Font = Font.OfSize (Fonts.OpenSans, 12),
				BackgroundColor = Color.Transparent,
				BorderColor = Color.FromHex ("26ade6"),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				BorderRadius = 7,
				BorderWidth = 1,
				HeightRequest = 35,
				WidthRequest = 125
			};
			gravatarButton.SetBinding (Button.CommandProperty, "InfoCommand");

			return gravatarButton;
		}

		View CreateFirstNameStack ()
		{
			var title = new FontLabel {
				Text = "First Name",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.HorizontalOptions = LayoutOptions.FillAndExpand;
			stack.Children.Add (view);
			stack.Children.Add (CreateFirstNameEntry ());
			return stack;
		}

		View CreateFirstNameEntry()
		{
			var first = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Placeholder = "required",
				HeightRequest = 30
			};
			first.Keyboard = Keyboard.Text;
			first.SetBinding (Entry.TextProperty, "FirstName");

			return first;
		}

		View CreateLastNameStack() {

			var title = new FontLabel {
				Text = "Last Name",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.HorizontalOptions = LayoutOptions.FillAndExpand;
			stack.Children.Add (view);
			stack.Children.Add (CreateLastNameEntry ());
			return stack;
		}

		View CreateLastNameEntry() 
		{
			var last = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Placeholder = "required",
				HeightRequest = 30
			};
			last.Keyboard = Keyboard.Text;
			last.SetBinding (Entry.TextProperty, "LastName");

			return last;
		}

		View CreateTitleStack ()
		{
			var title = new FontLabel {
				Text = "Job Title",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreateTitleEntry ());
			return stack;
		}

		View CreateTitleEntry ()
		{
			var title = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Placeholder = "required",
				HeightRequest = 30
			};
			title.Keyboard = Keyboard.Text;
			title.SetBinding (Entry.TextProperty, "Title");

			return title;
		}

		View CreateCompanyStack ()
		{
			var title = new FontLabel {
				Text = "Company",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreateCompanyEntry ());
			return stack;
		}

		View CreateCompanyEntry ()
		{
			var company = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Placeholder = "required",
				HeightRequest = 30
			};
			company.Keyboard = Keyboard.Text;
			company.SetBinding (Entry.TextProperty, "Company");

			return company;
		}

		View CreateEmailStack ()
		{
			var title = new FontLabel {
				Text = "Email",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreateEmailEntry ());
			return stack;
		}

		View CreateEmailEntry ()
		{
			var email = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				HeightRequest = 30
			};
			email.Keyboard = Keyboard.Email;
			email.SetBinding (Entry.TextProperty, "Email");

			return email;
		}

		View CreatePhoneStack ()
		{
			var title = new FontLabel {
				Text = "Phone",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreatePhoneEntry ());
			return stack;
		}

		View CreatePhoneEntry ()
		{
			var phone = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				HeightRequest = 30
			};
			phone.Keyboard = Keyboard.Telephone;
			phone.SetBinding (Entry.TextProperty, "Phone");

			return phone;
		}

		View CreateTwitterStack ()
		{

			var title = new FontLabel {
				Text = "Twitter",
				Font = Font.OfSize(Fonts.OpenSansBold, 10),
				TextColor = Color.FromHex("26ade6")
			};

			// to add padding to labels above fields
			var view = new ContentView {
				Padding = new Thickness(7, 0, 0, 0),
				Content = title
			};

			var stack = new StackLayout ();
			stack.Spacing = 0;
			stack.Children.Add (view);
			stack.Children.Add (CreateTwitterEntry ());
			return stack;
		}

		View CreateTwitterEntry ()
		{
			var twitter = new FontEntry {
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				HeightRequest = 30
			};
			twitter.Keyboard = Keyboard.Text;
			twitter.SetBinding (Entry.TextProperty, "Twitter");

			return twitter;
		}
	}
}

