using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Android.Text;
using Android.Text.Style;
using TinyIoC;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.Android
{
	[Activity (Label = "Connect 2014",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, 
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			// removes the default launcher icon from the actionbar
			ActionBar.SetIcon (null);

			TinyIoCContainer.Current.Register<IImageResizerService> (new ImageResizerService ());

            App.Initialize ();
			SetPage (App.GetMainPage ());
		}
	}
}

