using System;
using System.Drawing;
using Android.Graphics;
using System.IO;
using Android.Media;

namespace CouchbaseConnect2014.Services
{
	public class ImageResizerService : IImageResizerService
	{
		public byte[] ResizeImage (byte[] imageBytes, int newWidth, int newHeight)
		{
			Byte[] NewByteArray = null;

			// byte[] -> bitmap
			using (var image = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length)) {

				using (var bitmap = ThumbnailUtils.ExtractThumbnail (image, newWidth, newHeight)) {

					// bitmap -> byte[]
					MemoryStream stream = new MemoryStream ();
					bitmap.Compress (Bitmap.CompressFormat.Png, 0, stream);
					NewByteArray = stream.ToArray ();
				}
			}
				
			return NewByteArray;
		}
	}
}

