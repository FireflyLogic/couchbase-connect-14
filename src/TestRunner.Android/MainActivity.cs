using System.Reflection;

using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using TinyIoC;
using CouchbaseConnect2014.Services;

namespace TestRunner.Android
{
	[Activity (Label = "TestRunner.Android", MainLauncher = true)]
	public class MainActivity : TestSuiteActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			// tests can be inside the main assembly
			AddTest (Assembly.GetExecutingAssembly ());
			// or in any reference assemblies
			// AddTest (typeof (Your.Library.TestClass).Assembly);

			SetupDependencies ();

			// Once you called base.OnCreate(), you cannot add more assemblies.
			base.OnCreate (bundle);
		}

		void SetupDependencies()
		{
			TinyIoCContainer.Current.Register<IImageResizerService> (new ImageResizerService ());
		}
	}
}

