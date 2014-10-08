using System;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.Android;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Android.Content;
using Android.Graphics;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.ValueConverters;
using System.Globalization;
using Android.Widget;
using System.ComponentModel;

[assembly: Xamarin.Forms.ExportRenderer (typeof(FullScheduleCell), typeof(FullScheduleCellRenderer))]
namespace CouchbaseConnect2014.Android
{		
	public class FullScheduleCellRenderer : CellRenderer
	{
		static Typeface regular = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Regular.ttf");
		static Typeface bold = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Bold.ttf");
		static Typeface light = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Light.ttf");

		protected override View GetCellCore (Xamarin.Forms.Cell item, View convertView, ViewGroup parent, Context context)
		{

			var viewModel = (FullScheduleCellViewModel)item.BindingContext;
			LayoutInflater layoutInflater = LayoutInflater.FromContext (context);

			// creates the text color to be used depending on the track of the session
			var textColor = ((Xamarin.Forms.Color)new TrackTextColorConverter ()
				.Convert (viewModel.Track, typeof(Xamarin.Forms.Color), 
					null, CultureInfo.CurrentCulture))
				.ToAndroid ();

			var view = convertView;
			//if (view == null)
				view = layoutInflater.Inflate (Resource.Layout.FullScheduleCell, null);

			SetupTitle (view, viewModel, textColor, true);
			SetupLocation (view, viewModel, textColor, true);
			SetupTrack (view, viewModel, textColor, true);

			if(viewModel.IsOptional)
				SetupSelectButton (view, viewModel, textColor);

			// set the background color of the cell to the appropriate track color
			Color background = ((Xamarin.Forms.Color)new TrackBackgroundColorConverter ()
				.Convert (viewModel.Track, typeof(Xamarin.Forms.Color), null, CultureInfo.CurrentCulture)).ToAndroid ();
			view.SetBackgroundColor (background);

			return view;
		}

		void SetupTitle(View view, FullScheduleCellViewModel viewModel, Color textColor, bool visible) {

			TextView title = view.FindViewById<TextView>(Resource.Id.fs_title);

			if (visible) {
				title.Visibility = ViewStates.Visible;
				title.Text = viewModel.Title;
				title.SetTextColor (textColor);
				title.Typeface = light;
				title.TextSize = 14;
			} else {
				title.Visibility = ViewStates.Gone;
			}
		}

		void SetupLocation(View view, FullScheduleCellViewModel viewModel, Color textColor, bool visible) {

			TextView location = view.FindViewById<TextView>(Resource.Id.fs_location);

			if (visible) {
				location.Visibility = ViewStates.Visible;
				location.Text = viewModel.Location.ToUpper ();
				location.SetTextColor (textColor);
				location.Typeface = bold;
				location.TextSize = 12;
			} else {
				location.Visibility = ViewStates.Gone;
			}
		}

		void SetupTrack(View view, FullScheduleCellViewModel viewModel, Color textColor, bool visible) {

			TextView track = view.FindViewById<TextView>(Resource.Id.fs_track);

			if (visible) {
				track.Visibility = ViewStates.Visible;
				track.Text = new TrackValueConverter ().Convert (viewModel.Track, typeof(string), null, CultureInfo.CurrentCulture) as string;
				track.SetTextColor (textColor);
				track.Typeface = regular;	
				track.TextSize = 12;
			} else {
				track.Visibility = ViewStates.Gone;
			}
		}

		void SetupSelectButton(View view, FullScheduleCellViewModel viewModel, Color textColor) {

			Button selector = view.FindViewById<Button> (Resource.Id.fs_select);

			UpdateButtonTitle (selector, viewModel);
			selector.SetTextColor (textColor);
			selector.Click += (object sender, EventArgs e) => {
				viewModel.ToggleSelection.Execute (null);
			};
			viewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				if (e.PropertyName == "IsSelected") {
					UpdateButtonTitle (selector, viewModel);
				}
			};
		}

		static void UpdateButtonTitle (Button button, FullScheduleCellViewModel viewModel) {
			button.SetTextSize (global::Android.Util.ComplexUnitType.Dip, viewModel.IsSelected ? 24 : 48);
			button.SetText (viewModel.IsSelected ? "✓" : "○", TextView.BufferType.Normal);
		}
	}
}

