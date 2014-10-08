using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TinyIoC;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.Tests
{
	[TestFixture]
	public class ImageResizerServiceTests
	{
		[TestCase]
		public void can_resize_landscape_image()
		{
			byte[] imageBytes = ReadImageFromResources ("landscape_3264×2448.JPG");

			var service = TinyIoCContainer.Current.Resolve<IImageResizerService> ();
			Assert.IsNotNull (service, "pre-condition failed - need an instance of IImageResizerService");
		
			var newBytes = service.ResizeImage (imageBytes, 240, 240);
			WriteImageToDisk ("landscape_240×240.png", newBytes); 
		}	

		[TestCase]
		public void can_resize_square_image()
		{
			byte[] imageBytes = ReadImageFromResources ("square_2448×2448.JPG");

			var service = TinyIoCContainer.Current.Resolve<IImageResizerService> ();
			Assert.IsNotNull (service, "pre-condition failed - need an instance of IImageResizerService");

			var newBytes = service.ResizeImage (imageBytes, 240, 240);
			WriteImageToDisk ("square_240×240.png", newBytes); 
		}	

		byte[] ReadImageFromResources(string imageName)
		{
			var assembly = Assembly.GetCallingAssembly ();
			var names = assembly.GetManifestResourceNames ();
			var resourceName = names.Where (x => x.EndsWith (imageName, StringComparison.CurrentCultureIgnoreCase)).Single ();
			Console.WriteLine ("resourceName: {0}", resourceName);
			using (var stream = assembly.GetManifestResourceStream (resourceName))
			{
				Console.WriteLine ("Stream length: {0}", stream.Length);
				using(var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			}		
		}

		static void WriteImageToDisk (string imageName, byte[] newBytes)
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			// iOS 7 and earlier
			var filename = Path.Combine (documents, imageName);
			File.WriteAllBytes (filename, newBytes);
		}
	}
}

