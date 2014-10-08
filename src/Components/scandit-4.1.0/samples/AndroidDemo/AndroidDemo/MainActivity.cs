using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinScanditSDKDemoAndroid
{
	[Activity (Label = "XamarinScanditSDKDemoAndroid", MainLauncher = true)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (ScanActivity.appKey.Length != 43) {
				AlertDialog alert = new AlertDialog.Builder (this)
					.SetTitle ("App key not set")
						.SetMessage ("Please set the app key in the ScanActivity class.")
						.SetPositiveButton("OK", delegate {})
						.Create ();

				alert.Show ();
			}

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				// start the scanner
				StartActivity(typeof(ScanActivity));
			};
		}
	}


}


