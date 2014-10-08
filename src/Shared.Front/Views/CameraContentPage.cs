using System;
using Xamarin.Forms;

namespace CouchbaseConnect2014.Views
{
	public class CameraContentPage : ContentPage
	{
		public event EventHandler CameraRequested;

		public CameraContentPage ()
		{
		}

		public static readonly BindableProperty IsPresentedProperty = 
			BindableProperty.Create<CameraContentPage, bool> (
				b => b.IsPresented, 
				false, 
				propertyChanged: (bindable, oldValue, newValue) => 
				{
					if(newValue)
					{
						// raise event for renderer to pick-up
						var cvb = (CameraContentPage)bindable;
						cvb.OnCameraRequested();
						// TODO: put value (CameraIsPresented) back to false
					}
				});

		public static readonly BindableProperty CapturedImageProperty = 
			BindableProperty.Create<CameraContentPage, string> (
				b => b.Captured, 
				"test", 
				propertyChanged: (bindable, oldValue, newValue) => {
					Console.WriteLine ("OLD: " + oldValue + " NEW: " + newValue);
				});

		public string Captured
		{
			get { return (string)GetValue (CapturedImageProperty); }
			set { SetValue (CapturedImageProperty, value); }
		}

		public bool IsPresented
		{
			get { return (bool)GetValue (IsPresentedProperty); }
			set { SetValue (IsPresentedProperty, value); }
		}
			
		protected void OnCameraRequested ()
		{
			if (CameraRequested != null)
			{
				CameraRequested (this, new EventArgs());
			}
		}
	}
}

