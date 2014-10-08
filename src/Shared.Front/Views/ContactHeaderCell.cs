using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;

namespace CouchbaseConnect2014.Views
{
	public class ContactHeaderCell : ViewCell
	{
		public ContactHeaderCell()
		{
			this.Height = 25;
			var title = new FontLabel {
                Font = Font.OfSize(Fonts.OpenSansBold, 17),
				VerticalOptions = LayoutOptions.Center
			};

			title.SetBinding(Label.TextProperty, "Key");

			View = new StackLayout {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 25,
				BackgroundColor = Color.Default,
				Padding = new Thickness(5, 0, 0 , 0),
				Orientation = StackOrientation.Horizontal,
				Children = { title }
			};
		}
	}
}


