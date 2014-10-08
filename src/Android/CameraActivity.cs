using System.Collections.Generic;
using Android.Content.PM;
using Android.Net;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Android.OS;
using Android.App;
using Android.Content;
using Android.Provider;
using Xamarin.Media;
using System.Threading.Tasks;
using Android.Preferences;
using System;

namespace CouchbaseConnect2014.Android
{
	[Activity]
	public class CameraActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var picker = new MediaPicker (this);
			if (!picker.IsCameraAvailable)
				System.Console.WriteLine ("No camera!");
			else {
				var intent = picker.GetTakePhotoUI (new StoreCameraMediaOptions {
					Name = "test.jpg",
					Directory = "CouchbaseConnect",
				});
				StartActivityForResult (intent, 0);
			}
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (resultCode == Result.Ok) {
				data.GetMediaFileExtraAsync (this).ContinueWith (t => 
					{
						// save image in local database
						System.Console.WriteLine("--> MediaFile: {0}", t.Result.Path);

						// save image path to preferences so you can retrieve it
						// in previous view
//						ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this); 
//						ISharedPreferencesEditor editor = prefs.Edit();
//						editor.PutString("image_path", t.Result.Path);
//						editor.Apply();

					}, TaskScheduler.FromCurrentSynchronizationContext());
			}

//			// make it available in the gallery
//			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
//			if (_file != null) {
//				Uri contentUri = Uri.FromFile (_file);
//				mediaScanIntent.SetData (contentUri);
//				SendBroadcast (mediaScanIntent);
//			}

			// this activity should be done after you save
			// result from camera intent
			Finish ();
		}

//		private void dispatchTakePictureIntent() {
//			Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
//			if (takePictureIntent.ResolveActivity(PackageManager) != null) {
//				StartActivityForResult(takePictureIntent, 1);
//			}
//		}
	}
}

