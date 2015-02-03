using NUnit.Framework;
using System;
using System.Drawing;

namespace CouchbaseConnect2014.Tests
{
	[TestFixture]
	public class ImageCropLogicTests
	{
		[Test]
		public void can_get_a_square_from_a_square_image ()
		{
			var input = new SizeF (500, 500);
			var output = ImageCropLogic.GetLargestSquareFromRectangle (input);

            var target = new RectangleF (0, 0, 500, 500);

			Assert.AreEqual (target, output);
		}

		[Test]
		public void CanGetSquareFromTallImage ()
		{
            var input = new SizeF (900, 2400);
			var output = ImageCropLogic.GetLargestSquareFromRectangle (input);

            var target = new RectangleF (0, 750, 900, 900);

			Assert.AreEqual (target, output);
		}

		[Test]
		public void CanGetSquareFromWideImage ()
		{
            var input = new SizeF (2400, 900);
			var output = ImageCropLogic.GetLargestSquareFromRectangle (input);

            var target = new RectangleF (750, 0, 900, 900);

			Assert.AreEqual (target, output);
		}
	}
}

