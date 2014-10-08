using System;
using Xamarin.Forms;
using System.ComponentModel;
using CouchbaseConnect2014.ViewModels;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Views
{
    public class RootView : MasterDetailPage
    {
        readonly Dictionary<Type, NavigationPage> _navigationPages;

        public RootView ()
        {
            _navigationPages = new Dictionary<Type, NavigationPage> ();

            ViewModel = new RootViewModel ();
            ViewModel.PropertyChanged += ViewModelPropertyChanged;

            Title = "Couchbase Connect";
            Master = new MenuView ();

			// is the menu able to be swiped out?
            IsGestureEnabled = false;

            SetBinding (IsPresentedProperty, new Binding ("IsPresented", BindingMode.TwoWay));

            IsPresentedChanged += (object sender, EventArgs e) => {
                if (IsPresented) {
                    UnfocusAll (Detail);
                }
            };
        }

        void UnfocusAll (Page page)
        {
            NavigationPage navigationPage;
            ContentPage contentPage;

            if ((navigationPage = page as NavigationPage) != null) {
                UnfocusAll (navigationPage.CurrentPage);
            } else if ((contentPage = page as ContentPage) != null) {
                UnfocusAll (contentPage.Content);
            }
        }

        void UnfocusAll (View view) {
            Layout<View> layoutOfView;
            ScrollView scrollView;
            ContentView contentView;

            if (view == null) return;

            if ((layoutOfView = view as Layout<View>) != null) {
                foreach (var child in layoutOfView.Children)
                    UnfocusAll (child);
            } else if ((scrollView = view as ScrollView) != null) {
                UnfocusAll (scrollView.Content);
            } else if ((contentView = view as ContentView) != null) {
                UnfocusAll (contentView.Content);
            } else {
                view.Unfocus ();
            }
        }

        private RootViewModel ViewModel {
            get {
                return (RootViewModel)BindingContext;
            }
            set {
                BindingContext = value;
            }
        }

        void ViewModelPropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DetailViewType") NavigateTo (ViewModel.DetailViewType);
        }

        void NavigateTo (Type viewType) {
            NavigationPage navPage;

            if (!_navigationPages.TryGetValue (viewType, out navPage)) {
                var page = (Page)Activator.CreateInstance (viewType);
                navPage = new NavigationPage (page) {
                    BarTextColor = Color.FromHex ("ea2228"),
                    BarBackgroundColor = Color.White
                };
                _navigationPages.Add (viewType, navPage);
            }

            Detail = navPage;
            IsPresented = false;
        }
    }
}

