using System;
using System.Drawing;

namespace CouchbaseConnect2014
{
	public static class ImageCropLogic
	{
		public static RectangleF GetLargestSquareFromRectangle (SizeF rect)
		{
			if (rect.Width == rect.Height)
				return new RectangleF(0, 0, rect.Width, rect.Height);

			float w = Math.Min (rect.Width, rect.Height);
			float h = w;
			float x = 0, y = 0;
			float offset = (Math.Max (rect.Width, rect.Height) - Math.Min (rect.Width, rect.Height)) / 2;

			if (rect.Width > rect.Height)
				x = offset;
			else
				y = offset;

			return new RectangleF(x, y, w, h);
		}
	}
}

