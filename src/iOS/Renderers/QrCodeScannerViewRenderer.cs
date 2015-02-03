using System;
using Foundation;
using UIKit;
using ScanditSDK;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ZXing;
using CoreGraphics;
using CouchbaseConnect2014.Controls;
using System.Drawing;

[assembly: ExportRenderer(typeof(CouchbaseConnect2014.QrCodeScannerView), typeof(CouchbaseConnect2014.iOS.QrCodeScannerViewRenderer))]

namespace CouchbaseConnect2014.iOS
{
	public class QrCodeScannerViewRenderer: PageRenderer
	{
		protected async override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			var view = NativeView;
			var viewController = ViewController;

			// Get the device's display for width and height.
			CGRect screen = UIScreen.MainScreen.Bounds;

			// create top label - "Scan QR code to swap contact info"
			var topLabel = new UILabel (new CGRect (
				0, 
				0, 
				screen.Width, 
				30)
			);
			topLabel.Text = "Scan QR code to swap contact info";
			topLabel.TextAlignment = UITextAlignment.Center;
			topLabel.TextColor = UIColor.FromRGB (38, 173, 230);
			topLabel.Font = UIFont.FromName (Fonts.OpenSansBold, 12);

			view.Add (topLabel);

			// create the QR code scanner & add to view
			SIBarcodePicker scanner = new SIBarcodePicker ("EeQ6GjLtEeSWsF/zcFfsWC8RqIt/+skbdZJ/MWpLIR8");

			var scannerDelegate = new QrCodeScannerDelegate () { qrScanner = scanner };
			scanner.OverlayController.Delegate = scannerDelegate;

			// disable all codes except QR for scanning
			scanner.Set1DScanningEnabled (false);
			scanner.Set2DScanningEnabled (false);
			scanner.SetCode128Enabled (false);
			scanner.SetCode39Enabled (false);
			scanner.SetCode93Enabled (false);
			scanner.SetDataMatrixEnabled (false);
			scanner.SetEan13AndUpc12Enabled (false);
			scanner.SetEan8Enabled (false);
			scanner.SetItfEnabled (false);
			scanner.SetMicroDataMatrixEnabled (false);
			scanner.SetUpceEnabled (false);
			/* - * - * - * - * - * - * - */
			scanner.SetQrEnabled (true);
			/* - * - * - * - * - * - * - */

			// Set the scanner size 
			scanner.Size = new SizeF(
                (float)screen.Size.Width, 
                (float)screen.Size.Height * 0.45f
			);
			scanner.View.Bounds = new CGRect(
				0, 
				topLabel.Frame.Height, 
				screen.Size.Width, 
				screen.Size.Height * 0.45f
			);
			scanner.View.Frame = new CGRect(
				0, 
				topLabel.Frame.Height, 
				screen.Size.Width, 
				screen.Size.Height * 0.45f
			);

			scanner.OverlayController.SetViewfinderSize (0.5f, 0.5f, 0.5f, 0.5f);
			scanner.OverlayController.SetTorchEnabled (false);

			view.Add (scanner.View);
			scanner.StartScanning ();

			/* Creating the lower label - "Share your contact info" */
			var lowerLabel = new UILabel (new CGRect (
				0, 
				(scanner.View.Frame.Location.Y + scanner.View.Frame.Height), 
				screen.Width, 
				20)
			);
			lowerLabel.Text = "Share your contact info";
			lowerLabel.TextAlignment = UITextAlignment.Center;
			lowerLabel.TextColor = UIColor.FromRGB (38, 173, 230);
			lowerLabel.Font = UIFont.FromName (Fonts.OpenSansBold, 12);

			view.Add (lowerLabel);
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * */

			/* Generating QR code  */
			var qrWidthHeight = 0;

			if (screen.Height == 480)	// iPhone 4 & 4S -- shorter screens
				qrWidthHeight = 150;
			else 						// iPhone 5, 5C, & 5S -- longer screens
				qrWidthHeight = 175;

			IBarcodeWriter barcodeWriter = new BarcodeWriter 
			{ 
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Width = qrWidthHeight,
					Height = qrWidthHeight
				}
			};


			// TODO: temp code - move to view model
			var service = TinyIoC.TinyIoCContainer.Current.Resolve<CouchbaseConnect2014.Services.ICouchbaseService> ();
			var contactId = service.GetUserId ();
			var repo = TinyIoC.TinyIoCContainer.Current.Resolve<CouchbaseConnect2014.Services.IRepository> ();
			var localUser = await repo.GetProfile ();
			// end of temp code

			var qrContent = string.Format ("{0},{1},{2}", contactId, localUser.First, localUser.Last);

			var result = barcodeWriter.Write (qrContent);

			var qrImageView = new UIImageView (new CGRect (
				0, 
				(lowerLabel.Frame.Location.Y + lowerLabel.Frame.Height), 
				result.Size.Width, 
				result.Size.Height
			));
			qrImageView.Image = result;
			qrImageView.Center = new CGPoint (
				view.Center.X, 
				qrImageView.Center.Y
			);
			/* - * - * - * - * - * - * - */

			view.Add (qrImageView);
		}
	}

	/* Class for QR code scanner delegate */
	public class QrCodeScannerDelegate: SIOverlayControllerDelegate
	{
		public SIBarcodePicker qrScanner;

		public override void DidScanBarcode (SIOverlayController overlayController, NSDictionary barcode) {
			qrScanner.StopScanning ();

			// perform actions after a barcode was scanned
			Console.WriteLine ("barcode scanned: {0}, '{1}'", barcode["symbology"], barcode["barcode"]);
			var barcodeString = NSString.FromObject (barcode ["barcode"]).ToString();
			var contactInfo = barcodeString.Split (',');	// user id, first, last


			// TODO: temp code - move to view model
			var service = TinyIoC.TinyIoCContainer.Current.Resolve<CouchbaseConnect2014.Services.ICouchbaseService> ();
			var contactId = service.GetUserId ();
			var contactExchange = new CouchbaseConnect2014.Models.ContactExchange () {
				LocalUserId = contactId,
				UserId = contactInfo[0],
				First = contactInfo[1],
				Last = contactInfo[2]
			};
			var repo = TinyIoC.TinyIoCContainer.Current.Resolve<CouchbaseConnect2014.Services.IRepository> ();
			repo.SaveContactExchange (contactExchange);
			// end of temp code

			var message = string.Format("You have swapped contact info with {0} {1}.", contactInfo[1], contactInfo[2]);

			var contactAddedAlert = new UIAlertView ("Swap Contacts", message, null, "Ok", null);
			contactAddedAlert.Show ();

			contactAddedAlert.Clicked += (object sender, UIButtonEventArgs e) => { qrScanner.StartScanning(); };
		}

		public override void DidCancel (SIOverlayController overlayController, NSDictionary status) {
			// Perform actions after cancel was pressed
		}

		public override void DidManualSearch (SIOverlayController overlayController, string text) {
			// Perform actions after search was used
		}
	}
}

