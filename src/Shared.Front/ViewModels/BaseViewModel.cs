using System;
using System.ComponentModel;
using CouchbaseConnect2014.Services;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Security.Cryptography;
using System.Text;

namespace CouchbaseConnect2014.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public INavigation Navigation { get; set; }

		internal virtual Task Initialize (params object[] args)
		{
			return Task.FromResult (0);
		}

		protected void OnPropertyChanged(string propertyName) {
			if (PropertyChanged == null) return;
			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

		protected void SetObservableProperty<T>(
			ref T field, 
			T value,
			[CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return;
			field = value;
			OnPropertyChanged (propertyName);
		}

		public static void CreateAndBind<T> (Page page, params object[] args) where T : BaseViewModel {
			page.BindingContext = App.Container.Resolve<T> ();
            page.Appearing += (object sender, EventArgs e) => {
				((BaseViewModel)((Page)sender).BindingContext).Initialize (args);
			};
		}

        protected string EmailToGravatarUri (string email, int size)
        {
            var emailToUse = string.IsNullOrWhiteSpace (email)
                ? "xyz@xyz.com"
                : email.Trim ().ToLower ();

            return string.Format (
                "http://www.gravatar.com/avatar/{0}.jpg?s={1}", 
                Md5Hash (emailToUse),
                size
            );
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

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}