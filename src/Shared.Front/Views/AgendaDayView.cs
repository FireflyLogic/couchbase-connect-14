using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014.Views
{
    public class AgendaDayView : ContentView
    {
        public AgendaDayView ()
        {
            SetBinding (AgendaDayView.NavigationProperty, new Binding ("Navigation"));

            var listView = new ListView {
                ItemTemplate = new DataTemplate(typeof(AgendaCell)),
                RowHeight = 96
            };

            listView.SetBinding (ListView.ItemsSourceProperty, "Slots");
            listView.SetBinding (ListView.SelectedItemProperty, "SelectedSlot");

            Content = listView;
        }
    }
}

