using System;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;

namespace CouchbaseConnect2014.Android.Views
{
    public class RoundedImageView : ImageView
    {
		public RoundedImageView (Context context, IAttributeSet attrs) : base (context)
		{
		}

		public RoundedImageView (Context context) : base (context)
        {
        }

        protected override void OnDraw (Canvas canvas)
        {
            var drawable = Drawable;

            if (drawable == null) return;

            if (Width == 0 || Height == 0) return;

            var b = ((BitmapDrawable)drawable).Bitmap;
            if (b == null) return;

            var bitmap = b.Copy (Bitmap.Config.Argb8888, true);

            var w = Width;
            var h = Height;

            var roundBitmap = GetCroppedBitmap (bitmap, w);
            canvas.DrawBitmap (roundBitmap, 0, 0, null);
        }

        static Bitmap GetCroppedBitmap (Bitmap bmp, int radius)
        {
            Bitmap sbmp;
            if (bmp.Width != radius || bmp.Height != radius)
                sbmp = Bitmap.CreateScaledBitmap (bmp, radius, radius, false);
            else
                sbmp = bmp;
            var output = Bitmap.CreateBitmap (sbmp.Width,
                             sbmp.Height, Bitmap.Config.Argb8888);
            var canvas = new Canvas (output);

            var paint = new Paint ();
            var rect = new Rect (0, 0, sbmp.Width, sbmp.Height);

            paint.AntiAlias = true;
            paint.FilterBitmap = true;
            paint.Dither = true;
            canvas.DrawARGB (0, 0, 0, 0);
            paint.Color = Color.ParseColor ("#BAB399");
            canvas.DrawCircle (sbmp.Width / 2 + 0.7f, sbmp.Height / 2 + 0.7f,
                sbmp.Width / 2 + 0.1f, paint);
            paint.SetXfermode (new PorterDuffXfermode (PorterDuff.Mode.SrcIn));
            canvas.DrawBitmap (sbmp, rect, rect, paint);

            return output;
        }
    }
}

