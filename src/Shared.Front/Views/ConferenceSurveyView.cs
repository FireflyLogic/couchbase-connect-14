using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ViewModels;
using Shared.Front;
using CouchbaseConnect2014.ValueConverters;

namespace CouchbaseConnect2014.Views
{
	public class ConferenceSurveyView: ContentPage
	{
		/* Alert view bindable property */
		public string Alert 
		{ 
			get { return (string)GetValue (AlertProperty); }
			set { SetValue (AlertProperty, value); }
		}

		public static readonly BindableProperty AlertProperty = BindableProperty.Create<ConferenceSurveyView, string>(
			b => b.Alert, 
			null, 
			propertyChanged: async (bindable, oldValue, newValue) => 
			{
				if(newValue == null) return;

				var view = (ConferenceSurveyView)bindable;
				await view.DisplayAlert ("", newValue, "Ok");
				((ConferenceSurveyViewModel)view.BindingContext).AlertMessage = null;
			}
		);
		/* * * * * * * * * * * * * * * * */

		/* Survey exists bindable property */
		public bool SurveyExists
		{
			get { return (bool)GetValue (SurveyExistsProperty); }
			set { SetValue (SurveyExistsProperty, value); }
		}

		public static readonly BindableProperty SurveyExistsProperty = BindableProperty.Create<ConferenceSurveyView, bool>(
			b => b.SurveyExists, 
			true,
			BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) => 
			{
				var view = (ConferenceSurveyView)bindable;

				// BOOL: newValue = SurveyExists
				if(!newValue)
					view.ToolbarItems.Add (view.CreateSubmitButton ());
				else
					view.ToolbarItems.Clear();
			}
		);
		/* * * * * * * * * * * * * * * * * */

		/* * * * * * Constructor * * * * * * */
		public ConferenceSurveyView ()
		{
            BaseViewModel.CreateAndBind<ConferenceSurveyViewModel> (this);

			Title = "Conference Survey";

			var surveyCompletedView = CreateSurveyCompletedView ();
			surveyCompletedView.SetBinding (StackLayout.IsVisibleProperty, "SurveyExists");

			var surveyIncompleteView = CreateSurveyIncompleteView ();
			surveyIncompleteView.SetBinding (View.IsVisibleProperty, "SurveyExists", converter: new NegateValueConverter ());

			SetBinding (ConferenceSurveyView.SurveyExistsProperty, new Binding("SurveyExists"));

			SetBinding (AlertProperty, new Binding("AlertMessage"));

			MessagingCenter.Subscribe<ConferenceSurveyViewModel> (this, "DismissKeyboard", delegate
			{

			});

			Content = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = 
				{
					surveyCompletedView,
					surveyIncompleteView
				}
			};
		}
		/* * * * * * * * * * * * * * * * * * */
			
		/* * * * * * * * * * * * * SURVEY COMPLETED * * * * * * * * * * * * */
		StackLayout CreateSurveyCompletedView ()
		{
			return new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Spacing = 20,
				Padding = new Thickness (10, 30, 10, 10),
				Children = 
				{
					CreateLabelStack(),
					CreateTshirtImage()
				}
			};
		}

		StackLayout CreateLabelStack ()
		{
			var labelStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 0
			};
			labelStack.Children.Add (CreateScoreLabel ());
			labelStack.Children.Add (CreateHowToLabel ());

			return labelStack;
		}

		FontLabel CreateScoreLabel ()
		{
			var scoreLabel = new FontLabel 
			{
				Text = "Score!",
				Font = Font.OfSize(Fonts.OpenSans, 18),
				TextColor = Color.Black,
				XAlign = TextAlignment.Center
			};

			return scoreLabel;
		}

		FontLabel CreateHowToLabel ()
		{
			var howToLabel = new FontLabel 
			{
				Text = "Claim your free t-shirt \nat the registration desk",
				Font = Font.OfSize(Fonts.OpenSansLight, 18),
				TextColor = Color.FromHex ("333333"),
				XAlign = TextAlignment.Center
			};

			return howToLabel;
		}

		Image CreateTshirtImage ()
		{
			var tshirtImage = new Image 
			{
				Source = "surveyShirt.png",
			};

			return tshirtImage;
		}
		/* * * * * * * * * * * * * * * * * * * * * * * ** * * * * * * * * * */

		/* * * * * * * * * * * * SURVEY NOT COMPLETED * * * * * * * * * * * */
		View CreateSurveyIncompleteView ()
		{
			return new ScrollView 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Content = new StackLayout 
				{
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					VerticalOptions = LayoutOptions.StartAndExpand,
					Padding = new Thickness (30, 30, 30, 50),
					Spacing = 20,
					Children = {
						CreateLabel ("What is your overall evaluation of \nCouchbase Connect 2014?"),
						CreateRatingStarsView (),
						CreateLineLabelStack ("The length of the conference was:"),
						CreateLengthOptionsView (),
						CreateLineLabelStack ("Which track(s) did you find most valuable? \nSelect one or more"),
						CreateValuableTrackOptionsView (),
						CreateLineLabelStack ("Any other suggestions?"),
						CreateCommentBox (),
						CreateLineView ()
					}
				}
			};
		}

		ToolbarItem CreateSubmitButton ()
		{
			var submitButton = new ToolbarItem { Name = "Submit" };
			submitButton.SetBinding (ToolbarItem.CommandProperty, "SubmitSurvey");

			return submitButton;
		}

		FontLabel CreateLabel(string message)
		{
			var label = new FontLabel 
			{
				Text = message,
				TextColor = Color.FromHex ("26ade6"),
				Font = Font.OfSize(Fonts.OpenSansBold, 12),
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			return label;
		}

		View CreateRatingStarsView ()
		{
			var ratingStarsView = new FiveStarRatingView 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			ratingStarsView.SetBinding (
				FiveStarRatingView.RatingValueProperty, 
				"ConferenceEvaluationRating", 
				BindingMode.OneWayToSource
			);

			return ratingStarsView;
		}

		StackLayout CreateLineLabelStack (string text)
		{
			var lineLabelStack = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Spacing = 10
			};
			lineLabelStack.Children.Add (CreateLineView ());
			lineLabelStack.Children.Add (CreateLabel (text));

			return lineLabelStack;
		}

		BoxView CreateLineView ()
		{
			var lineWidth = 275;

			if (Device.OS == TargetPlatform.Android) 
				lineWidth = 325;

			var line = new BoxView 
			{
				WidthRequest = lineWidth,
				HeightRequest = 1.0,
				Color = Color.FromHex ("c8c7cc"),
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			return line;
		}

		View CreateLengthOptionsView ()
		{
			var lengthOptionsView = new SingleOptionRatingView 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			lengthOptionsView.SetBinding (
				SingleOptionRatingView.RatingValueProperty, 
				"ConferenceLengthRating", 
				BindingMode.OneWayToSource
			);

			return lengthOptionsView;
		}

		View CreateValuableTrackOptionsView ()
		{
			var valuableTrackOptionsView = new MultipleOptionRatingView 
			{
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			valuableTrackOptionsView.SetBinding (
				MultipleOptionRatingView.RatingValuesProperty, 
				"MostValuableTracks",
				BindingMode.OneWayToSource
			);

			return valuableTrackOptionsView;
		}

		View CreateCommentBox ()
		{
			var commentBox = new CommentEditor 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Hint = "Add Comment",
				WidthRequest = 275,
				HeightRequest = 115
			};

			commentBox.SetBinding (
				CommentEditor.TextProperty, 
				"ConferenceSurveyComments", 
				BindingMode.OneWayToSource
			);

			return commentBox;
		}
	}
}

