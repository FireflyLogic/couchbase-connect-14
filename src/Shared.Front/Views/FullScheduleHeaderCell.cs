using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Views
{
	public class FullScheduleHeaderCell : ViewCell
	{
		public FullScheduleHeaderCell()
		{
			this.Height = 20;
			var title = new FontLabel {
				Font = Font.OfSize(Fonts.OpenSansLight, 12),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			title.SetBinding (Label.TextProperty, "StartTime", converter: new DateTimeValueConverter ("h:mm tt"));

			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HeightRequest = 20,
				BackgroundColor = Color.Default,
				Children = { title }
			};
		}
	}
}




