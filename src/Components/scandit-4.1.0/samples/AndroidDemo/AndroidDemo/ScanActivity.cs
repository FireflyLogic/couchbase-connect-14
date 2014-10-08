using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Scandit;
using Scandit.Interfaces;

namespace XamarinScanditSDKDemoAndroid
{
	[Activity (Label = "ScanActivity")]			
	public class ScanActivity : Activity, Scandit.Interfaces.IScanditSDKListener
	{
		private ScanditSDKAutoAdjustingBarcodePicker picker;
		public static string appKey = "---- ENTER YOUR APP KEY HERE - SIGN UP AT WWW.SCANDIT.COM ----";		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			RequestWindowFeature (WindowFeatures.NoTitle);
			Window.SetFlags (WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

			// Setup the barcode scanner
			picker = new ScanditSDKAutoAdjustingBarcodePicker (this, appKey, ScanditSDK.CameraFacingBack);
			picker.OverlayView.AddListener (this);

			// Show the scan user interface
			SetContentView (picker);
		}

		public void DidScanBarcode (string barcode, string symbology) {
			Console.WriteLine ("barcode scanned: {0}, '{1}'", symbology, barcode);

			// Call GC.Collect() before stopping the scanner as the garbage collector for some reason does not 
			// collect objects without references asap but waits for a long time until finally collecting them.
			GC.Collect ();

			// stop the camera
			picker.StopScanning ();

			AlertDialog alert = new AlertDialog.Builder (this)
				.SetTitle (symbology + " Barcode Detected")
					.SetMessage (barcode)
					.SetPositiveButton("OK", delegate {
						picker.StartScanning ();
					})
					.Create ();

			alert.Show ();
		}

		public void DidCancel () {
			Console.WriteLine ("Cancel was pressed.");
			Finish ();
		}

		public void DidManualSearch (string text) {
			Console.WriteLine ("Search was used.");
		}

		protected override void OnResume () {
			picker.StartScanning ();
			base.OnResume ();
		}

		protected override void OnPause () {
			// Call GC.Collect() before stopping the scanner as the garbage collector for some reason does not 
			// collect objects without references asap but waits for a long time until finally collecting them.
			GC.Collect ();
			picker.StopScanning ();
			base.OnPause ();
		}

		public override void OnBackPressed () {
			base.OnBackPressed ();
			Finish ();
		}
	}
}

