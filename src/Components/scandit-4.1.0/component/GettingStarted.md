Scandit SDK Barcode Scanner
===========================================

Using the Scandit SDK Barcode Scanner component in your Xamarin app is as simple as:

* downloading and installing the component
* signing up for an API key at http://www.scandit.com (free for the enterprise trial)
* integrating the Scandit SDK component into your app by implementing the callbacks for successful scans and instantiating the ScanditSDKBarcodePicker as shown in the example code below.

Install the Scandit SDK component
---------------------------------

Add the Scandit Barcode Scanner to your project via the Component Manager.

Get an Scandit SDK App Key
--------------------------

[Sign up](http://www.scandit.com/pricing) for a free enterprise trial license and copy your app key from within your Scandit web account.


iOS: Integrate the Scandit SDK into Your iOS App
---------------------------------------

To handle the callbacks when a barcode is scanned or the user presses the cancel button, one of the classes in your app needs to implement the ScanditSDK.SIOverlayControllerDelegate interface:

```csharp
using ScanditSDK;

public class YourDelegate : SIOverlayControllerDelegate
{
	public override void DidScanBarcode (SIOverlayController overlayController, NSDictionary barcode) {
		// perform actions after a barcode was scanned
		Console.WriteLine ("barcode scanned: {0}, '{1}'", barcode["symbology"], barcode["barcode"]);
	}

	public override void DidCancel (SIOverlayController overlayController, NSDictionary status) {
		// perform actions after cancel was pressed
	}

	public override void DidManualSearch (SIOverlayController overlayController, string text) {
		// perform actions after search was used
	}
}
```

To start the Scandit SDK Barcode Scanner, instantiate the ScanditSDK.SIBarcodePicker object, set the delegate and present the scan view.

Make sure you pass the APP key from your Scandit account to the ScanditSDK.SIBarcodePicker object.


```csharp
// Setup the barcode scanner
var picker = new ScanditSDK.SIBarcodePicker ("ENTER YOUR APP KEY HERE");
picker.OverlayController.Delegate = new YourDelegate ();

// Display the Scandit user interface
some_ui_view_controller_object.PresentViewController (picker, true, null);

picker.StartScanning ();
```

Android: Integrate the Scandit SDK into Your Android App
---------------------------------------

Instantiate the ScanditSDKBarcodePicker, start the scanner, show the scan user interface and handle the callbacks of the Scandit.Interfaces.IScanditSDKListener interface and add permissions for CAMERA, INTERNET and VIBRATE.

```csharp

public class ScanActivity : Activity, Scandit.Interfaces.IScanditSDKListener
	{
		private ScanditSDKBarcodePicker picker;
		public static string appKey = "---- ENTER YOUR APP KEY HERE - SIGN UP AT WWW.SCANDIT.COM ----";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Setup the barcode scanner
			picker = new ScanditSDKBarcodePicker (this, appKey);
			picker.OverlayView.AddListener (this);

			// Start scanning
			picker.StartScanning ();

			// Show scan user interface
			SetContentView (picker);
		}

		public void DidScanBarcode (string barcode, string symbology) {
			Console.WriteLine ("barcode scanned: {0}, '{1}'", symbology, barcode);

		public void DidCancel () {
			Console.WriteLine ("Cancel was pressed.");
		}

		public void DidManualSearch (string text) {
			Console.WriteLine ("Search was used.");
		}
}
```

Note: Do not forget to add permissions in the Android manifest file for CAMERA, INTERNET and VIBRATE.



More information
----------------

For more details, check out the sample projects that come with the Scandit SDK component.

Detailed guides and API information is available at [www.scandit.com/support](http://www.scandit.com/support).


Support
-------

Questions? Contact `info@scandit.com`.
