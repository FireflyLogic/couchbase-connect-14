using System;
using Xamarin.Forms;
using System.Collections;
using System.Linq;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Controls
{
    public class PipSet : StackLayout
    {
        int _selectedIndex;

        public PipSet ()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.Center;
            Spacing = 4;
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<PipSet, IList> (
                pips => pips.ItemsSource,
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PipSet)bindable).ItemsSourceChanging ();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PipSet)bindable).ItemsSourceChanged ();
                }
            );

        public IList ItemsSource {
            get {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set {
                SetValue (ItemsSourceProperty, value);
            }
        }

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<PipSet, object> (
                pips => pips.SelectedItem,
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PipSet)bindable).SelectedItemChanged ();
                });

        public object SelectedItem {
            get {
                return GetValue (SelectedItemProperty);
            }
            set {
                SetValue (SelectedItemProperty, value);
            }
        }

        void ItemsSourceChanging ()
        {
            if (ItemsSource != null)
                _selectedIndex = ItemsSource.IndexOf (SelectedItem);
        }

        void ItemsSourceChanged ()
        {
            if (ItemsSource == null) return;

            var countDelta = ItemsSource.Count - Children.Count;

            if (countDelta > 0) {
                for (var i = 0; i < countDelta; i++) {
                    Children.Add (CreatePip ());
                }
            } else if (countDelta < 0) {
                for (var i = 0; i < -countDelta; i++) {
                    Children.RemoveAt (0);
                }
            }

//            if (_selectedIndex >= 0 && _selectedIndex < ItemsSource.Count)
//                SelectedItem = ItemsSource [_selectedIndex];

//            UpdateSelection ();
        }

        void SelectedItemChanged () {
            var selectedIndex = ItemsSource.IndexOf (SelectedItem);
            var pips = Children.Cast<Image> ().ToList ();

            foreach (var pip in pips) UnselectPip (pip);

            if (selectedIndex > -1) SelectPip (pips [selectedIndex]);
        }

        static View CreatePip ()
        {
            return new Image { Source = "pip.png" };
        }

        static void UnselectPip (Image pip)
        {
            pip.Source = "pip.png";
        }

        static void SelectPip (Image pip)
        {
            pip.Source = "pip_selected.png";
        }
    }
}

