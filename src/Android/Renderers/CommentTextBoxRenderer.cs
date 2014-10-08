using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CouchbaseConnect2014;
using CouchbaseConnect2014.Android;
using Android.Graphics;

[assembly: ExportRenderer (typeof (CommentTextBox), typeof (CommentTextBoxRenderer))]

namespace CouchbaseConnect2014.Android
{
	public class CommentTextBoxRenderer: EditorRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement == null) {  
				var nativeEditText = (global::Android.Widget.EditText) Control;

				nativeEditText.Hint = "Add Comment";
				nativeEditText.Typeface = Typeface.CreateFromAsset (Forms.Context.Assets, "OpenSans-Regular.ttf");
				nativeEditText.TextSize = 14f;
				nativeEditText.SetHintTextColor (global::Android.Graphics.Color.Rgb (150, 150, 150));
				nativeEditText.SetBackgroundColor(global::Android.Graphics.Color.Rgb (252, 252, 252));
			}
		}
	}
}

