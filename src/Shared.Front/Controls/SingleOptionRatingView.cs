using System;
using Xamarin.Forms;
using System.Collections.Generic;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014;

namespace Shared.Front
{
	public class SingleOptionRatingView : StackLayout
	{
		List<RatingRadioButton> optionRatingList;
		string[] optionTextArray;

		public string RatingValue 
		{
			get { return (string)GetValue (RatingValueProperty); }
			set { SetValue (RatingValueProperty, value); }

		}

		public static readonly BindableProperty RatingValueProperty = 
			BindableProperty.Create<SingleOptionRatingView, string>(
				ratingView => ratingView.RatingValue,
				null
			);

		public SingleOptionRatingView ()
		{
			optionRatingList = new List<RatingRadioButton> ();
			optionTextArray = new string[]
			{
				"Too long",
				"Just right",
				"Too short"
			};

			/* Set up view */
			Spacing = 10;

			/* Add subviews: StackLayouts(horizontal) with radio button and text */
			for (int i = 1; i <= optionTextArray.Length; i++)
			{
				var option = CreateOption (i);
				var optionStack = CreateOptionStack (option, optionTextArray[i-1]);

				Children.Add (optionStack);
				optionRatingList.Add (option);
			}
		}

		public string GetRatingValue ()
		{
			return RatingValue;
		}

		RatingRadioButton CreateOption (int optionId)
		{
			var option = new RatingRadioButton (optionId) 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			return option;
		}

		StackLayout CreateOptionStack (RatingRadioButton option, string optionText)
		{
			var optionStack = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Spacing = 10
			};
			optionStack.Children.Add (option);
			optionStack.Children.Add(CreateOptionLabel(optionText));
			optionStack.GestureRecognizers.Add (CreateOptionTappedRecognizer ());

			return optionStack;
		}

		FontLabel CreateOptionLabel (string optionText)
		{
			var optionLabel = new FontLabel
			{
				Text = optionText,
				TextColor = Color.FromHex ("333333"),
				Font = Font.OfSize(Fonts.OpenSansLight, 18),
				HorizontalOptions = LayoutOptions.EndAndExpand,
				YAlign = TextAlignment.Center
			};

			return optionLabel;
		}

		TapGestureRecognizer CreateOptionTappedRecognizer ()
		{
			var optionTapEventHandler = new EventHandler (delegate(object sender, EventArgs e) {
				var optionStackTapped = (StackLayout)sender;
				var optionTapped = (RatingRadioButton)optionStackTapped.Children[0];
				UpdateRating (optionTapped.GetOptionId());
			});

			var optionTap = new TapGestureRecognizer ();
			optionTap.NumberOfTapsRequired = 1;
			optionTap.Tapped += optionTapEventHandler;

			return optionTap;
		}

		void UpdateRating (int option)
		{
			RatingValue = optionTextArray [option - 1];

			// loop through all of the options, making sure only chosen one is highlighted
			for (int i = 0; i < optionRatingList.Count; i++) 
				optionRatingList [i].TurnRadioButtonOff ();

			optionRatingList [option - 1].TurnRadioButtonOn ();
		}
	}
}

