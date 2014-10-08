using System;
using System.Globalization;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Media;
using CouchbaseConnect2014.Android;
using CouchbaseConnect2014.Views;

[assembly:ExportRenderer(typeof(CameraContentPage), typeof(CameraContentPageRenderer))]

namespace CouchbaseConnect2014.Android
{
	public class CameraContentPageRenderer : PageRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			var cv = (CameraContentPage)this.Element;
			cv.CameraRequested += (object sender, EventArgs args) => 
			{
				PresentCamera();
			};
		}

		async void PresentCamera ()
		{

			var picker = new MediaPicker (Context);
			if (!picker.IsCameraAvailable)
				System.Console.WriteLine ("No camera!");
			else {
				try {
					// TakePhotoAsync is deprecated?
					var file = await picker.TakePhotoAsync (new StoreCameraMediaOptions {
						Name = "test.jpg",
						Directory = "CouchbaseConnect",
						DefaultCamera = CameraDevice.Rear
					});
					Console.WriteLine(file.Path);
					((CameraContentPage)this.Element).Captured = file.Path;
				} catch (Exception e) {
					Console.WriteLine ("Take Photo Cancelled.");
				}
				((CameraContentPage)this.Element).IsPresented = false;
			}
		}
	}
}

