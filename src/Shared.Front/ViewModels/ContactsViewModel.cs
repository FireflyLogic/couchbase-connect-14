using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using CouchbaseConnect2014.Models;
using System.Collections.Specialized;

namespace CouchbaseConnect2014.ViewModels
{
	public class ContactsViewModel : BaseViewModel
	{
		IRepository _repository;
		ICouchbaseService _couchbaseService;

		bool _hasInitialized = false;

		public ContactsViewModel (IRepository repository, ICouchbaseService couchbaseService)
		{
			_repository = repository;
			_couchbaseService = couchbaseService;
		}

		internal override async Task Initialize (params object[] args)
		{
			Profile = await _repository.GetProfile ();

			if (_hasInitialized == false)
			{ 
				Contacts = _repository.GetAllContacts ();
				Contacts.CollectionChanged += 
					(object sender, NotifyCollectionChangedEventArgs e) => SetContactsGrouped ();
			}
			_hasInitialized = true;
		}

        void SetContactsGrouped () {
            ContactsGrouped = CreateContactsList ();
        }

		Contact _profile;
		public Contact Profile {
			get {
				return _profile;
			}
			set {
				_profile = value;
				if (_profile == null) return;

				FirstName = _profile.First;
				LastName = _profile.Last;
				Title = _profile.Role;
			}
		}

		private string _alertMessage;
		public string AlertMessage {
			get {
				return _alertMessage;
			}
			set {
				SetObservableProperty (ref _alertMessage, value);
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

		string _title;
		public string Title {
			get {
				return _title;
			}
			set {
				SetObservableProperty (ref _title, value);
			}
		}

		bool _isEmpty;
		public bool IsEmpty {
			get {
				return _isEmpty;
			}
			set {
				SetObservableProperty (ref _isEmpty, value);
			}
		}

		ObservableCollection<Contact> _contacts;
		internal ObservableCollection<Contact> Contacts {
			get {
				return _contacts;
			}
			set {
				_contacts = value;
			}
		}

		public ObservableCollection<Grouping<string, ContactCellViewModel>> CreateContactsList ()
		{
			var cells = from contact in Contacts 
                select new ContactCellViewModel(contact);

			var sorted = from cell in cells
                orderby SafeToLower (cell.Last), SafeToLower (cell.First)
				group cell by cell.LastNameSort into contactGroup
				select new Grouping<string, ContactCellViewModel> (contactGroup.Key, contactGroup);

			return new ObservableCollection<Grouping<string, ContactCellViewModel>>(sorted);
		}

        string SafeToLower (string value)
        {
            return value == null ? null : value.ToLower ();
        }

		public ObservableCollection<Grouping<string, ContactCellViewModel>> _contactsGrouped;
		public ObservableCollection<Grouping<string, ContactCellViewModel>> ContactsGrouped { 
			get {
				return _contactsGrouped;
			}
			set {
				SetObservableProperty (ref _contactsGrouped, value);

				// check to see if you have any contacts
                IsEmpty = _contactsGrouped.Count == 0;
                SetContactsFiltered ();
			}
		}

		public ObservableCollection<Grouping<string, ContactCellViewModel>> _contactsFiltered;
		public ObservableCollection<Grouping<string, ContactCellViewModel>> ContactsFiltered { 
			get {
				return _contactsFiltered;
			}
			set {
				SetObservableProperty (ref _contactsFiltered, value);
			}
		}

        ContactCellViewModel _selectedItem;
        public ContactCellViewModel SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                _selectedItem = value;
                if (_selectedItem == null) return;
				// should clear searchbox and unfocus
				if (_selectedItem.Contact is ContactProxy) {
					_selectedItem = null;
					OnPropertyChanged("SelectedItem");
				} else {
					Navigation.PushAsync (new ContactView (_selectedItem.Contact));
				}
            }
        }

        string _searchText = "";
		public string SearchText { 
			get {
				return _searchText;
			}
			set {
				SetObservableProperty (ref _searchText, value);
				SetContactsFiltered ();
			}
		}

		// what to do as the user types in searchbox
		void SetContactsFiltered () {
            if (ContactsGrouped == null)
                return;

			ContactsFiltered = new ObservableCollection<Grouping<string, ContactCellViewModel>>(
				ContactsGrouped.Select (group => new Grouping<string, ContactCellViewModel>(group.Key, 
					group.Where (contact => contact.Last.ToLower().Contains (SearchText.ToLower()) ||
						contact.First.ToLower().Contains (SearchText.ToLower())))).Where(group => group.Any())
			);
		}

		// Place comment here.
		public class Grouping<K, T> : ObservableCollection<T>
		{
			public K Key { get; private set; }

			public Grouping(K key, IEnumerable<T> items)
			{
				Key = key;
				foreach (var item in items)
					this.Items.Add(item);
			}
		}

		public Command<object> AddContactCommand
		{
			get {
				return new Command<object> (ExecuteAddContactCommand);
			}
		}

		private void ExecuteAddContactCommand(object parameter)
		{
			if (_couchbaseService.DoesUserExistOnServer() == false)
			{
				AlertMessage = "Ah nuts... you caught us off guard. Would you please (pretty please) " + 
					"restart your app before trying to add a contact. (BTW, the step that was missed " + 
					"requires a network connection).";
				return;
			}

			if (!string.IsNullOrWhiteSpace (FirstName) && !string.IsNullOrWhiteSpace (LastName))
			{
				Navigation.PushAsync (new QrCodeScannerView ());
			} else
				AlertMessage = "Looks like you haven't saved a profile. " +
					"Head back to the menu to create your profile, and " +
					"then you'll be able to swap contact info.";
		}
	}
}