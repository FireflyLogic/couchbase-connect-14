using System;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Media;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.iOS.Renderers;
using CouchbaseConnect2014.Views;

[assembly:ExportRenderer(typeof(CameraContentPage), typeof(CameraContentPageRenderer))]

namespace CouchbaseConnect2014.iOS.Renderers
{
	public class CameraContentPageRenderer : PageRenderer
	{
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var cv = (CameraContentPage)this.Element;
			cv.CameraRequested += HandleCameraRequested;
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			var cv = (CameraContentPage)this.Element;
			cv.CameraRequested -= HandleCameraRequested; 
		}

		void HandleCameraRequested (object sender, EventArgs e)
		{
			PresentCamera();
		}

		void PresentCamera ()
		{
			var picker = new MediaPicker();

			MediaPickerController controller = picker.GetTakePhotoUI (new StoreCameraMediaOptions 
			{
				Name = "scavengerhunt.jpg",
				Directory = "CouchbaseConnect"
			});

			PresentViewController (controller, true, null);

			controller.GetResultAsync().ContinueWith (t => 
			{
				// Dismiss the UI yourself
				controller.DismissViewController (true, () => 
						{
							try
							{
								if (t.Status != TaskStatus.Canceled) 
								{
									MediaFile file = t.Result;

									((CameraContentPage)this.Element).Captured = file.Path;
								}
							}
							catch(AggregateException aggX)
							{
								if(aggX.InnerException is TaskCanceledException)
								{
									//nothing
									Console.WriteLine("Task Cancelled");
								}
								else
								{
									throw;
								}
							}

						});

			}, TaskScheduler.FromCurrentSynchronizationContext());

			((CameraContentPage)this.Element).IsPresented = false;
		}
	}
}

