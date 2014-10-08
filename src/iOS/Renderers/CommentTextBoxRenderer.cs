using System;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CouchbaseConnect2014;
using CouchbaseConnect2014.iOS;
using System.Drawing;

[assembly: ExportRenderer (typeof (CommentTextBox), typeof (CommentTextBoxRenderer))]

namespace CouchbaseConnect2014.iOS
{
	public class CommentTextBoxRenderer: EditorRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement == null) {  
				var nativeTextView = (UITextView) Control;
				nativeTextView.Delegate = new TextViewDelegate ();

				nativeTextView.Font = UIFont.FromName (Fonts.OpenSans, 14);
				nativeTextView.Text = "Add Comment";
				nativeTextView.TextColor = UIColor.FromRGB(150, 150, 150);
				nativeTextView.AutocorrectionType = UITextAutocorrectionType.Yes;
				nativeTextView.AutocapitalizationType = UITextAutocapitalizationType.Sentences;
			}
		}
	}

	public class TextViewDelegate: UITextViewDelegate
	{
		public override void EditingStarted (UITextView textView)
		{
			if (textView.Text == "Add Comment") 
			{
				textView.Text = "";
				textView.TextColor = UIColor.Black;
			}

			textView.BecomeFirstResponder ();

			var parentScrollView = GetScrollView (textView);

			if (parentScrollView != null) 
			{
				var newOffset = new PointF (0, parentScrollView.ContentOffset.Y + 210);
				parentScrollView.SetContentOffset (newOffset, true);
			}
		}

		public override void EditingEnded (UITextView textView)
		{
			if (textView.Text == "") 
			{
				textView.Text = "Add Comment";
				textView.TextColor = UIColor.DarkGray;
			}

			textView.ResignFirstResponder ();
		}

		UIScrollView GetScrollView (UIView view)
		{
			if (view.Superview == null)
				return null;
			if (view.Superview is UIScrollView)
				return view.Superview as UIScrollView;

			return GetScrollView (view.Superview);
		}
	}
}

