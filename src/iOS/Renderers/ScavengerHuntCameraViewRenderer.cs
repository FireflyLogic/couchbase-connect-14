using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Media;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.iOS;

[assembly: ExportRenderer(typeof(ScavengerHuntCameraView), typeof(ScavengerHuntCameraViewRenderer))]

namespace CouchbaseConnect2014.iOS
{
	public class ScavengerHuntCameraViewRenderer : PageRenderer
	{
//		protected override void OnElementChanged (VisualElementChangedEventArgs e)
//		{
//			base.OnElementChanged (e);
//
//			var view = NativeView;
//			var viewController = ViewController;
//
//			// Get the device's display for width and height.
//			RectangleF screen = UIScreen.MainScreen.Bounds;
//
//		}
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// Create media picker/camera
			var mediaPicker = new MediaPicker();
			MediaPickerController cameraController = mediaPicker.GetTakePhotoUI (new StoreCameraMediaOptions 
			{
				Name = "TestImage.jpg",
				Directory = "Couchbase Scavenger Hunt",
				DefaultCamera = CameraDevice.Rear
			});

			PresentViewController (cameraController, true, null);

			cameraController.GetResultAsync().ContinueWith (t => 
			{
				cameraController.DismissViewController (true, () => 
				{
					MediaFile file = t.Result;
					Console.WriteLine("--> file: {0}", file.Path);
				});

			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}

