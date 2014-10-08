using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CouchbaseConnect2014.Android;
using Android.Widget;
using System.Reflection;
using System.ComponentModel;
using System;
using Android.Graphics;
using Android.Util;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using FormsRoundedImageView = CouchbaseConnect2014.Controls.RoundedImageView;
using AndroidRoundedImageView = CouchbaseConnect2014.Android.Views.RoundedImageView;

[assembly: ExportRendererAttribute(typeof(FormsRoundedImageView), typeof(RoundedImageRenderer))]

namespace CouchbaseConnect2014.Android
{
	/// <summary>
	/// Custom renderer for the RoundedBoxView in Android.
	/// </summary>
	public class RoundedImageRenderer : ImageRenderer
	{
        protected override void OnElementChanged (ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged (e);

            if (e.OldElement == null) {
                ImageView nativeControl = new AndroidRoundedImageView (base.Context);
                base.SetNativeControl (nativeControl);
            }

            typeof(ImageRenderer)
                .GetMethod ("UpdateBitmap", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke (this, new object [] { });

            typeof(ImageRenderer)
                .GetMethod ("UpdateAspect", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke (this, new object [] { });
        }
	}
}
