using System;
using CouchbaseConnect2014;
using CouchbaseConnect2014.Android;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Android.Content;
using Android.Widget;
using Android.Graphics;
using System.ComponentModel;


[assembly:Xamarin.Forms.ExportRenderer(typeof(ScavengerHuntCell), typeof(ScavengerHuntCellRenderer))]
namespace CouchbaseConnect2014.Android
{
	public class ScavengerHuntCellRenderer : CellRenderer
	{
		static Typeface light = Typeface.CreateFromAsset (Xamarin.Forms.Forms.Context.Assets, "OpenSans-Light.ttf");

		protected override View GetCellCore (Xamarin.Forms.Cell item, View convertView, ViewGroup parent, Context context)
		{
			var viewModel = (ScavengerHuntCellViewModel)item.BindingContext;
			LayoutInflater layoutInflater = LayoutInflater.FromContext (context);

			var view = convertView;
			if (view == null)
				view = layoutInflater.Inflate (Resource.Layout.ScavengerHuntCell, null);

			SetupLabel (view, viewModel, Color.Black);
			SetupImage (view, viewModel);

			return view;
		}

		void SetupImage(View view, ScavengerHuntCellViewModel viewModel) {

			ImageView image = view.FindViewById<ImageView>(Resource.Id.image);

			UpdateImage (image, viewModel);

			viewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				if (e.PropertyName == "CapturedImage") {
					UpdateImage (image, viewModel);
				}
			};
		}

		void SetupLabel(View view, ScavengerHuntCellViewModel viewModel, Color textColor) {

			TextView label = view.FindViewById<TextView>(Resource.Id.label);

			label.Text = viewModel.HuntItemDescription;
			label.SetTextColor (textColor);
			label.Typeface = light;
			label.TextSize = 18;
		}

		static void UpdateImage (ImageView image, ScavengerHuntCellViewModel viewModel) {
			if (viewModel.CapturedImage != null) {
				using (var bitmap = BitmapFactory.DecodeByteArray (viewModel.CapturedImage, 0, viewModel.CapturedImage.Length)) {
					image.SetImageBitmap (bitmap);
				}
			} else {
				image.SetImageResource (Resource.Drawable.add_pic);
			}
		}
	}
}

