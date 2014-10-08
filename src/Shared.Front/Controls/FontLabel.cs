using System;
using Xamarin.Forms;

namespace CouchbaseConnect2014.Controls
{
    public class FontLabel : Label
    {

		public static readonly BindableProperty UnderlineProperty =
			BindableProperty.Create("Underline", typeof(bool), typeof(FontLabel), false);

		public bool Underline
		{
			get { return (bool)GetValue(UnderlineProperty); }
			set { SetValue(UnderlineProperty, value); }
		}

    }
}

