using System;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Android.Content;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Views;
using CouchbaseConnect2014.Android;
using Android.Widget;
using Android.Graphics;
using CouchbaseConnect2014.ValueConverters;
using System.Globalization;

[assembly:Xamarin.Forms.ExportRenderer(typeof(AgendaCell), typeof(AgendaCellRenderer))]
namespace CouchbaseConnect2014.Android
{
	public class AgendaCellRenderer : CellRenderer
	{
		static Typeface regular = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Regular.ttf");
		static Typeface bold = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Bold.ttf");
		static Typeface light = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Light.ttf");

		protected override View GetCellCore (Xamarin.Forms.Cell item, View convertView, ViewGroup parent, Context context)
		{
			var viewModel = (AgendaCellViewModel)item.BindingContext;
			LayoutInflater layoutInflater = LayoutInflater.FromContext (context);

			// creates the text color to be used depending on the track of the session
			var textColor = ((Xamarin.Forms.Color)new TrackTextColorConverter ()
				.Convert (viewModel.Track, typeof(Xamarin.Forms.Color), 
					null, CultureInfo.CurrentCulture))
				.ToAndroid ();

			var view = convertView;
			if (view == null)
				view = layoutInflater.Inflate (Resource.Layout.AgendaCell, null);

			if (viewModel.IsBooked) {

				SetupTitle (view, viewModel, textColor, true);
				SetupLocation (view, viewModel, textColor, true);
				SetupTrack (view, viewModel, textColor, true);

				/**** Don't show these if user has selected a session ****/
				SetupAddIcon (view, false);
				SetupChooseLabel (view, textColor, false);

			} else {

				SetupAddIcon (view, true);
				SetupChooseLabel (view, textColor, true);

				/**** Don't show these if the user hasn't selected a session haven't ****/
				SetupTitle (view, viewModel, textColor, false);
				SetupLocation (view, viewModel, textColor, false);
				SetupTrack (view, viewModel, Color.Red, false);
			}

			SetupTime (view, viewModel, textColor);
			SetupAMPMLabel (view, viewModel, textColor);

			// set the background color of the cell to the appropriate track color
			Color background = ((Xamarin.Forms.Color)new TrackBackgroundColorConverter ()
				.Convert (viewModel.Track, typeof(Xamarin.Forms.Color), null, CultureInfo.CurrentCulture)).ToAndroid ();
			view.SetBackgroundColor (background);

			return view;
		}

		void SetupTitle(View view, AgendaCellViewModel viewModel, Color textColor, bool visible) {

			TextView title = view.FindViewById<TextView>(Resource.Id.title);

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

		void SetupLocation(View view, AgendaCellViewModel viewModel, Color textColor, bool visible) {

			TextView location = view.FindViewById<TextView>(Resource.Id.location);

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

		void SetupTrack(View view, AgendaCellViewModel viewModel, Color textColor, bool visible) {

			TextView track = view.FindViewById<TextView>(Resource.Id.track);

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

		void SetupAddIcon(View view, bool visible) {

			ImageView image = view.FindViewById<ImageView> (Resource.Id.image);

			if (visible) {
				image.Visibility = ViewStates.Visible;
			} else {
				image.Visibility = ViewStates.Gone;
			}
		}

		void SetupChooseLabel(View view, Color textColor, bool visible) {

			TextView choose = view.FindViewById<TextView> (Resource.Id.choose);

			if (visible) {
				choose.Visibility = ViewStates.Visible;
				choose.SetTextColor (textColor);
				choose.Typeface = light;
				choose.TextSize = 14;
			} else {
				choose.Visibility = ViewStates.Gone;
			}
		}

		void SetupTime(View view, AgendaCellViewModel viewModel, Color textColor) {

			TextView time = view.FindViewById<TextView>(Resource.Id.time);
			time.Text = viewModel.Time.ToString ("h:mm");
			time.SetTextColor (textColor);
			time.TranslationY = 7;
			time.Typeface = light;
			time.TextSize = 24;
		}

		void SetupAMPMLabel(View view, AgendaCellViewModel viewModel, Color textColor) {

			TextView timelabel = view.FindViewById<TextView>(Resource.Id.timelabel); 
			timelabel.Text = viewModel.Time.ToString ("tt");
			timelabel.SetTextColor (textColor);
			timelabel.Typeface = bold;
			timelabel.TextSize = 9;
		}
	}
}

