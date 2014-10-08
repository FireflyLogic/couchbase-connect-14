using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ViewModels
{
    public class MenuItemViewModel : BaseViewModel
	{
        public string Text {
            get;
            set;
        }

        public Type ViewType {
            get;
            set;
        }

        bool _isSelected;
        public bool IsSelected {
            get {
                return _isSelected;
            }
            set {
                SetObservableProperty (ref _isSelected, value);
            }
        }

        public ICommand ItemTapped {
            get {
                return new Command (() => {
                    MessagingCenter.Send (this, "Tapped");
                });
            }
        }
	}
}

