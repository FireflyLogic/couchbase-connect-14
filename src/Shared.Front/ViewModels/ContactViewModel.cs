using System;
using CouchbaseConnect2014.ViewModels;
using System.Threading.Tasks;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.ViewModels
{
	public class ContactViewModel : BaseViewModel
	{
		public ContactViewModel ()
		{
		}

        internal override Task Initialize (params object[] args)
        {
            var task = base.Initialize (args);
            Contact = (Contact)args [0];
            return task;
        }

        Contact _contact;
        Contact Contact {
            get {
                return _contact;
            }
            set {
                _contact = value;
                Email = _contact.Email;
				HasPhone = !string.IsNullOrWhiteSpace (_contact.Phone);
				HasTwitter = !string.IsNullOrWhiteSpace (_contact.Twitter);
            }
        }

        string _email;
        string Email {
            get {
                return _email;
            }
            set {
                _email = value;
                ImageUri = EmailToGravatarUri (_email, 100);
				HasEmail = !string.IsNullOrWhiteSpace(_email);
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

		bool _hasEmail;
		public bool HasEmail {
			get {
				return _hasEmail;
			}
			set {
				SetObservableProperty (ref _hasEmail, value);
			}
		}

		bool _hasPhone;
		public bool HasPhone {
			get {
				return _hasPhone;
			}
			set {
				SetObservableProperty (ref _hasPhone, value);
			}
		}

		bool _hasTwitter;
		public bool HasTwitter {
			get {
				return _hasTwitter;
			}
			set {
				SetObservableProperty (ref _hasTwitter, value);
			}
		}
	}
}

