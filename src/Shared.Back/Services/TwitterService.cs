using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CouchbaseConnect2014.Services
{
	public class TwitterService : ITwitterService
	{
		public async Task<IList<CouchbaseConnect2014.Models.Tweet>> GetTweets (string listName, string screenName, string accessToken)
		{
			try
			{
				using (var client = new HttpClient ())
				{
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", accessToken);
					var response = await client.GetAsync ("https://api.twitter.com/1.1/lists/statuses.json?slug=" + listName + "&owner_screen_name=" + screenName);
				
					if (response.StatusCode != System.Net.HttpStatusCode.OK)
					{
						string content = await response.Content.ReadAsStringAsync ();
						Console.WriteLine (content);
						return null;
					}
				
					Console.WriteLine ("Tweets fetched from Twitter.");
				
					string result = await response.Content.ReadAsStringAsync ();
					var dtoTweets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tweet>> (result);
				
					// flatten DTOs into application model
					var tweets = dtoTweets.Select (
						             x => new CouchbaseConnect2014.Models.Tweet () {
							Content = WebUtility.HtmlDecode (x.Text),
							Url = string.Format ("https://twitter.com/{0}/status/{1}", x.User.ScreenName, x.Id),
							Icon = x.User.ProfileImageUrlHttps,
							Name = x.User.Name,
							Twitter = x.User.ScreenName,
							CreatedAtDate = x.CreatedDate,
							FirstEmbeddedUrl = x.Entities.Urls.Count > 0 ? x.Entities.Urls [0].ShortUrl : null
						}
					             ).ToList ();
				
					return tweets;
				}
			}
			catch
			{
				return null;
			}
		}
			
		#region DTOs used to serialize twitter messages
		[JsonObject(MemberSerialization.OptIn)]
		class Tweet
		{
			[JsonProperty(PropertyName = "id_str")]
			public string Id
			{
				get;
				set;
			}
				
			[JsonProperty(PropertyName = "text")]
			public string Text
			{
				get;
				set;
			}

			string _createdDateString;
			[JsonProperty (PropertyName = "created_at")]
			public string CreatedDateString
			{
				get
				{
					return _createdDateString;
				}
				set
				{
					_createdDateString = value;

					try
					{
						CreatedDate = DateTime.ParseExact (value,
							"ddd MMM dd HH:mm:ss zzz yyyy",
							CultureInfo.InvariantCulture.DateTimeFormat);
					} catch (Exception ex)
					{
						Console.WriteLine (ex.Message);
					}
				}
			}

			public DateTime CreatedDate
			{
				get;
				private set;
			}

			[JsonProperty(PropertyName = "user")]
			public User User
			{
				get;
				set;
			}

			[JsonProperty(PropertyName = "entities")]
			public Entities Entities
			{
				get;
				set;
			}
		}

		[JsonObject(MemberSerialization.OptIn)]
		class User
		{
			[JsonProperty(PropertyName = "name")]
			public string Name
			{
				get;
				set;
			}

			[JsonProperty(PropertyName = "screen_name")]
			public string ScreenName
			{
				get;
				set;
			}

			[JsonProperty(PropertyName = "profile_image_url_https")]
			public string ProfileImageUrlHttps
			{
				get;
				set;
			}
		}

		[JsonObject(MemberSerialization.OptIn)]
		class Entities
		{
			[JsonProperty(PropertyName = "urls")]
			public IList<Url> Urls
			{
				get;
				set;
			}
		}

		[JsonObject(MemberSerialization.OptIn)]
		class Url
		{
			[JsonProperty(PropertyName = "expanded_url")]
			public string ExpandedUrl
			{
				get;
				set;
			}

			[JsonProperty(PropertyName = "url")]
			public string ShortUrl
			{
				get;
				set;
			}
		}
		#endregion
	}
}
	
