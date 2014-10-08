using System;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class ScavengerHuntCellViewModel : BaseViewModel
	{
		public ScavengerHuntCellViewModel (ScavengerHuntView item)
		{
			HuntItem = item;

			IsCaptured = item.IsCaptured;
			CapturedImage = item.CaptureImage;
			HuntItemDescription = item.ItemDescription;
		}

		internal ScavengerHuntView HuntItem { get; private set; }
			
        bool _isCaptured;
		public bool IsCaptured
		{
			get { return _isCaptured; }
			set 
			{ 
				SetObservableProperty (ref _isCaptured, value); 
			}
		}

		// Image: what you have
		byte[] _capturedImage;
		public byte[] CapturedImage 
		{
			get { return _capturedImage; }
			set 
			{ 
				if (value == null)
					return;
				SetObservableProperty (ref _capturedImage, value);
			}
		}

//		// Image: what you need
//		byte[] _huntNeedImage;
//		public byte[] HuntNeedImage 
//		{
//			get { return _huntNeedImage; }
//			set 
//			{ 
//				SetObservableProperty (ref _huntNeedImage, value);
//			}
//		}

		string _huntItemDescription;
		public string HuntItemDescription 
		{
			get { return _huntItemDescription; }
			set 
			{ 
				SetObservableProperty (ref _huntItemDescription, value); 
			}
		}
	}
}

