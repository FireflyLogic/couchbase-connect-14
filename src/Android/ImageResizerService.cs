using System;

namespace CouchbaseConnect2014.Services
{
	public class ImageResizerService : IImageResizerService
	{
		public ImageResizerService ()
		{
		}

		#region IImageResizerService implementation

		public byte[] ResizeImage (byte[] imageBytes, int newWidth, int newHeight)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

