using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CouchbaseConnect2014.Android.Renderers;
using Android.Widget;
using Android.Graphics;
using CouchbaseConnect2014.Controls;
using System.ComponentModel;

[assembly: ExportRenderer (typeof(FontLabel), typeof(FontLabelRenderer))]
namespace CouchbaseConnect2014.Android.Renderers
{
    public class FontLabelRenderer : LabelRenderer
    {
        TextView view;

        protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged (e);
            this.view = (TextView)Control;

			var label = (FontLabel)Element;

            UpdateText ();
			UnderlineText (label);
        }

        protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged (sender, e);

			var label = (FontLabel)Element;

			if (string.IsNullOrEmpty (e.PropertyName) || e.PropertyName == "UnderlineProperty") {
				this.UnderlineText (label);
				return;
			}

            if (e.PropertyName == Label.TextColorProperty.PropertyName) {
                this.UpdateText ();
                return;
            }
            if (e.PropertyName == Label.FontProperty.PropertyName) {
                this.UpdateText ();
                return;
            }
            if (e.PropertyName == Label.TextProperty.PropertyName) {
                this.UpdateText ();
				return;
            }
        }

        void UpdateText ()
        {
            FormattedString formattedString = base.Element.FormattedText ?? base.Element.Text;
            this.view.TextFormatted = formattedString.ToAttributed (Font.Default, base.Element.TextColor, this.view);

            if (Element.Font.FontFamily != null)
                this.view.Typeface = Typeface.CreateFromAsset (Forms.Context.Assets,
                    string.Format ("{0}.ttf", GetFontFamily(Element.Font)));

            if (Element.Font.FontSize > 0)
                this.view.TextSize = (float)Element.Font.FontSize;
        }

        static string GetFontFamily (Font font) {
            var family = font.FontFamily;

            if (family != null && family.IndexOf ("-") == -1)
                family += "-Regular";

            return family;
        }

		void UnderlineText (FontLabel label) {
			if(label.Underline)
				Control.PaintFlags = Control.PaintFlags | PaintFlags.UnderlineText;
		}
    }
}

