using System;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Foundation;

namespace CouchbaseConnect2014.Services
{
	public class ImageResizerService : IImageResizerService
	{
		public byte[] ResizeImage (byte[] imageBytes, int newWidth, int newHeight)
		{
			// byte[] -> NSData -> UIImage
			Byte[] NewByteArray = null;
			using (var data = NSData.FromArray (imageBytes)) {
				using (UIImage image = new UIImage (data)) {

                    var cropRect = (CGRect)ImageCropLogic.GetLargestSquareFromRectangle ((SizeF)image.Size);
					var cropped = CropImage(image, cropRect);
					var resized = ResizeImage (cropped, newWidth, newHeight);

					using (NSData imageData = resized.AsPNG ()) {
						NewByteArray = new Byte[imageData.Length];
						System.Runtime.InteropServices.Marshal.Copy (imageData.Bytes, NewByteArray, 0, Convert.ToInt32 (imageData.Length));
					}
				}
			}
			return NewByteArray;
		}

		// resize the image (without trying to maintain aspect ratio)
		UIImage ResizeImage(UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContext(new CGSize(width, height));
			sourceImage.Draw(new CGRect(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}

		// crop the image, without resizing
		UIImage CropImage(UIImage sourceImage, CGRect rect)
		{
			var imgSize = sourceImage.Size;
			UIGraphics.BeginImageContext(new CGSize(rect.Width, rect.Height));
			var context = UIGraphics.GetCurrentContext();
			var clippedRect = new CGRect(0, 0, rect.Width, rect.Height);
			context.ClipToRect(clippedRect);
			var drawRect = new CGRect(-rect.X, -rect.Y, imgSize.Width, imgSize.Height);
			sourceImage.Draw(drawRect);
			var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return modifiedImage;
		}
	}
}

