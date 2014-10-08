using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.ValueConverters;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.Views
{
	public class ChooseSessionView : ContentPage
	{
		CarouselLayout sessionInfoCarousel;

        public ChooseSessionView (Slot slot, Session session)
		{
            BaseViewModel.CreateAndBind<ChooseSessionViewModel> (this, slot, session);

			NavigationPage.SetBackButtonTitle (this, "");

            ToolbarItems.Add (CreateSelectButton ());

            SetBinding (ChooseSessionView.NavigationProperty, new Binding ("Navigation"));
            SetBinding (ChooseSessionView.TitleProperty, new Binding ("Title"));
            SetBinding (ChooseSessionView.SelectButtonTextProperty, new Binding ("SelectButtonText"));

			var relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var lowerStackLayout = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Spacing = 20,
				Padding = new Thickness (0, 20, 0, 20),
			};
			lowerStackLayout.Children.Add (CreatePips ());
			lowerStackLayout.Children.Add (CreateRateButton ());

			relativeLayout.Children.Add (lowerStackLayout,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToParent ((parent) => { return parent.Height - 100; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; })
			);

			sessionInfoCarousel = CreateSessionInfoCarousel ();
			relativeLayout.Children.Add (sessionInfoCarousel,
				Constraint.RelativeToParent ((parent) => { return parent.X; }),
				Constraint.RelativeToParent ((parent) => { return parent.Y; }),
				Constraint.RelativeToParent ((parent) => { return parent.Width; }),
				Constraint.RelativeToView (lowerStackLayout, (parent,sibling) => { return parent.Height - sibling.Height; })
			);

			var fadeOut = CreateFadeOut ();
			relativeLayout.Children.Add (fadeOut,
				Constraint.RelativeToParent (parent => { return parent.X; }),
				Constraint.RelativeToView (lowerStackLayout, (parent,sibling) => { return sibling.Y - 30; }),
				Constraint.RelativeToParent (parent => { return parent.Width; }),
				Constraint.Constant (40)
			);

			Content = relativeLayout;
		}

        public static BindableProperty SelectButtonTextProperty =
            BindableProperty.Create<ChooseSessionView, string> (
                b => b.SelectButtonText,
                "",
                propertyChanged: (bindable, oldValue, newValue) => {
                    var view = (ChooseSessionView)bindable;
                    view.SetSelectButtonName (newValue);
                }
            );

        public string SelectButtonText {
            get {
                return (string)GetValue (SelectButtonTextProperty);
            }
            set {
                SetValue (SelectButtonTextProperty, value);
            }
        }

        void SetSelectButtonName (string text)
        {
            if (ToolbarItems.Count > 0) {
                var item = ToolbarItems [0] as ToolbarItem;
                item.Name = text;
                ToolbarItems [0] = item;
            }
        }

        ToolbarItem CreateSelectButton ()
        {
            var selectButton = new ToolbarItem { Name = "Select" };
            selectButton.SetBinding (ToolbarItem.CommandProperty, "ToggleSessionSelection");
            return selectButton;
        }

		CarouselLayout CreateSessionInfoCarousel ()
		{
			var carousel = new CarouselLayout 
			{
				ItemTemplate = new DataTemplate(typeof(SessionInfoView)),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			carousel.SetBinding (CarouselLayout.ItemsSourceProperty, "SlotSessions");
			carousel.SetBinding (CarouselLayout.SelectedItemProperty, "SlotSession");

			return carousel;
		}

		View CreateFadeOut ()
		{
			var fadeoutImage = new Image 
			{
				Source = "fadeout.png",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				HeightRequest = 40,
				Aspect = Aspect.Fill
			};

			return fadeoutImage;
		}

		View CreatePips () {
			var pipSet = new PipSet ();
			pipSet.SetBinding (PipSet.ItemsSourceProperty, "SlotSessions");
			pipSet.SetBinding (PipSet.SelectedItemProperty, "SlotSession");
			return pipSet;
		}

		Button CreateRateButton ()
		{
			var rateButton = new RateButton 
			{
				Text = "Rate Session",
				TextColor = Color.FromHex ("007aff"),
				Font = Font.OfSize(Fonts.OpenSans, 12),
				BackgroundColor = Color.Transparent,
				BorderColor = Color.FromHex ("007aff"),
				BorderRadius = 7,
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.End,
				WidthRequest = 140,
				HeightRequest = 35
			};

			rateButton.Clicked += (object sender, EventArgs e) => 
			{
				var selectedSession = (SessionInfoViewModel)sessionInfoCarousel.SelectedItem;
				var sessionId = selectedSession.Session.Id;

				Navigation.PushAsync(new RateSessionView(sessionId));
			};

			return rateButton;
		}
	}
}

