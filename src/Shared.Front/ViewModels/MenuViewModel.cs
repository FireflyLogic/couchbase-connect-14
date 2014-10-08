using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using System.Threading.Tasks;
using CouchbaseConnect2014.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace CouchbaseConnect2014.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        IRepository _repository;
		ICouchbaseService _couchbaseService;

		public MenuViewModel (IRepository repository, ICouchbaseService couchbaseService)
        {
            _repository = repository;
			_couchbaseService = couchbaseService;

            Items = new List<MenuItemViewModel> {
                new MenuItemViewModel {
                    Text = "Home",
                    ViewType = typeof(HomeView)
                },
                new MenuItemViewModel {
                    Text = "My Agenda",
                    ViewType = typeof(AgendaView)
                },
				new MenuItemViewModel {
					Text = "Conference Survey",
                    ViewType = typeof(ConferenceSurveyView)
				},
                new MenuItemViewModel {
                    Text = "Contacts",
                    ViewType = typeof(ContactsView)
                },
                new MenuItemViewModel {
                    Text = "Scavenger Hunt",
                    ViewType = typeof(Views.ScavengerHuntView)
                },
				new MenuItemViewModel {
					Text = "Profile",
					ViewType = typeof(EditProfileView)
				},
            };

            SelectedItem = Items [0];

			// receives a message from EditProfileViewModel once the user has saved their profile
			// forces the Menu header to update
			MessagingCenter.Subscribe<EditProfileViewModel> (this, "Profile", (sender) => {
				this.Initialize();
			});

            MessagingCenter.Subscribe<MenuItemViewModel> (this, "Tapped", sender => {
                SelectedItem = sender;
            });
        }

		internal override async Task Initialize (params object[] args)
        {
            await base.Initialize ();

            Profile = await _repository.GetProfile ();
        }

		bool _hasProfile;
		public bool HasProfile {
			get {
				return _hasProfile;
			}
			set {
				SetObservableProperty (ref _hasProfile, value);
			}
		}

        Contact _profile;
        internal Contact Profile {
            get {
                return _profile;
            }
            set {
                _profile = value;
                if (_profile == null) return;
                FirstName = _profile.First;
                LastName = _profile.Last;

                // checks if user currently has a saved profile.
                if (!string.IsNullOrWhiteSpace (Profile.First) && !string.IsNullOrWhiteSpace (Profile.Last))
                    HasProfile = true;
                else
                    HasProfile = false;
            }
        }

        string _firstName;
        public string FirstName {
            get {
                return _firstName;
            }
            set {
                SetObservableProperty (ref _firstName, value);
            }
        }

        string _lastName;
        public string LastName {
            get {
                return _lastName;
            }
            set {
                SetObservableProperty (ref _lastName, value);
            }
        }

        public IList<MenuItemViewModel> Items {
            get;
            set;
        }

        MenuItemViewModel _selectedItem;
        public MenuItemViewModel SelectedItem 
		{
            get 
			{
                return _selectedItem;
            }
            set 
			{
				SetObservableProperty (ref _selectedItem, value);
                if (value == null) return;
                foreach (var item in Items) item.IsSelected = item == _selectedItem;
				MessagingCenter.Send ((BaseViewModel)this, "NavigateTo", value.ViewType);
			}
        }

		public Command<object> CreateProfileCommand
		{
			get {
				return new Command<object> (ExecuteCreateProfileCommand);
			}
		}

		// opens the profile view if user taps on "Create your profile"
		private void ExecuteCreateProfileCommand(object parameter)
		{
			foreach (var item in Items) {
				if(item.ViewType == typeof(EditProfileView))
					SelectedItem = item;
			}
			//MessagingCenter.Send ((BaseViewModel)this, "NavigateTo", typeof(EditProfileView));
		}

		public ICommand AddContactCommand
		{
			get 
			{
				return new Command ((sender) =>
				{
					if (_couchbaseService.DoesUserExistOnServer() == false)
					{
						return;
					}
					
					MessagingCenter.Send ((BaseViewModel)this, "NavigateTo", typeof(QrCodeScannerView));
				});
			}
		}

    }
}

