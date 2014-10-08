using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ViewModels
{
    public class RootViewModel : BaseViewModel
    {
        public RootViewModel ()
        {
            MessagingCenter.Subscribe (this, "NavigateTo", 
				(BaseViewModel sender, Type viewType) => {
					DetailViewType = viewType;
				});

            MessagingCenter.Subscribe<MenuItemViewModel> (this, "Tapped", sender => {
                IsPresented = false;
            });
        }

        Type _detailViewType;
        public Type DetailViewType {
            get {
                return _detailViewType;
            }
            set {
                SetObservableProperty (ref _detailViewType, value);
            }
        }

        bool _isPresented;
        public bool IsPresented {
            get {
                return _isPresented;
            }
            set {
                SetObservableProperty (ref _isPresented, value);
            }
        }
    }
}

