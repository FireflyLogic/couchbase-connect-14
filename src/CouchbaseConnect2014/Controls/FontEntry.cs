using System;
using Xamarin.Forms;

namespace CouchbaseConnect2014.Controls
{
	public class FontEntry : Entry
	{
		public static readonly BindableProperty FontProperty =
			BindableProperty.Create("Font", typeof(Font), typeof(FontEntry), new Font());

		public static readonly BindableProperty XAlignProperty =
			BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(FontEntry),
				TextAlignment.Start);

//		public static readonly BindableProperty HasBorderProperty =
//			BindableProperty.Create("HasBorder", typeof(bool), typeof(FontEntry), true);

		public Font Font
		{
			get { return (Font)GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		public TextAlignment XAlign
		{
			get { return (TextAlignment)GetValue(XAlignProperty); }
			set { SetValue(XAlignProperty, value); }
		}

//		public bool HasBorder
//		{
//			get { return (bool)GetValue(HasBorderProperty); }
//			set { SetValue(HasBorderProperty, value); }
//		}
	}
}

