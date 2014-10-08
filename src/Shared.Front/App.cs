using System;
using System.Collections.Generic;
using Couchbase.Lite;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Views;
using TinyIoC;

namespace CouchbaseConnect2014
{
	public class App
	{
        public async static void Initialize () {

			TinyIoCContainer.Current.Register<QrCodeScannerView> ();
			TinyIoCContainer.Current.Register<ITwitterService> (new TwitterService ());
			TinyIoCContainer.Current.Register<ITimeService> (new TimeService ());
			TinyIoCContainer.Current.Register<ICouchbaseUserService> (new CouchbaseUserService());

			var couchbaseService = new CouchbaseService ();
			TinyIoCContainer.Current.Register<ICouchbaseService> (couchbaseService);
			TinyIoCContainer.Current.Register<IRepository>(new Repository (couchbaseService));

			await couchbaseService.InitializeDatabase();
        }

		public static Dictionary<string, TrackColorScheme> TrackColor = new Dictionary<string, TrackColorScheme> () 
		{
            //;Development;Enterprise;Internals;Mobile;Operations;Customer
			{"Mobile",Colors.Track.Mobile},
			{"Enterprise",Colors.Track.Enterprise},
			{"Developer",Colors.Track.Developer},
			{"Operations",Colors.Track.Operations},
			{"Architecture",Colors.Track.Architecture},
			{"Customer",Colors.Track.Customer},

			// Non-track Tracks.... used for colors of cells
			{"Registration", Colors.Track.Registration},
			{"MealBreak",Colors.Track.MealBreak},
			{"NoTrack", Colors.Track.NoTrack},
		};

		public static Page GetMainPage ()
		{
            return new RootView ();
        }

        public static TinyIoCContainer Container {
            get {
                return TinyIoCContainer.Current;
            }
        }
	}
}

