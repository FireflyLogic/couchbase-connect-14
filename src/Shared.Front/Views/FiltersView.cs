using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.ViewModels;
using System.Windows.Input;

namespace CouchbaseConnect2014
{
	public class FiltersView : ContentPage
	{
		public FiltersView ()
		{
            BaseViewModel.CreateAndBind<FiltersViewModel> (this);

			Title = "Filters";
			NavigationPage.SetBackButtonTitle (this, "");
			this.ToolbarItems.Add(CreateClearButton());

			Content = CreateTrackFilterListView ();
		}

		ToolbarItem CreateClearButton ()
		{
			var clearButton = new ToolbarItem { Name = "Clear" };
			clearButton.SetBinding (ToolbarItem.CommandProperty, "ClearTrackFilters");

			return clearButton;
		}

		View CreateTrackFilterListView ()
		{
			var trackFilterListView = new ListView {
				RowHeight = 80,
				ItemTemplate = new DataTemplate (typeof(FiltersCell))
			};

            trackFilterListView.SetBinding (ListView.ItemsSourceProperty, "TrackFilters");

			return trackFilterListView;
		}
	}
}

