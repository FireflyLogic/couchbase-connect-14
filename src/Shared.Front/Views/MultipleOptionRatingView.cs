using System;
using Xamarin.Forms;
using System.Collections.Generic;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014;

namespace Shared.Front
{
	public class MultipleOptionRatingView : StackLayout
	{
		List<RatingCheckbox> optionRatingList;
		string[] optionTextArray;

		public List<string> RatingValues 
		{
			get { return (List<string>)GetValue (RatingValuesProperty); }
			set { 
				SetValue (RatingValuesProperty, value); 
			}

		}

		public static readonly BindableProperty RatingValuesProperty = 
			BindableProperty.Create<MultipleOptionRatingView, List<string>>(
				ratingView => ratingView.RatingValues,
				new List<string> ()
			);

		public MultipleOptionRatingView ()
		{
			optionRatingList = new List<RatingCheckbox> ();
			optionTextArray = new string[]
			{
				"Keynotes",
				"Mobile",
				"Enterprise",
				"Developer",
				"Operations",
				"Architecture",
				"Customer",
				"All",
				"None"
			};

			/* Initial setup of RatingValues */
			RatingValues = new List<string> ();

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

		public List<string> GetRatingValues ()
		{
			return RatingValues;
		}

		RatingCheckbox CreateOption (int optionId)
		{
			var option = new RatingCheckbox (optionId) 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			return option;
		}

		StackLayout CreateOptionStack (RatingCheckbox option, string optionText)
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
				var optionTapped = (RatingCheckbox)optionStackTapped.Children[0];
				UpdateRating (optionTapped.GetOptionId());
			});

			var optionTap = new TapGestureRecognizer ();
			optionTap.NumberOfTapsRequired = 1;
			optionTap.Tapped += optionTapEventHandler;

			return optionTap;
		}

		void UpdateRating (int optionId)
		{
			RatingCheckbox checkBox = optionRatingList [optionId - 1];

			if (checkBox.IsChecked())
			{
				checkBox.TurnCheckboxOff ();
				RatingValues.Remove (optionTextArray [optionId - 1]);
			} 
			else
			{
				checkBox.TurnCheckboxOn ();
				RatingValues.Add (optionTextArray [optionId - 1]);
			}
		}
	}
}
