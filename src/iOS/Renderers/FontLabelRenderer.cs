using System;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.ComponentModel;


[assembly: ExportRenderer (typeof(FontLabel), typeof(FontLabelRenderer))]
namespace CouchbaseConnect2014.iOS
{
	public class FontLabelRenderer : LabelRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			var view = (FontLabel)Element;

			SetUnderline (view);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (FontLabel)Element;

			if (string.IsNullOrEmpty (e.PropertyName) || e.PropertyName == "UnderlineProperty")
				SetUnderline(view);
		}

		void SetUnderline (FontLabel view) {
			if (view.Underline) {
				Control.AttributedText = new NSAttributedString (Control.Text, Control.Font, Control.TextColor,
					underlineStyle: NSUnderlineStyle.Single);
			}
		}
	}
}

