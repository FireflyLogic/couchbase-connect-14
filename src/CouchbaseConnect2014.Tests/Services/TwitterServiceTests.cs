using System;
using NUnit.Framework;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.Tests.Services
{
	[TestFixture]
	public class TwitterServiceTests
	{

		[Test]
		public async void should_return_tweets_for_valid_screen_name()
		{
			ITwitterService service = new TwitterService();

			var tweets = await service.GetTweets ("couchbase-connect-2014", "aobendorf", "AAAAAAAAAAAAAAAAAAAAAI8caQAAAAAA11sGE7k7NbRrgYcuHkDCFtxmvas%3DlOBjwbru32uLUHiVybiRu9WFpp69R52jWXnhqIAuXdLg0RORPX");

			Assert.IsNotNull (tweets);
			Assert.IsTrue (tweets.Count > 0);
		}
	}
}

