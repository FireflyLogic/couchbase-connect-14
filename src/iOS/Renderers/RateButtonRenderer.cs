using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.iOS;
using MonoTouch.UIKit;
using System.Drawing;
using CouchbaseConnect2014.Controls;

[assembly: ExportRenderer (typeof (RateButton), typeof (RateButtonRenderer))]

namespace CouchbaseConnect2014.iOS
{
	public class RateButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {  
				var nativeButton = (UIButton) Control;

				nativeButton = UIButton.FromType (UIButtonType.RoundedRect);
			}
		}
	}
}

