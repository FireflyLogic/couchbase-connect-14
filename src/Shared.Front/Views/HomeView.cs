using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.Views
{
	public class HomeView : ContentPage
	{
		public HomeView ()
		{
            BaseViewModel.CreateAndBind<HomeViewModel> (this);

            Title = "Couchbase Connect";
			NavigationPage.SetBackButtonTitle (this, "");

			SetBinding (HomeView.NavigationProperty, new Binding ("Navigation"));

			var relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var currentSessionsCarousel = CreateCurrentSessionsCarousel ();
			relativeLayout.Children.Add (currentSessionsCarousel,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToParent ((parent) => { return parent.Y; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.Constant (175)
			);

			relativeLayout.Children.Add (CreateTweetList (),
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToView   (currentSessionsCarousel, (parent,sibling) => { return sibling.Height; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height - 175; })
			);

			relativeLayout.Children.Add (CreateActivityIndicator (),
				Constraint.RelativeToParent ((parent) => { return (parent.Width / 2) - 25; }),
				Constraint.RelativeToView   (currentSessionsCarousel, (parent,sibling) => { return (sibling.Height / 2) + (parent.Height / 2) - 25; }),
				Constraint.Constant(50),
				Constraint.Constant(50)
			);

			relativeLayout.Children.Add (CreateTimeOutView(),
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToView   (currentSessionsCarousel, (parent,sibling) => { return sibling.Height; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToView   (currentSessionsCarousel, (parent,sibling) => { return parent.Height - sibling.Height; })
			);

            relativeLayout.Children.Add (CreatePipsContainer (), 
                Constraint.Constant (0),
				Constraint.RelativeToView (currentSessionsCarousel, 
                    (parent,sibling) => { return sibling.Height - 18; }),
                Constraint.RelativeToParent (parent => parent.Width),
                Constraint.Constant (18)
            );

			Content = relativeLayout;
		}

		View CreateActivityIndicator ()
		{
			var activityIndicator = new ActivityIndicator
			{
				Color = Color.Black,
			};
			activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
			activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			return activityIndicator;
		}

		View CreateTimeOutView ()
		{
			var header = new FontLabel {
				Text = "Unfortunate",
				Font = Font.OfSize (Fonts.OpenSans, 16),
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			var timeout = new FontLabel {
				Text = "We're sorry. We were unable to retrieve any Tweets at this time.",
				Font = Font.OfSize(Fonts.OpenSansLight, 14),
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			var content = new StackLayout {

				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TranslationY = -20,
				Children = {
					header,
					timeout
				}
			};

			var timeOutView = new StackLayout {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(40, 0, 40, 0),
				BackgroundColor = Color.Default,
				Children = {
					content
				}
			};

			timeOutView.SetBinding(Label.IsVisibleProperty, "TimedOut");

			return timeOutView;
		}

        static ListView CreateTweetList ()
        {
            var tweetList = new ListView {
                RowHeight = 80,
                ItemTemplate = new DataTemplate (typeof(TwitterCell)),
                HasUnevenRows = true
            };
            tweetList.SetBinding (ListView.ItemsSourceProperty, "Tweets");
            return tweetList;
        }

        View CreateCurrentSessionsCarousel ()
        {
            var carousel = new CarouselLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(typeof(HomeCurrentSessionView))
            };
            carousel.SetBinding (CarouselLayout.ItemsSourceProperty, "CurrentSessions");
            carousel.SetBinding (CarouselLayout.SelectedItemProperty, "CurrentSession");

            return carousel;
        }

        View CreatePipsContainer ()
        {
            return new StackLayout {
                Children = { CreatePips () }
            };
        }

        View CreatePips () {
            var pipSet = new PipSet ();
            pipSet.SetBinding (PipSet.ItemsSourceProperty, "CurrentSessions");
            pipSet.SetBinding (PipSet.SelectedItemProperty, "CurrentSession");
            return pipSet;
        }
	}

	public class TwitterCell : ViewCell 
	{
		public TwitterCell() {

			RelativeLayout relativelayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			/* Twitter Image */
			var image = new Image ();
			image.SetBinding (Image.SourceProperty, new Binding ("Icon"));
			image.WidthRequest = image.HeightRequest = 50;

			relativelayout.Children.Add (image, 
				Constraint.Constant (15), 
				Constraint.RelativeToParent ((parent) => { return parent.Y + 15; })
			);
			/* * * * * * * * */

			/* Twitter Name Label */
			var twitterNameLabel = new FontLabel 
			{
				Font = Font.OfSize(Fonts.OpenSansBold, 12),
				YAlign = TextAlignment.End
			};
			twitterNameLabel.SetBinding (FontLabel.TextProperty, "Name");

			relativelayout.Children.Add (twitterNameLabel,
				Constraint.RelativeToView (image, (parent,sibling) => { return sibling.X + sibling.Width + 10; }),
				Constraint.RelativeToView (image, (parent,sibling) => { return sibling.Y; })
			);
			/* * * * * * * * * * * */

			/* Twitter Handle Label */
			var twitterHandleLabel = new FontLabel 
			{
				Font = Font.OfSize(Fonts.OpenSansLight, 10),
				YAlign = TextAlignment.End
			};
			twitterHandleLabel.SetBinding(FontLabel.TextProperty, "Twitter");

			relativelayout.Children.Add (twitterHandleLabel, 
				Constraint.RelativeToView (twitterNameLabel, (parent, sibling) => { return sibling.X + sibling.Width + 5;}),
				Constraint.RelativeToView (twitterNameLabel, (parent, sibling) => { return sibling.Y; }),
				null,
				Constraint.RelativeToView (twitterNameLabel, (parent,sibling) => { return sibling.Height; })
			);
			/* * * * * * * * * * * * */

			/* Time Label */
			var timeLabel = new FontLabel
			{
				Font = Font.OfSize(Fonts.OpenSansLight, 10),
				XAlign = TextAlignment.End,
				YAlign = TextAlignment.End,
			};
			timeLabel.SetBinding(FontLabel.TextProperty, "Time");

			relativelayout.Children.Add (timeLabel, 
				Constraint.RelativeToView (twitterHandleLabel, (parent, sibling) => { return sibling.X + sibling.Width; }),
				Constraint.RelativeToView (twitterHandleLabel, (parent, sibling) => { return sibling.Y; }),
				Constraint.RelativeToParent ((parent) => 
				{ 
					return parent.Width - twitterHandleLabel.Width - twitterNameLabel.Width - image.Width - 40; 
				}),
				Constraint.RelativeToView (twitterHandleLabel, (parent,sibling) => { return sibling.Height; })
			);
			/* * * * * * */

			/* Content Label */
			var contentLabel = new FontLabel 
			{
				Font = Font.OfSize(Fonts.OpenSans, 12)
			};
			contentLabel.SetBinding(FontLabel.TextProperty, "Content");

			Constraint contentHeightConstraint;
			if (Device.OS == TargetPlatform.iOS)
			{
				contentHeightConstraint = Constraint.RelativeToParent ((parent) => { return parent.Height - 20; });
			} else
			{
				contentHeightConstraint = null;
			}

			relativelayout.Children.Add(contentLabel, 
				Constraint.RelativeToView (image, (parent, sibling) => { return sibling.X + sibling.Width + 10; }),
				Constraint.RelativeToView (twitterNameLabel,(parent, sibling) => { return sibling.Y + sibling.Height; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width - image.Width - 35; }),
				contentHeightConstraint
			);
			/* * * * * * * * * */

			relativelayout.GestureRecognizers.Add (CreateTapGestureRecognizer ());

			View = relativelayout;
		}

		IGestureRecognizer CreateTapGestureRecognizer ()
		{
			var recognizer = new TapGestureRecognizer ();
			recognizer.SetBinding (TapGestureRecognizer.CommandProperty, "TweetTapped");
			return recognizer;
		}

		// this is to account for iOS not dynamically creating the size of each
		// cell in the listview
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			var session = (TweetCellViewModel)BindingContext;

			// rough translation of character-count to cell height
			// doesn't always work, but close enough for now
			if (Device.OS == TargetPlatform.iOS) 
			{
				if (session.Content.Length > 100)		// 4 lines of text
					this.Height = 110;
				else if (session.Content.Length > 81)	// 3 lines of text
					this.Height = 95;
				else
					this.Height = 80;					// 1-2 lines of text
			}
		}
	}
}

