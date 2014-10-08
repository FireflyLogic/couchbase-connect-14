using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.Tests
{
	public class MockTwitterService : ITwitterService
	{
		public Task<IList<Tweet>> GetTweets (string listName, string screenName, string accessToken)
		{
			return Task.FromResult<IList<Tweet>>(new List<Tweet>());
		}
	}
}

