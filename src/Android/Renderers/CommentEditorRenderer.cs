using System;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.Android;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Graphics;

[assembly: ExportRenderer (typeof (CommentEditor), typeof (CommentEditorRenderer))]
namespace CouchbaseConnect2014.Android
{
	public class CommentEditorRenderer : EditorRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);

			var view = (CommentEditor)Element;

			SetFont (view);
			SetHint (view);
		}

		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var view = (CommentEditor)Element;

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Font")
				SetFont (view);

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Hint")
				SetHint (view);
		}

		// sets the font for the EditText
		void SetFont (CommentEditor view) {
			//FormattedString formattedString = base.Element.FormattedText ?? base.Element.Text;
			FormattedString formattedString = base.Element.Text;
			this.Control.TextFormatted = formattedString.ToAttributed (Font.Default, view.TextColor, this.Control);

			if (view.Font.FontFamily != null)
				this.Control.Typeface = Typeface.CreateFromAsset (Forms.Context.Assets,
					string.Format ("{0}.ttf", GetFontFamily(view.Font)));

			if (view.Font.FontSize > 0)
				this.Control.TextSize = (float)view.Font.FontSize;
		}

		static string GetFontFamily (Font font) {
			var family = font.FontFamily;

			if (family != null && family.IndexOf ("-") == -1)
				family += "-Regular";

			return family;
		}

		// sets the hint of the EditText
		void SetHint (CommentEditor view) {

			Control.Hint = view.Hint;
		}

	}
}

