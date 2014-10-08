using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ScanditSDK;

namespace ScanditSDKDemo
{
	public class ScanditSDKRotatingBarcodePicker : SIBarcodePicker
	{
		public ScanditSDKRotatingBarcodePicker (string appKey) : base(appKey)
		{

		}
		

		public override bool ShouldAutorotate () {
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations () {
			return UIInterfaceOrientationMask.All;
		}
	}
}

