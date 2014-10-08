using System;
using Xamarin.Forms;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Controls;
using Shared.Front;

namespace CouchbaseConnect2014.Views
{
	public class RateSessionView : ContentPage
	{
		/* Alert view bindable property */
		public string Alert 
		{ 
			get { return (string)GetValue (AlertProperty); }
			set { SetValue (AlertProperty, value); }
		}

		public static readonly BindableProperty AlertProperty = BindableProperty.Create<RateSessionView, string>(
			b => b.Alert, 
			null, 
			propertyChanged: async (bindable, oldValue, newValue) => 
		{
			if(newValue == null) return;

			var view = (RateSessionView)bindable;
			await view.DisplayAlert ("", newValue, "Ok");
			((RateSessionViewModel)view.BindingContext).AlertMessage = null;
		}
		);
		/* * * * * * * * * * * * * * * * */

		public RateSessionView (string sessionId)
		{
			BaseViewModel.CreateAndBind<RateSessionViewModel> (this, sessionId);

			Title = "Rate Session";
			NavigationPage.SetBackButtonTitle (this, "");

            SetBinding (NavigationProperty, new Binding ("Navigation"));

			var doneButton = new ToolbarItem { Name = "Done" };
			doneButton.SetBinding (ToolbarItem.CommandProperty, "SaveRating");
			this.ToolbarItems.Add(doneButton);

			SetBinding (AlertProperty, new Binding("AlertMessage"));

			Content = CreateStack ();
		}

		View CreateStack() {

			var stack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20, 40, 20, 20),
				Spacing = 40,
				Children = {
					CreateContentRating(),
					CreateSpeakerRating(),
					CreateComment()
				}
			};

			// to make fields move up as keyboard comes up
			var scrollview = new ScrollView {
				Orientation = ScrollOrientation.Vertical,
				Content = stack
			};

			return scrollview;
		}

		View CreateContentRating() {

			var title = new FontLabel {
				Text = "Content",
				Font = Font.OfSize(Fonts.OpenSans, 18)
			};

			var content = new FiveStarRatingView 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			content.SetBinding (
				FiveStarRatingView.RatingValueProperty, 
				"ContentRating", 
				BindingMode.TwoWay
			);

			var view = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					title,
					content
				}
			};

			return view;
		}

		View CreateSpeakerRating() {

			var title = new FontLabel {
				Text = "Speaker",
				Font = Font.OfSize(Fonts.OpenSans, 18)
			};

			var content = new FiveStarRatingView 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			content.SetBinding (
				FiveStarRatingView.RatingValueProperty, 
				"SpeakerRating", 
				BindingMode.TwoWay
			);

			var view = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				Children = {
					title,
					content
				}
			};

			return view;
		}

		View CreateComment() {
		
			var comment = new CommentEditor {
				VerticalOptions = LayoutOptions.EndAndExpand,
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				Hint = "Add Comment",
				HeightRequest = 115
			};

			comment.SetBinding (
				CommentEditor.TextProperty, 
				"Comments", 
				BindingMode.TwoWay
			);

			var view = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = {
					CreateDivider(),
					comment,
					CreateDivider()
				}
			};

			return view;
		}

		View CreateDivider ()
		{
			return new BoxView 
			{
				Color = Color.FromHex("cccccc"),
				HeightRequest = 1
			};
		}
	}
}

