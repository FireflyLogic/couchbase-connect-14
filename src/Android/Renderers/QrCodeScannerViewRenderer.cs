using System;
using System.Drawing;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Interop;
using Scandit;
using Scandit.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ZXing;
using AColor = Android.Graphics.Color;
using ATextAlignment = Android.Views.TextAlignment;
using AView = Android.Views.View;
using RelLayout = Android.Widget.RelativeLayout;

[assembly:ExportRenderer(typeof(CouchbaseConnect2014.QrCodeScannerView), typeof(CouchbaseConnect2014.Android.QrCodeScannerViewRenderer))]

namespace CouchbaseConnect2014.Android
{
	public class QrCodeScannerViewRenderer: PageRenderer, IScanditSDKListener
	{
		RelLayout relativeLayout;
		ScanditSDKBarcodePicker barcodePicker;
		AView overlay;
		TextView topTextView;
		TextView bottomTextView;
		ImageView qrImageView;

		protected override async void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			var metrics = Resources.DisplayMetrics;
			var activity = this.Context as Activity;

			relativeLayout = new RelLayout (activity);
			var relativeLayoutParams = new RelLayout.LayoutParams (
				RelLayout.LayoutParams.MatchParent, 
				RelLayout.LayoutParams.MatchParent
			);

			// Create topTextView - "Scan QR code to swap contact info"
			topTextView = new TextView (activity);
			topTextView.Text = "Scan QR code to swap contact info";
			topTextView.Gravity = GravityFlags.Center;
			topTextView.SetTextColor (AColor.Rgb (38, 173, 230));
			topTextView.TextSize = 14f;
			topTextView.Typeface = Typeface.CreateFromAsset (Forms.Context.Assets, "OpenSans-Bold.ttf");

			RelLayout.LayoutParams topTVLayoutParams = new RelLayout.LayoutParams(
				RelLayout.LayoutParams.MatchParent, 
				RelLayout.LayoutParams.MatchParent
			);

			relativeLayout.AddView (topTextView, topTVLayoutParams);
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * */

			/* Create the Barcode Picker View */
			barcodePicker = new ScanditSDKBarcodePicker (activity, "EeQ6GjLtEeSWsF/zcFfsWC8RqIt/+skbdZJ/MWpLIR8");
			barcodePicker.OverlayView.AddListener (this);

			// disable all codes except QR for scanning
			barcodePicker.Set1DScanningEnabled (false);
			barcodePicker.Set2DScanningEnabled (false);
			barcodePicker.SetCode128Enabled (false);
			barcodePicker.SetCode39Enabled (false);
			barcodePicker.SetCode93Enabled (false);
			barcodePicker.SetDataMatrixEnabled (false);
			barcodePicker.SetEan13AndUpc12Enabled (false);
			barcodePicker.SetEan8Enabled (false);
			barcodePicker.SetItfEnabled (false);
			barcodePicker.SetMicroDataMatrixEnabled (false);
			barcodePicker.SetUpceEnabled (false);
			/* - * - * - * - * - * - * - */
			barcodePicker.SetQrEnabled (true);
			/* - * - * - * - * - * - * - */

			RelLayout.LayoutParams bpLayoutParams = new RelLayout.LayoutParams(
				RelLayout.LayoutParams.MatchParent, 
				RelLayout.LayoutParams.MatchParent
			);
			bpLayoutParams.AddRule(LayoutRules.CenterHorizontal);

			relativeLayout.AddView (barcodePicker, bpLayoutParams);
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * */

			/* Create the overlay view -- this is to cover the bottom part of the barcode scanner view */
			overlay = new AView(activity);
			overlay.SetBackgroundColor (AColor.White);

			RelLayout.LayoutParams oLayoutParams = new RelLayout.LayoutParams(
				RelLayout.LayoutParams.MatchParent, 
				RelLayout.LayoutParams.MatchParent
			);
			oLayoutParams.AddRule(LayoutRules.AlignParentBottom);

			relativeLayout.AddView (overlay, oLayoutParams);
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * */

			// Create bottomTextView - "Share your contact info"
			bottomTextView = new TextView (activity);
			bottomTextView.Text = "Share your contact info";
			bottomTextView.Gravity = GravityFlags.Center;
			bottomTextView.SetTextColor (AColor.Rgb (38, 173, 230));
			bottomTextView.TextSize = 14f;
			bottomTextView.Typeface = Typeface.CreateFromAsset (Forms.Context.Assets, "OpenSans-Bold.ttf");

			RelLayout.LayoutParams bottomTVLayoutParams = new RelLayout.LayoutParams(
				RelLayout.LayoutParams.MatchParent, 
				RelLayout.LayoutParams.MatchParent
			);

			relativeLayout.AddView (bottomTextView, bottomTVLayoutParams);
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * */

			/* * * Generating QR code * * */
			IBarcodeWriter barcodeWriter = new BarcodeWriter 
			{ 
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Width = 175,
					Height = 175
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
			qrImageView = new ImageView (activity);
			qrImageView.SetImageBitmap (result);

			RelLayout.LayoutParams qrLayoutParams = new RelLayout.LayoutParams (
				RelLayout.LayoutParams.MatchParent, 
				RelLayout.LayoutParams.MatchParent
			);
			qrLayoutParams.AddRule (LayoutRules.CenterHorizontal);

			relativeLayout.AddView (qrImageView, qrLayoutParams);
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * */

			AddView (relativeLayout, relativeLayoutParams);

			barcodePicker.SetScanningHotSpot (0.5f, 0.2f);
			barcodePicker.OverlayView.SetViewfinderDimension (0.4f, 0.28f);
			barcodePicker.OverlayView.SetTorchEnabled (false);

			barcodePicker.StartScanning();
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);

			relativeLayout.Layout (0, 0, r, b);
			topTextView.Layout (0, 25, r, 65);

			var mswBP = MeasureSpec.MakeMeasureSpec (r - l, MeasureSpecMode.Exactly); 
			var mshBP = MeasureSpec.MakeMeasureSpec (b - t, MeasureSpecMode.Exactly); 
			barcodePicker.Measure(mswBP, mshBP);
			barcodePicker.Layout (0, 90, r - l, b - t);

			overlay.Layout (0, ((b / 2) + 40), r - l, b);
			bottomTextView.Layout (0, (b / 2) + 60, r - l, b - 100);
			qrImageView.Layout (0, (b / 2) + 100, r - l, b - 20);
		}

		/* IScanditSDKListener methods */
		public void DidScanBarcode (string barcode, string symbology) 
		{
			barcodePicker.StopScanning ();

			/* Do something once a barcode has been scanned */
			Console.WriteLine ("barcode scanned: {0}, '{1}'", symbology, barcode);
			var contactInfo = barcode.Split (',');	// user id, first, last

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
			Toast.MakeText (this.Context, message, ToastLength.Short).Show();

			barcodePicker.StartScanning ();
		}

		public void DidCancel () {
			Console.WriteLine ("Cancel was pressed.");
		}

		public void DidManualSearch (string text) {
			Console.WriteLine ("Search was used.");
		}
	}
}

