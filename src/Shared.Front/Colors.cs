using System;
using Xamarin.Forms;

namespace CouchbaseConnect2014
{
	public static class Colors
	{
        public static Color MenuBackground = Color.FromHex("121212");
        public static Color MenuText = Color.White;

		public static class Track
		{
            public static TrackColorScheme Plenary = new TrackColorScheme {
				Background = Color.FromHex("fcfcfc"),
				PrimaryText = Color.FromHex("333333"),
                SecondaryText = Color.Black
            };

            public static TrackColorScheme Mobile = new TrackColorScheme {
                Background = Color.FromHex("26ade6"),
				PrimaryText = Color.FromHex("cff2ff"),
                SecondaryText = Color.White
            };

            public static TrackColorScheme Enterprise = new TrackColorScheme {
                Background = Color.FromHex("126589"),
				PrimaryText = Color.FromHex("b3eaff"),
                SecondaryText = Color.White
            };

            public static TrackColorScheme Developer = new TrackColorScheme {
                Background = Color.FromHex("47cec5"),
				PrimaryText = Color.FromHex("065f59"),
                SecondaryText = Color.White
            };

            public static TrackColorScheme Operations = new TrackColorScheme {
                Background = Color.FromHex("ffe831"),
				PrimaryText = Color.FromHex("c87100"),
                SecondaryText = Color.White
            };

            public static TrackColorScheme Architecture = new TrackColorScheme {
                Background = Color.FromHex("ee6839"),
				PrimaryText = Color.FromHex("932500"),
                SecondaryText = Color.White
            };

            public static TrackColorScheme Scalability = new TrackColorScheme {
                Background = Color.FromHex("ea2228"),
				PrimaryText = Color.FromHex("ffc0c2"),
                SecondaryText = Color.White
            };

            public static TrackColorScheme Customer = new TrackColorScheme {
                Background = Color.FromHex("cd2a2e"),
				PrimaryText = Color.FromHex("ffb098"),
                SecondaryText = Color.White
            };

			/* These are NOT actual tracks, just a way to re-use Color code thru-out app */
			public static TrackColorScheme Registration = new TrackColorScheme
			{
				Background = Color.FromHex("373e41"),
				PrimaryText = Color.FromHex("ffffff"),
				SecondaryText = Color.White
			};

			public static TrackColorScheme MealBreak = new TrackColorScheme
			{
				Background = Color.FromHex("289eac"),
				PrimaryText = Color.FromHex("d9ffff"),
				SecondaryText = Color.White
			};

			public static TrackColorScheme NoTrack = new TrackColorScheme
			{
				Background = Color.FromHex("dfebeb"),
				PrimaryText = Color.FromHex("286779"),
				SecondaryText = Color.White
			};
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
		}  
	}
}
