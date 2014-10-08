using System;
using NUnit.Framework;
using CouchbaseConnect2014.Services;
using System.Collections.Generic;
using System.Linq;

namespace CouchbaseConnect2014.Tests
{
	[TestFixture]
	public class CouchbaseUserServiceTests
	{
		[TestCase]
		public async void can_create_user_with_valid_parameters()
		{
			string authServerUrl = "https://demo-mobile.couchbase.com/connect2014/";
			string name = Guid.NewGuid ().ToString ();
			ICouchbaseUserService service = new CouchbaseUserService ();

			var success = await service.CreateUser (authServerUrl, name);

			Assert.IsTrue (success);

//			var user = await service.GetUser (authServerUrl, name);
//
//			Assert.IsNotNull (user);
//			Assert.AreEqual (name, user.Name, "name don't match");
//			Assert.IsNull (user.Channels, "expected null channel");
		}

		[TestCase]
		public async void can_get_well_known_user()
		{
			string name = "GUEST";
			string authServerUrl = "http://demo.mobile.couchbase.com/connect2014/";
			ICouchbaseUserService service = new CouchbaseUserService ();

			var user = await service.GetUser (authServerUrl, name);

			Assert.IsNotNull (user);
			Assert.AreEqual (name, user.Name);
			Assert.IsNull (user.Channels, "expected null channel");
		}

		[TestCase]
		public async void returns_null_for_unknown_user()
		{
			string name = Guid.NewGuid().ToString();
			string authServerUrl = "http://demo.mobile.couchbase.com/connect2014/";
			ICouchbaseUserService service = new CouchbaseUserService ();

			var user = await service.GetUser (authServerUrl, name);

			Assert.IsNull (user);
		}

	}
}

