using System;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Models;
using Xamarin.Forms;
using System.Security.Cryptography;
using System.Text;
using CouchbaseConnect2014.Services;
using System.Threading.Tasks;
using CouchbaseConnect2014.Views;

namespace CouchbaseConnect2014
{
	public class EditProfileViewModel : BaseViewModel
	{
		readonly IRepository _repository;

		public EditProfileViewModel (IRepository repository)
		{
			_repository = repository;
		}

		internal override async Task Initialize (params object[] args)
		{
			Profile = await _repository.GetProfile();
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
				Company = _profile.Company;
				Email = _profile.Email;
				Phone = _profile.Phone;
				Twitter = _profile.Twitter;
			}
		}

		string _name;
		public string FullName {
			get {
				return _name;
			}
			set {
				SetObservableProperty (ref _name, value);
			}
		}

		string _firstName;
		public string FirstName {
			get {
				return _firstName;
			}
			set {
				SetObservableProperty (ref _firstName, value);
                UpdateFullName ();
			}
		}

		string _lastName;
		public string LastName {
			get {
				return _lastName;
			}
			set {
				SetObservableProperty (ref _lastName, value);
                UpdateFullName ();
			}
		}

		void UpdateFullName ()
		{
			FullName = string.Format ("{0} {1}", FirstName, LastName);
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

		string _company;
		public string Company {
			get {
				return _company;
			}
			set {
				SetObservableProperty (ref _company, value);
			}
		}

		string _email;
		public string Email {
			get {
				return _email;
			}
			set {
				SetObservableProperty (ref _email, value);
                ImageUri = EmailToGravatarUri (_email, 90);
			}
		}

		string _phone;
		public string Phone {
			get {
				return _phone;
			}
			set {
				SetObservableProperty (ref _phone, value);
			}
		}

		string _twitter;
		public string Twitter {
			get {
				return _twitter;
			}
			set {
				SetObservableProperty (ref _twitter, value);
			}
		}

		string _imageUri;
		public string ImageUri {
			get {
				return _imageUri;
			}
			set {
                SetObservableProperty (ref _imageUri, value);
			}
		}

		string Md5Hash (string value)
		{
			var hash = MD5.Create ();
			var data = hash.ComputeHash (Encoding.UTF8.GetBytes (value));
			var builder = new StringBuilder ();
			for (var i = 0; i < data.Length; i++) {
				builder.Append (data [i].ToString ("x2"));
			}
			return builder.ToString ();
		}


//		private string _successMessage;
//		public string SuccessMessage {
//			get {
//				return _successMessage;
//			}
//			set {
//				SetObservableProperty (ref _successMessage, value);
//			}
//		}

		public Command<object> SaveProfileCommand
		{
			get {
				return new Command<object> (ExecuteSaveProfileCommand);
			}
		}

		private void ExecuteSaveProfileCommand(object parameter)
		{
			var result = inputValidation ();

			if (result) {

				CaptureProfile ();
				_repository.SaveProfile (_profile);

				// notifies the Menu that the header needs to update with the newly saved
				// profile
				MessagingCenter.Send<EditProfileViewModel> (this, "Profile");

				MessagingCenter.Send ((BaseViewModel)this, "NavigateTo", typeof(HomeView));

				//SuccessMessage = "Profile saved.";

			} else {

				AlertMessage = "Come on, give us something to work with! We need at least First Name, Last Name, Title, and Company.";

			}
		}

		private void CaptureProfile() {

			_profile.First = FirstName.Trim();
			_profile.Last = LastName.Trim();
			_profile.Role = Title.Trim();
			_profile.Company = Company.Trim();
			_profile.Email = Email.Trim();
			_profile.Phone = Phone.Trim();
			_profile.Twitter = Twitter.Trim();
		}

		private Boolean inputValidation() {

			bool isValid = true;

			if (string.IsNullOrWhiteSpace (_firstName))
				isValid = false;
			if (string.IsNullOrWhiteSpace (_lastName))
				isValid = false;
			if (string.IsNullOrWhiteSpace (_title))
				isValid = false;
			if (string.IsNullOrWhiteSpace (_company))
				isValid = false;

			return isValid;
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

		public Command<object> InfoCommand
		{
			get {
				return new Command<object> (ExecuteInfoCommand);
			}
		}

		// opens default brower to "What is Gravatar?" link
		private void ExecuteInfoCommand(object parameter)
		{
			Device.OpenUri(new Uri("https://en.gravatar.com/support/what-is-gravatar/"));
		}
	}
}

