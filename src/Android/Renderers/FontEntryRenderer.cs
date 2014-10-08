using System;
using Xamarin.Forms.Platform.Android;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.Android;
using Xamarin.Forms;
using Android.Widget;
using System.ComponentModel;
using Android.Graphics;


[assembly: ExportRenderer (typeof(FontEntry), typeof(FontEntryRenderer))]
namespace CouchbaseConnect2014.Android
{
	public class FontEntryRenderer : EntryRenderer
	{
		EditText view;

		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			this.view = (EditText)Control;

			Control.InputType = global::Android.Text.InputTypes.TextFlagCapWords;

			var mEntry = (FontEntry)Element;

			UpdateText (mEntry);
		}

		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var mEntry = (FontEntry)Element;

//			if (e.PropertyName == Label.TextColorProperty.PropertyName) {
//				this.UpdateText ();
//				return;
//			}
			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Font") {
				this.UpdateText (mEntry);
			}
//			if (e.PropertyName == Label.TextProperty.PropertyName) {
//				this.UpdateText ();
//			}
		}

		void UpdateText (FontEntry mEntry)
		{
			//FormattedString formattedString = base.Element.FormattedText ?? base.Element.Text;
			FormattedString formattedString = base.Element.Text;
			this.view.TextFormatted = formattedString.ToAttributed (Font.Default, base.Element.TextColor, this.view);

			if (mEntry.Font.FontFamily != null)
				this.view.Typeface = Typeface.CreateFromAsset (Forms.Context.Assets,
					string.Format ("{0}.ttf", GetFontFamily(mEntry.Font)));

			if (mEntry.Font.FontSize > 0)
				this.view.TextSize = (float)mEntry.Font.FontSize;
		}

		static string GetFontFamily (Font font) {
			var family = font.FontFamily;

			if (family != null && family.IndexOf ("-") == -1)
				family += "-Regular";

			return family;
		}
	}
}

