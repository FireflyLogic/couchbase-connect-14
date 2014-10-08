using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.Views
{
	public class ContactsView : ContentPage
	{
		ListView _listView;

		public ContactsView ()
		{
            BaseViewModel.CreateAndBind<ContactsViewModel> (this);
			SetBinding (ContactsView.NavigationProperty, new Binding("Navigation"));
			SetBinding(AlertProperty, new Binding("AlertMessage"));

			Title = "Contacts";
			NavigationPage.SetBackButtonTitle (this, "");

			var addContactButton = new ToolbarItem
			{
				Icon = "plus.png",
				Name = "Add Contact",
			};
			addContactButton.SetBinding<ContactsViewModel> (ToolbarItem.CommandProperty, vm => vm.AddContactCommand);

			this.ToolbarItems.Add(addContactButton);

			var relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var searchBar = CreateSearchBar ();
			relativeLayout.Children.Add (searchBar,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToParent ((parent) => { return parent.Y; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; })
			);

			_listView = CreateList ();

			relativeLayout.Children.Add (_listView,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToView   (searchBar, (parent,sibling) => { return sibling.Height; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToView (searchBar, (parent,sibling) => { return parent.Height - sibling.Height; })
			);

			var gettingStarted = CreateGettingStartedView ();
			relativeLayout.Children.Add (gettingStarted,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToParent ((parent) => { return (parent.Height / 2) - 100; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height - 175; })
			);

			Content = relativeLayout;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			_listView.SelectedItem = null;
		}

		public string Alert { 
			get {
				return (string)GetValue (AlertProperty);
			}
			set {
				SetValue (AlertProperty, value);
			}
		}

		public static readonly BindableProperty AlertProperty = 
			BindableProperty.Create<ContactsView, string>(b => b.Alert, null, 
				propertyChanged: async (bindable, oldValue, newValue) => {
					if(newValue == null) 
						return;
					var view = (ContactsView)bindable;
					await view.DisplayAlert ("", newValue, "Ok");
					((ContactsViewModel)view.BindingContext).AlertMessage = null;
				});


		View CreateSearchBar() {
		
			CustomSearchBar searchBar = new CustomSearchBar
			{
				Placeholder = "Search",
				BarTint = Color.FromRgb(247, 247, 247)
			};
			searchBar.SetBinding (SearchBar.TextProperty, "SearchText");

			return searchBar;
		}

		ListView CreateList() {

            ListView listView = new ListView {
                ItemTemplate = new DataTemplate (typeof(ContactCell)),
                RowHeight = 75,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding ("Key"),
                GroupShortNameBinding = new Binding ("Key"),
                HasUnevenRows = true
            };

            listView.SetBinding (ListView.ItemsSourceProperty, "ContactsFiltered");
			listView.SetBinding (ListView.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
			
            if(Device.OS != TargetPlatform.WinPhone)
				listView.GroupHeaderTemplate = new DataTemplate(typeof(ContactHeaderCell));

			listView.SetBinding (ListView.IsVisibleProperty, "IsEmpty", converter: new NegateValueConverter());

			return listView;
		}

		View CreateGettingStartedView() {

			var header = new FontLabel {
				Text = "Getting Started",
				Font = Font.OfSize (Fonts.OpenSansBold, 18),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			var details = new FontLabel {
				Text = "Looks like your contact list is empty. " +
					"Get out there and make some connections! " +
					"\n(Tap the + sign to add contacts)",
				Font = Font.OfSize (Fonts.OpenSansLight, 14),
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			var view = new StackLayout {
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(30, 5, 30, 5),
//				BackgroundColor = Color.Default,
				IsVisible = false,
				Children = {
					header,
					details
				}
			};
			view.SetBinding (StackLayout.IsVisibleProperty, "IsEmpty");

			return view;
		}
	}

	public class CustomSearchBar : SearchBar {
		// Use Bindable properties to maintain XAML binding compatibility

		public static readonly BindableProperty BarTintProperty = BindableProperty.Create<CustomSearchBar, Color?>(p => p.BarTint, null);
		public Color? BarTint
		{
			get { return (Color?)GetValue(BarTintProperty); }
			set { SetValue(BarTintProperty, value); }
		}
	}
}

