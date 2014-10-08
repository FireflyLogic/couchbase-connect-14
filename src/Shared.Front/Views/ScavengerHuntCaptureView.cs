using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Controls;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014.Views
{
	public class ScavengerHuntCaptureView : CameraContentPage
	{
		Models.ScavengerHuntView HuntItem;

		public ScavengerHuntCaptureView (Models.ScavengerHuntView item)
		{
			BaseViewModel.CreateAndBind<ScavengerHuntCaptureViewModel> (this, item);
			SetBinding (ScavengerHuntCaptureView.NavigationProperty, new Binding ("Navigation"));

			Title = "Found Something?";
			NavigationPage.SetBackButtonTitle (this, "");

			HuntItem = item;

			SetBinding (CameraContentPage.IsPresentedProperty, new Binding("CameraIsPresented", BindingMode.TwoWay));
			SetBinding (CameraContentPage.CapturedImageProperty, new Binding("CameraImage", BindingMode.TwoWay));

			var relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var needStack = CreateNeedStack ();
			relativeLayout.Children.Add (needStack,
				Constraint.RelativeToParent (parent => { return parent.X; }),
				Constraint.RelativeToParent (parent => { return parent.Y; }),
				Constraint.RelativeToParent (parent => { return parent.Width; }),
				Constraint.RelativeToParent (parent => { return (parent.Height / 2) - 0.25; })
			);

			var separator = CreateSeparator ();
			relativeLayout.Children.Add (separator,
				Constraint.RelativeToParent (parent => { return parent.X; }),
				Constraint.RelativeToView (needStack, (parent,sibling) => { return sibling.Height; }),
				Constraint.RelativeToParent (parent => { return parent.Width; }),
				Constraint.Constant (0.5)
			);

			var haveStack = CreateHaveStack ();
			relativeLayout.Children.Add (haveStack,
				Constraint.RelativeToParent (parent => { return parent.X; }),
				Constraint.RelativeToView (separator, (parent,sibling) => { return sibling.Y + sibling.Height; }),
				Constraint.RelativeToParent (parent => { return parent.Width; }),
				Constraint.RelativeToParent (parent => { return (parent.Height / 2) - 0.25; })
			);

			Content = relativeLayout;
		}

		View CreateNeedStack ()
		{
			return new StackLayout 
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 5,
				Padding = new Thickness (0, 15, 0, 15),
				Children =
				{
					CreateNeedLabel(),
					CreateDescriptionLabel(),
					CreateNeedImage()
				}
			};
		}

		View CreateNeedLabel ()
		{
			var needLabel = new FontLabel 
			{
				Text = "What you need",
				TextColor = Color.FromHex ("333333"),
				Font = Font.OfSize (Fonts.OpenSansLight, 20),
				XAlign = TextAlignment.Center
			};

			// extra layout because text wasn't centered after
			// returning from taking a picture
			var needLabelView = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					needLabel
				}
			};

			return needLabelView;
		}

		View CreateDescriptionLabel ()
		{
			var descriptionLabel = new FontLabel 
			{
				Text = HuntItem.ItemDescription,
				TextColor = Color.FromHex ("26ade6"),
				Font = Font.OfSize (Fonts.OpenSansBold, 12),
				XAlign = TextAlignment.Center
			};

			// extra layout because text wasn't centered after
			// returning from taking a picture
			var descriptionLabelView = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					descriptionLabel
				}
			};

			return descriptionLabelView;
		}

		View CreateNeedImage ()
		{
			var needImage = new RoundedImageView 
			{
				//Source = "temp_selfie.jpg",
				WidthRequest = 120,
				HeightRequest = 120
			};
			//needImage.SetBinding (Image.SourceProperty, "WhatYouNeedImage", converter: new ByteArrayToImageSourceConverter());
			needImage.SetBinding (Image.SourceProperty, "WhatYouNeedImage");

			var needImageView = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					needImage
				}
			};

			return needImageView;
		}

		View CreateSeparator ()
		{
			return new BoxView 
			{
				Color = Color.FromHex ("c8c7cc")
			};
		}

		View CreateHaveStack ()
		{
			var grid = new Grid {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					CreateHaveImage(),
					CreateSmallImage()
				}
			};

			var stack = new StackLayout {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 10,
				Padding = new Thickness (0, 15, 0, 15),
				Children = {
					CreateHaveLabel(),
					grid
				}
			};

			return stack;
		}

		View CreateHaveLabel ()
		{
			var haveLabel = new FontLabel 
			{
				Text = "What you have",
				TextColor = Color.FromHex ("333333"),
				Font = Font.OfSize (Fonts.OpenSansLight, 20),
				XAlign = TextAlignment.Center
			};

			// extra layout because text wasn't centered after
			// returning from taking a picture
			var haveLabelView = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					haveLabel
				}
			};

			return haveLabelView;
		}

		View CreateHaveImage ()
		{
			var haveImage = new RoundedImageView 
			{
				Source = "camera_large.png",
				WidthRequest = 120,
				HeightRequest = 120,
			};
			haveImage.GestureRecognizers.Add (CreateTapGestureRecognizer ());
			haveImage.SetBinding (Image.SourceProperty, "CapturedImage");

			var haveImageView = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					haveImage
				}
			};

			return haveImageView;
		}

		View CreateSmallImage() {

			var image = new RoundedImageView {
				Source = "camera_small.png",
				WidthRequest = 40,
				HeightRequest = 40,
			};

			var view = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				TranslationX = 15,
				Children = 
				{
					image
				}
			};
			view.GestureRecognizers.Add (CreateTapGestureRecognizer ());
			view.SetBinding (StackLayout.IsVisibleProperty, "IsCaptured");

			return view;
		}

		TapGestureRecognizer CreateTapGestureRecognizer ()
		{
			var tapGestureRecognizer = new TapGestureRecognizer ();

			tapGestureRecognizer.SetBinding (TapGestureRecognizer.CommandProperty, "LaunchCamera");

			return tapGestureRecognizer;
		}
	}
}

