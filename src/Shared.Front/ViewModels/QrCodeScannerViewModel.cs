using System;
using System.Threading.Tasks;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.ViewModels;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ViewModels
{
	public class QrCodeScannerViewModel : BaseViewModel
	{
		readonly IRepository _repository;

		public QrCodeScannerViewModel (IRepository repository)
		{
			_repository = repository;
		}

		internal override async Task Initialize (params object[] args)
		{
			_profile = await _repository.GetProfile();
		}

		Contact _profile;
		public Contact Profile {
			get {
				return _profile;
			}
			set {
				SetObservableProperty (ref _profile, value);
			}
		}

		public Command<object> ExchangeContacts
		{
			get {
				return new Command<object> (ExecuteContactExchangeCommand);
			}
		}

		private void ExecuteContactExchangeCommand(object parameter)
		{
			if (parameter is string == false)
			{
				Console.WriteLine ("Can not exchange contacts. Need user id.");
				return;
			}

			var contactExchange = new ContactExchange () {
				LocalUserId = _profile.Id,
				UserId = parameter as string,
			};

			_repository.SaveContactExchange (contactExchange);
		}
	}
}

