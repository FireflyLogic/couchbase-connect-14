using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CouchbaseConnect2014.Android;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.Controls;

[assembly: ExportRenderer (typeof (RateButton), typeof (RateButtonRenderer))]

namespace CouchbaseConnect2014.Android
{
	public class RateButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {  
				var nativeButton = (global::Android.Widget.Button) Control;

				var drawable = Resources.GetDrawable ("rateButton");
				nativeButton.SetBackgroundDrawable (drawable);
				nativeButton.RequestLayout ();
			}
		}
	}
}

