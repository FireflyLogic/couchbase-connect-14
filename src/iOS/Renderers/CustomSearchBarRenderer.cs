using System;
using Xamarin.Forms;
using CouchbaseConnect2014;
using CouchbaseConnect2014.iOS;
using CouchbaseConnect2014.Views;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

[assembly: ExportRenderer (typeof (CustomSearchBar), typeof (CustomSearchBarRenderer))]
namespace CouchbaseConnect2014.iOS
{
	public class CustomSearchBarRenderer : SearchBarRenderer
	{
		CancellationTokenSource _tokensource = new CancellationTokenSource();

		protected override void OnElementChanged (ElementChangedEventArgs<SearchBar> e)
		{
			base.OnElementChanged (e);

			// BEN MADE ME!
			Control.TextChanged += async (object sender, UISearchBarTextChangedEventArgs ex) => {
				if(ex.SearchText == string.Empty) {
					try {
						_tokensource = new CancellationTokenSource();
						await Task.Delay(1200, _tokensource.Token);
						Control.ResignFirstResponder();
					} catch (TaskCanceledException exc) {
						Console.WriteLine(exc.Message);
					}
				} else {
					if(_tokensource.Token.CanBeCanceled)
						_tokensource.Cancel();
				}
			};
		}

		// There might be a better place for this, but I don't know where it is
		public override void Draw(RectangleF rect)
		{
			var csb = (CustomSearchBar) Element;
			if (csb.BarTint != null)
				Control.BarTintColor = csb.BarTint.GetValueOrDefault().ToUIColor();

			Control.SearchBarStyle = UISearchBarStyle.Prominent;
			Control.ShowsCancelButton = false;

			// crashes iOS devices under 7.1?
			// need to search through all views of searchbar and set correct view
			// to have these properties
//			Control.ReturnKeyType = UIReturnKeyType.Default;
//			Control.EnablesReturnKeyAutomatically = false;

//			Control.KeyboardType = UIKeyboardType.Default;
//			var frame = Control.Frame;
//			var backView = new UIView(frame);
//			backView.BackgroundColor = UIColor.White;
//			Control.add

			base.Draw(rect);
		}
	}
}

