using System;
using System.Threading.Tasks;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using Xamarin.Forms;
using System.Security.Cryptography;
using System.Text;

namespace CouchbaseConnect2014.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        readonly IRepository _repository;

		public ProfileViewModel (IRepository repository)
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
                FirstName = _profile.First;
                LastName = _profile.Last;
                Title = _profile.Role;
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

        string _email;
        public string Email {
            get {
                return _email;
            }
            set {
                SetObservableProperty (ref _email, value);
                ImageUri = EmailToGravatarUri (_email, 100);
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

		public Command<object> EditProfileCommand
		{
			get {
				return new Command<object> (ExecuteEditProfileCommand);
			}
		}

		private void ExecuteEditProfileCommand(object parameter)
		{

			Navigation.PushAsync(new EditProfileView());
		}
    }
}
