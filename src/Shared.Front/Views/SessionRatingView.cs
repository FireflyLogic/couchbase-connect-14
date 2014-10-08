using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Shared.Front
{
	/* * Survey Rating View as a Content Page * */
	public class SessionRatingView: ContentPage
	{
		List<RatingStar> contentRatingList;
		List<RatingStar> speakerRatingList;

		public SessionRatingView ()
		{
			Title = "Rate Session";

			/* Content Rating */
			contentRatingList = new List<RatingStar> ();

			var contentTapEventHandler = new EventHandler (delegate(object sender, EventArgs e) {
				var starTapped = (RatingStar)sender;
				UpdateRating (contentRatingList, starTapped.GetStarNum());
			});

			var contentTap = new TapGestureRecognizer ();
			contentTap.NumberOfTapsRequired = 1;
			contentTap.Tapped += contentTapEventHandler;

			var oneStarContent = new RatingStar (1) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			oneStarContent.GestureRecognizers.Add (contentTap);

			var twoStarContent = new RatingStar (2) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			twoStarContent.GestureRecognizers.Add (contentTap);

			var threeStarContent = new RatingStar (3) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			threeStarContent.GestureRecognizers.Add (contentTap);

			var fourStarContent = new RatingStar (4) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			fourStarContent.GestureRecognizers.Add (contentTap);

			var fiveStarContent = new RatingStar (5) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			fiveStarContent.GestureRecognizers.Add (contentTap);

			contentRatingList.Add (oneStarContent);
			contentRatingList.Add (twoStarContent);
			contentRatingList.Add (threeStarContent);
			contentRatingList.Add (fourStarContent);
			contentRatingList.Add (fiveStarContent);

			var contentButtonLayout = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 5,
				Children = 
				{
					oneStarContent,
					twoStarContent,
					threeStarContent,
					fourStarContent,
					fiveStarContent
				}
			};
			/* - * - * - * - * - * - * - * - * - * - * - * - * - */
			/* - * - * - * - * - * - * - * - * - * - * - * - * - */

			/* Speaker Rating */
			speakerRatingList = new List<RatingStar> ();

			var speakerTapEventHandler = new EventHandler (delegate(object sender, EventArgs e) {
				var starTapped = (RatingStar)sender;
				UpdateRating (speakerRatingList, starTapped.GetStarNum());
			});

			var speakerTap = new TapGestureRecognizer ();
			speakerTap.NumberOfTapsRequired = 1;
			speakerTap.Tapped += speakerTapEventHandler;



			var oneStarSpeaker = new RatingStar (1) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			oneStarSpeaker.GestureRecognizers.Add (speakerTap);

			var twoStarSpeaker = new RatingStar (2) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			twoStarSpeaker.GestureRecognizers.Add (speakerTap);

			var threeStarSpeaker = new RatingStar (3) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			threeStarSpeaker.GestureRecognizers.Add (speakerTap);

			var fourStarSpeaker = new RatingStar (4) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			fourStarSpeaker.GestureRecognizers.Add (speakerTap);

			var fiveStarSpeaker = new RatingStar (5) 
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			fiveStarSpeaker.GestureRecognizers.Add (speakerTap);

			speakerRatingList.Add (oneStarSpeaker);
			speakerRatingList.Add (twoStarSpeaker);
			speakerRatingList.Add (threeStarSpeaker);
			speakerRatingList.Add (fourStarSpeaker);
			speakerRatingList.Add (fiveStarSpeaker);

			var speakerButtonLayout = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 5,
				Children = 
				{
					oneStarSpeaker,
					twoStarSpeaker,
					threeStarSpeaker,
					fourStarSpeaker,
					fiveStarSpeaker
				}
			};
			/* - * - * - * - * - * - * - * - * - * - * - * - * - */
			/* - * - * - * - * - * - * - * - * - * - * - * - * - */

			var scrollArea = new ScrollView 
			{
				IsEnabled = false,
				Content = new StackLayout 
				{
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					VerticalOptions = LayoutOptions.StartAndExpand,
					Spacing = 20,
					Padding = new Thickness(0, 20, 0, 20),
					Children = 
					{
						// content rating label
						new Label
						{
							Text = "Content Rating",
							Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
							XAlign = TextAlignment.Start
						},

						// stars (list of star images, clicked & unclicked)
						contentButtonLayout,

						// speaker rating label
						new Label
						{
							Text = "Speaker Rating",
							Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
							XAlign = TextAlignment.Start
						},

						// stars (list of star images, clicked & unclicked)
						speakerButtonLayout,

						// additional comments (textBox)
						new CommentTextBox
						{
							WidthRequest = 300,
							HeightRequest = 200
						}
					}
				}
			};

			Content = scrollArea;
		}

		void UpdateRating (List<RatingStar> ratingList, int starNumTapped)
		{
			// loop through all of the stars and turn on/off the correct ones
			for (int i = 0; i < ratingList.Count; i++) 
			{
				if (i + 1 <= starNumTapped) {
					ratingList [i].TurnStarOn ();
				} else {
					ratingList [i].TurnStarOff ();
				}
			}
		}
	}
}

