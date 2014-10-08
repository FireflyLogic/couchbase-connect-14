using System;
using CouchbaseConnect2014.ViewModels;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014
{
	public class ContactCellViewModel : BaseViewModel
	{
        public ContactCellViewModel (Contact contact)
        {
            Contact = contact;
			IsProxy = contact is ContactProxy;
            First = contact.First;
            Last = contact.Last;
            Company = contact.Company;
            Role = contact.Role;
            Email = contact.Email;
            Phone = contact.Phone;
            Twitter = contact.Twitter;
        }

        internal Contact Contact { get; private set; }

		bool _isProxy;
		public bool IsProxy
		{
			get { return _isProxy; }
			set { SetObservableProperty (ref _isProxy, value); }
		}

		string _email;
		public string Email {
			get {
				return _email;
			}
			set {
				SetObservableProperty (ref _email, value);
                ImageUri = EmailToGravatarUri (_email, 60);
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

		public string First {
			get;
			set;
		}

		public string Last {
			get;
			set;
		}

		public string Role {
			get;
			set;
		}

		public string Company {
			get;
			set;
		}

		public string Phone {
			get;
			set;
		}

		public string Twitter {
			get;
			set;
		}

		// Place comment here.
		public string LastNameSort
		{
			get
			{
				if (string.IsNullOrWhiteSpace(Last) || Last.Length == 0)
					return "?";

				return Last[0].ToString().ToUpper();
			}
		}
	}
}

