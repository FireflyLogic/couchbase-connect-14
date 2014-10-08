using System;
using Xamarin.Forms;
using CouchbaseConnect2014;
using System.Collections.Generic;

namespace Shared.Front
{
	public class FiveStarRatingView : StackLayout
	{
		List<RatingStar> starRatingList;

		public int RatingValue 
		{
			get { return (int)GetValue (RatingValueProperty); }
			set { SetValue (RatingValueProperty, value); }

		}

		public static readonly BindableProperty RatingValueProperty = 
			BindableProperty.Create<FiveStarRatingView, int>(
				ratingView => ratingView.RatingValue,
				0,
				propertyChanged: (bindable, oldValue, newValue) => 
				{
					((FiveStarRatingView)bindable).UpdateRating(newValue);
				}
			);

		public FiveStarRatingView ()
		{
			starRatingList = new List<RatingStar> ();

			/* Set up view */
			Orientation = StackOrientation.Horizontal;
			HorizontalOptions = LayoutOptions.CenterAndExpand;
			Spacing = 10;

			/* Add subviews: stars */
			for (int i = 1; i <= 5; i++)
			{
				var star = CreateStar (i);
				Children.Add (star);
				starRatingList.Add (star);
			}
		}

		public int GetRatingValue ()
		{
			return RatingValue;
		}

		RatingStar CreateStar (int starnumber)
		{
			var star = new RatingStar (starnumber) 
			{
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			star.GestureRecognizers.Add (CreateStarTappedRecognizer ());

			return star;
		}

		TapGestureRecognizer CreateStarTappedRecognizer ()
		{
			var contentTapEventHandler = new EventHandler (delegate(object sender, EventArgs e) {
				var starTapped = (RatingStar)sender;
				UpdateRating (starTapped.GetStarNum());
			});

			var starTap = new TapGestureRecognizer ();
			starTap.NumberOfTapsRequired = 1;
			starTap.Tapped += contentTapEventHandler;

			return starTap;
		}

		void UpdateRating (int starNumTapped)
		{
			RatingValue = starNumTapped;

			// loop through all of the stars and turn on/off the correct ones
			for (int i = 0; i < starRatingList.Count; i++) 
			{
				if (i + 1 <= starNumTapped) {
					starRatingList [i].TurnStarOn ();
				} else {
					starRatingList [i].TurnStarOff ();
				}
			}
		}
	}
}

