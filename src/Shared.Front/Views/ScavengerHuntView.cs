using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014.Views
{
    public class ScavengerHuntView : ContentPage
    {
		ListView _listView;

        public ScavengerHuntView ()
        {
            BaseViewModel.CreateAndBind<ScavengerHuntViewModel> (this);

            Title = "Scavenger Hunt";
			NavigationPage.SetBackButtonTitle (this, "");

			SetBinding (HomeView.NavigationProperty, new Binding ("Navigation"));

			_listView = CreateListView ();

			Content = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 0,
				Children = 
				{
					CreateHeader(),
					CreateSeparator(),
					_listView
				}
			};
        }

		protected override void OnAppearing ()
		{
			_listView.SelectedItem = null;
			base.OnAppearing ();
		}

		View CreateHeader ()
		{
			return new StackLayout 
			{
				Padding = new Thickness (0, 20, 0, 20),
				Children = 
				{ 
					CreateHeaderLabel() 
				}
			};
		}

		View CreateHeaderLabel ()
		{
			var headerLabel = new FontLabel 
			{
				TextColor = Color.FromHex ("26ade6"),
				Font = Font.OfSize(Fonts.OpenSansBold, 12),
				XAlign = TextAlignment.Center
			};
			headerLabel.SetBinding (
				FontLabel.TextProperty, 
				"CaptureStats", 
				converter: new ScavengerHuntHeaderConverter ());

			var headerLabelView = new StackLayout {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness (10, 0, 10, 0),
				Children = {
					headerLabel
				}
			};

			return headerLabelView;
		}

		View CreateSeparator ()
		{
			return new BoxView 
			{
				Color = Color.FromHex ("c8c7cc"),
				HeightRequest = 0.5,
				WidthRequest = 400,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
		}

		ListView CreateListView ()
		{
			var huntListView = new ListView 
			{
				RowHeight = 105,
				ItemTemplate = new DataTemplate (typeof(ScavengerHuntCell))
			};

			huntListView.SetBinding (ListView.ItemsSourceProperty, "ScavengerHuntItems");
			huntListView.SetBinding (ListView.SelectedItemProperty, "SelectedItem");

			return huntListView;
		}
    }
}

