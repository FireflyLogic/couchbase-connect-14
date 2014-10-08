using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScavengerHuntCameraView), typeof(ScavengerHuntCameraViewRenderer))]

namespace CouchbaseConnect2014.Android
{
	public class ScavengerHuntCameraViewRenderer : PageRenderer
	{
		public ScavengerHuntCameraViewRenderer ()
		{
		}
	}
}

