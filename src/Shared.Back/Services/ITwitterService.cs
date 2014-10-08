using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchbaseConnect2014.Services
{
	public interface ITwitterService
	{
		Task<IList<CouchbaseConnect2014.Models.Tweet>> GetTweets (string listName, string screenName, string accessToken);
	}
}

