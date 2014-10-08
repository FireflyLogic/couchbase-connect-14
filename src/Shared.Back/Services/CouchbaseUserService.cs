using System;
using CouchbaseConnect2014.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CouchbaseConnect2014.Services
{
	public class CouchbaseUserService : ICouchbaseUserService
	{
		public async Task<bool> CreateUser(string authServerUrl, string name)
		{
			try
			{
				string password = name;
				string email = null;
				
				var user = new User () {
					Name = name,
					Password = password,
					Email = email
				};
				var userJson = JsonConvert.SerializeObject (user);
				
				using (var client = new HttpClient ())
				{
					var body = new StringContent (userJson);
					var response = await client.PostAsync (authServerUrl + "_user/", body);
				
					if (response.StatusCode != System.Net.HttpStatusCode.Created)
					{
						string content = await response.Content.ReadAsStringAsync ();
						Console.WriteLine ("Problem creating couchbase user: " + content);
						return false;
					}
				}
				
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Exception creating couchbase user: " + ex.Message);
				return false;
			}
		}

		public async Task<CouchbaseUser> GetUser(string authServerUrl, string name)
		{
			using (var client = new HttpClient ())
			{
				var response = await client.GetAsync (authServerUrl + "_user/" + name);

				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					string content = await response.Content.ReadAsStringAsync ();
					Console.WriteLine (content);
					return null;
				}

				string result = await response.Content.ReadAsStringAsync ();
				var dtoUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User> (result);

				// flatten DTOs into application model
				var user = new CouchbaseUser () {
					Name = dtoUser.Name
				};

				return user;
			}
		}

		class User
		{
			[JsonProperty (PropertyName = "name")]
			public string Name
			{
				get;
				set;
			}

			[JsonProperty (PropertyName = "email")]
			public string Email
			{
				get;
				set;
			}

			[JsonProperty (PropertyName = "password")]
			public string Password
			{
				get;
				set;
			}
		}
	}
}

