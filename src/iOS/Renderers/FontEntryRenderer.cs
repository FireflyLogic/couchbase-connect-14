using System;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.iOS;
using System.Drawing;
using System.ComponentModel;

[assembly: ExportRenderer (typeof (FontEntry), typeof (FontEntryRenderer))]
namespace CouchbaseConnect2014.iOS
{
	public class FontEntryRenderer : EntryRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			var view = (FontEntry)Element;

			Control.AutocapitalizationType = UITextAutocapitalizationType.Words;
			Control.AutocorrectionType = UITextAutocorrectionType.Default;

			SetFont(view);
			SetTextAlignment(view);
//			SetBorder(view);

			ResizeHeight();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (FontEntry)Element;

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Font")
				SetFont(view);

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "XAlign")
				SetTextAlignment(view);
				
//			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "HasBorder")
//				SetBorder(view);

			ResizeHeight();
		}

//		private void SetBorder(FontEntry view)
//		{
//			Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
//		}

		private void SetTextAlignment(FontEntry view)
		{
			switch (view.XAlign)
			{
			case TextAlignment.Center:
				Control.TextAlignment = UITextAlignment.Center;
				break;
			case TextAlignment.End:
				Control.TextAlignment = UITextAlignment.Right;
				break;
			case TextAlignment.Start:
				Control.TextAlignment = UITextAlignment.Left;
				break;
			}
		}

		private void SetFont(FontEntry view)
		{
			UIFont uiFont;
			if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
				Control.Font = uiFont;
			else if (view.Font == Font.Default)
				Control.Font = UIFont.SystemFontOfSize(17f);
		}

		private void ResizeHeight()
		{
			if (Element.HeightRequest >= 0) return;

			var height = Math.Max(Bounds.Height,
				new UITextField {Font = Control.Font}.IntrinsicContentSize.Height);

			Control.Frame = new RectangleF(0.0f, 0.0f, (float) Element.Width, height);

			Element.HeightRequest = height;
		}
	}
}

