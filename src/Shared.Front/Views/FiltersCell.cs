using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class FiltersCell : ViewCell
	{
		public FiltersCell ()
		{
			View = CreateFilterCellView ();
		}

		RelativeLayout CreateFilterCellView ()
		{
			var filterCellView = CreateRelativeLayout ();
			var trackLabel = CreateTrackLabel ();
			var selectionIndicator = CreateSelectionIndicator ();

			filterCellView.Children.Add (trackLabel,
				Constraint.RelativeToParent (parent => { return parent.X + 15; }),
				Constraint.RelativeToParent (parent => { return parent.Y; }),
				Constraint.RelativeToParent (parent => { return parent.Width * 0.87; }),
				Constraint.RelativeToParent (parent => { return parent.Height; })
			);
			filterCellView.Children.Add (selectionIndicator,
				Constraint.RelativeToView (trackLabel, (parent,sibling) => { return sibling.Width; }),
				Constraint.RelativeToParent (parent => { return parent.Y + 23; }),
				null,
				Constraint.RelativeToParent (parent => { return parent.Height; })
			);

            filterCellView.GestureRecognizers.Add (CreateTapGestureRecognizer ());

			return filterCellView;
		}

        IGestureRecognizer CreateTapGestureRecognizer ()
        {
            var tapGestureRecognizer = new TapGestureRecognizer ();
            tapGestureRecognizer.SetBinding (TapGestureRecognizer.CommandProperty, "ToggleSelection");
            return tapGestureRecognizer;
        }

		RelativeLayout CreateRelativeLayout ()
		{
			var relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			relativeLayout.SetBinding (RelativeLayout.BackgroundColorProperty, "Track", converter: new TrackBackgroundColorConverter ());

			return relativeLayout;}

		View CreateTrackLabel ()
		{
			var trackLabel = new FontLabel 
			{
				Font = Font.OfSize(Fonts.OpenSansLight, 18),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				YAlign = TextAlignment.Center
			};
			trackLabel.SetBinding (FontLabel.TextProperty, "Track");
			trackLabel.SetBinding (FontLabel.TextColorProperty, "Track", converter: new TrackTextColorConverter ());

			return trackLabel;
		}

		View CreateSelectionIndicator ()
		{
			var selectionIndicator = new FontLabel 
			{
                Font = Font.OfSize (Fonts.OpenSansLight, 24),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				WidthRequest = 25,
				HeightRequest = 25
			};

            selectionIndicator.SetBinding (FontLabel.TextProperty, "IsSelected", 
                converter: new BooleanToTextValueConverter ("✓", "○"));

            selectionIndicator.SetBinding (FontLabel.TextColorProperty, "Track", 
                converter: new TrackTextColorConverter ());

			return selectionIndicator;
		}
	}

}

