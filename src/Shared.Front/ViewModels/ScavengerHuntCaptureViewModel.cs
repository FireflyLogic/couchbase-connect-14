using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Views;
using System.IO;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014
{
	public class ScavengerHuntCaptureViewModel : BaseViewModel
	{
		readonly IRepository _repository;
		readonly IImageResizerService _imageResizerService;

		internal override Task Initialize (params object[] args)
		{
			ScavengerHuntItem = (Models.ScavengerHuntView)args [0];
			return base.Initialize (args);
		}

		public ScavengerHuntCaptureViewModel (IRepository repository, IImageResizerService imageResizerService)
		{
			_repository = repository;
			_imageResizerService = imageResizerService;
		}

		Models.ScavengerHuntView _scavengerHuntItem;
		public Models.ScavengerHuntView ScavengerHuntItem {
			get {
				return _scavengerHuntItem;
			}
			set {
				_scavengerHuntItem = value;

				// not right...
				if (value.ItemImage != null && value.ItemImage.Length > 0)
					ItemImage = value.ItemImage;
				else
					WhatYouNeedImage = "comingSoon.png";

				IsCaptured = value.IsCaptured;

				// not right...
				if (value.CaptureImage != null)
					Photo = value.CaptureImage;
				else
					CapturedImage = "camera_large.png";
			}
		}

		byte[] _itemImage;
		public byte[] ItemImage {
			get {
				return _itemImage;
			}
			set {
				_itemImage = value;

				WhatYouNeedImage = FileImageSource.FromStream (() => {
					return new MemoryStream (value);
				});
			}
		}

		ImageSource _whatYouNeedImage;
		public ImageSource WhatYouNeedImage {
			get {
				return _whatYouNeedImage;
			}
			set {
				SetObservableProperty(ref _whatYouNeedImage, value);
			}
		}

		bool _isCaptured;
		public bool IsCaptured {
			get {
				return _isCaptured;
			}
			set {
				SetObservableProperty(ref _isCaptured, value);
			}
		}

		string _cameraImage;
		public string CameraImage {
			get {
				return _cameraImage;
			}
			set {
				if (value == null)
					return;
				SetObservableProperty (ref _cameraImage, value);

				var tempPhoto = _imageResizerService.ResizeImage (File.ReadAllBytes (value), 240, 240);

				var capture = new ScavengerHuntCapture () {
					Image = tempPhoto,
					ItemId = ScavengerHuntItem.ItemId
				};
				_repository.SaveScavengerHuntCapture (capture);

				Photo = tempPhoto;
			}
		}

		byte[] _photo;
		public byte[] Photo {
			get {
				return _photo;
			}
			set {

				_photo = value;

				CapturedImage = FileImageSource.FromStream (() => {
					return new MemoryStream (value);
				});

				IsCaptured = true;
			}
		}

		ImageSource _capturedImage;
		public ImageSource CapturedImage {
			get {
				return _capturedImage;
			}
			set {
				SetObservableProperty (ref _capturedImage, value);
			}
		}

		bool _cameraIsPresented;
		public bool CameraIsPresented
		{
			get { return _cameraIsPresented; }
			set { SetObservableProperty (ref _cameraIsPresented, value); }
		}

		public ICommand LaunchCamera
		{
			get 
			{
				return new Command ((sender) => 
				{
					CameraIsPresented = true;
				});
			}
		}
	}
}

