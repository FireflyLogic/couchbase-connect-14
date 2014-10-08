using System;

namespace CouchbaseConnect2014.Services
{
	public interface IImageResizerService
	{
		byte[] ResizeImage (byte[] imageBytes, int newWidth, int newHeight);
	}
}

