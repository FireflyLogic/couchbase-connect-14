using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014.Views
{
    public class AgendaView : ContentPage
    {
        public AgendaView ()
        {
            BaseViewModel.CreateAndBind<AgendaViewModel> (this);

            Title = "My Agenda";
			BackgroundColor = Color.Default;
			NavigationPage.SetBackButtonTitle (this, "");
            SetBinding (NavigationProperty, new Binding ("Navigation"));

			this.ToolbarItems.Add(CreateFullScheduleItem ());

            Content = CreateRootStack ();
        }

        ToolbarItem CreateFullScheduleItem ()
        {
			var toolbarItem = new ToolbarItem { Name = "See All" };
            toolbarItem.SetBinding (ToolbarItem.CommandProperty, "ShowFullSchedule");
            return toolbarItem;
        }

        View CreateRootStack ()
        {
            return new StackLayout {
                Spacing = 0,
                Children = {
                    CreateDayRow (),
                    CreatePips (),
                    CreateSeparator (),
                    CreateSlotList ()
                }
            };
        }

        View CreateDayRow ()
        {
            var carousel = new CarouselLayout {
                HeightRequest = 40,
                ItemTemplate = new DataTemplate (typeof(DayHeader))
            };
            carousel.SetBinding (CarouselLayout.ItemsSourceProperty, "AgendaDays", BindingMode.OneWay);
            carousel.SetBinding (CarouselLayout.SelectedItemProperty, "SelectedDay", BindingMode.TwoWay);
            return carousel;
        }

        View CreatePips ()
        {
            var pips = new PipSet {
                TranslationY = -10
            };
            pips.SetBinding (PipSet.ItemsSourceProperty, "AgendaDays", BindingMode.OneWay);
            pips.SetBinding (PipSet.SelectedItemProperty, "SelectedDay", BindingMode.OneWay);
            return pips;
        }

        View CreateSeparator ()
        {
            return new BoxView {
				Color = Color.FromHex ("c8c7cc"),
                HeightRequest = 0.5,
                WidthRequest = this.Width
            };
        }

        View CreateSlotList ()
        {
            return new AgendaDayView ();
        }
    }
}

