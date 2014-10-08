using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class QrCodeScannerView: ContentPage
	{
		public QrCodeScannerView ()
		{
			BaseViewModel.CreateAndBind<QrCodeScannerViewModel> (this);

			Title = "Swap Contacts";
			NavigationPage.SetBackButtonTitle (this, "");

			// This view will be rendered natively
		}
	}
}

