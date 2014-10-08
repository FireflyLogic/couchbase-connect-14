using System;
using Xamarin.Forms;

namespace CouchbaseConnect2014.Controls
{
	public class CommentEditor : Editor
	{

		public static readonly BindableProperty FontProperty =
			BindableProperty.Create("Font", typeof(Font), typeof(CommentEditor), new Font());

		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create("TextColor", typeof(Color), typeof(CommentEditor), Color.Black);

		public static readonly BindableProperty HintProperty =
			BindableProperty.Create("Hint", typeof(string), typeof(CommentEditor), "");

		public Font Font
		{
			get { return (Font)GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		public Color TextColor
		{
			get { return (Color)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		public string Hint
		{
			get { return (string)GetValue(HintProperty); }
			set { SetValue(HintProperty, value); }
		}
	}
}

