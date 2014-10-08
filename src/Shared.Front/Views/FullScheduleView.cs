using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Views
{
	public class FullScheduleView : ContentPage
	{
		public FullScheduleView ()
		{
            BaseViewModel.CreateAndBind<FullScheduleViewModel> (this);
            SetBinding (FullScheduleView.NavigationProperty, new Binding ("Navigation"));

            Title = "Full Schedule";
			BackgroundColor = Color.Default;
			NavigationPage.SetBackButtonTitle (this, "");

            this.ToolbarItems.Add (CreateFiltersItem ());
            SetBinding (ToolbarIconProperty, new Binding ("FilterIcon"));
            Content = CreateRootStack ();
		}

        public static BindableProperty ToolbarIconProperty =
            BindableProperty.Create<FullScheduleView, string> (
                b => b.ToolbarIcon,
                "filter_0.png",
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((FullScheduleView)bindable).SetToolbarIcon (newValue);
                });

        public string ToolbarIcon {
            get {
                return (string)GetValue (ToolbarIconProperty);
            }
            set {
                SetValue (ToolbarIconProperty, value);
            }
        }

        void SetToolbarIcon (string newValue)
        {
            var toolbarItem = ToolbarItems [0];
            toolbarItem.Icon = newValue;
            ToolbarItems [0] = toolbarItem;
        }

        static ToolbarItem CreateFiltersItem ()
        {
            var filters = new ToolbarItem {
                Icon = "filter.png",
                Name = "Filter"
            };

            filters.SetBinding (ToolbarItem.CommandProperty, "ShowFilters");
            return filters;
        }

        View CreateRootStack ()
        {
            return new StackLayout {
				Spacing = 0,
                Children = {
                    CreateDayRow (),
                    CreatePips (),
					CreateSeparator(),
                    CreateSessonListView ()
                }
            };
        }

        View CreateDayRow ()
        {
            var carousel = new CarouselLayout {
                HeightRequest = 40,
                ItemTemplate = new DataTemplate (typeof(DayHeader))
            };
            carousel.SetBinding (CarouselLayout.ItemsSourceProperty, "FullScheduleDays");
            carousel.SetBinding (CarouselLayout.SelectedItemProperty, "SelectedDay");
            return carousel;
        }

        View CreatePips ()
        {
			var pips = new PipSet {
				TranslationY = -10
			};
            pips.SetBinding (PipSet.ItemsSourceProperty, "FullScheduleDays");
            pips.SetBinding (PipSet.SelectedItemProperty, "SelectedDay");
            return pips;
        }

		View CreateSeparator ()
		{
			var line = new BoxView 
			{
				WidthRequest = 320,
				HeightRequest = 1.0,
				Color = Color.FromHex ("c8c7cc"),
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			return line;
		}

        View CreateSessonListView ()
        {
            var listView = new ListView {
                IsGroupingEnabled = true,
                RowHeight = 100,
				HasUnevenRows = true,

                GroupDisplayBinding = new Binding ("StartTime", 
                    converter: new DateTimeValueConverter ("h:mm tt")),
				GroupHeaderTemplate = new DataTemplate(typeof(FullScheduleHeaderCell)),

                ItemTemplate = new DataTemplate (typeof(FullScheduleCell))
            };

            listView.SetBinding (ListView.ItemsSourceProperty, "Slots");
            listView.SetBinding (ListView.SelectedItemProperty, "SelectedSession");

            return listView;
        }
	}
}

