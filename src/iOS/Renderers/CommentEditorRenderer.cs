using System;
using Xamarin.Forms.Platform.iOS;
using CouchbaseConnect2014.iOS;
using CouchbaseConnect2014.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using MonoTouch.UIKit;
using System.Drawing;

[assembly: ExportRenderer (typeof (CommentEditor), typeof (CommentEditorRenderer))]
namespace CouchbaseConnect2014.iOS
{
	public class CommentEditorRenderer : EditorRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);

			var view = (CommentEditor)Element;
			var textView = (UITextView)Control;

			textView.AutocapitalizationType = UITextAutocapitalizationType.Sentences;
			textView.AutocorrectionType = UITextAutocorrectionType.Default;

			// creates a delegate to handle things that should already be handled
			// by xamarin.forms editor
			Control.Delegate = new TextViewDelegate (ref view, view.Hint, view.TextColor.ToUIColor());

			SetHint (view);
			SetFont (view);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (CommentEditor)Element;

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Hint")
				SetHint(view);

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Font")
				SetFont(view);

			if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "TextColor")
				SetFont(view);

			if (string.IsNullOrEmpty (e.PropertyName) || e.PropertyName == "Text")
				SetTextColor (view);
		}

		private void SetHint(CommentEditor view) {

			view.Text = view.Hint;
			SetHintColor (view);
		}

		private void SetFont(CommentEditor view) {

			UIFont uiFont;
			if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
				Control.Font = uiFont;
			else if (view.Font == Font.Default)
				Control.Font = UIFont.SystemFontOfSize(17f);
		}

		// makes sure that the hint color is correct and
		// isn't the default color (black)
		private void SetHintColor (CommentEditor view) {
		
			Control.TextColor = UIColor.FromRGB (150, 150, 150);
		}

		private void SetTextColor (CommentEditor view)
		{
			if (view.Text == view.Hint)
				SetHintColor (view);
			else
				Control.TextColor = UIColor.Black;
		}

		public class TextViewDelegate : UITextViewDelegate
		{
			private string _hint;
			private UIColor _textColor;
			private CommentEditor editorView;

			public TextViewDelegate(ref CommentEditor editor, string hint, UIColor textColor) {
				_hint = hint;
				_textColor = textColor;
				editorView = editor;
			}

			public override void EditingStarted (UITextView textView)
			{
				if (textView.Text == _hint) 
				{
					textView.Text = "";
					textView.TextColor = _textColor;
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
					textView.Text = _hint;
					editorView.Text = _hint;
					textView.TextColor = UIColor.FromRGB (150, 150, 150);
				} else
				{
					editorView.Text = textView.Text;
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
}

