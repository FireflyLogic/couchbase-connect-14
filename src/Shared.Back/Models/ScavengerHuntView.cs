using System;

namespace CouchbaseConnect2014.Models
{
	public class ScavengerHuntView
	{
		public string ItemId
		{
			get;
			set;
		}

		public string ItemDescription
		{
			get;
			set;
		}

		public byte[] ItemImage
		{
			get;
			set;
		}

		public byte[] CaptureImage
		{
			get;
			set;
		}

		public int Order
		{
			get;
			set;
		}

		public bool IsCaptured
		{
			get
			{
				return CaptureImage != null;
			}
		}
	}
}

