//#define USE_COUCHBASE_SERVER
#define CB_USER_AUTH_ENABLED
#define CB_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Lite;
using Couchbase.Lite.Auth;
using Newtonsoft.Json.Linq;
using CouchbaseConnect2014.Models;
using TinyIoC;

namespace CouchbaseConnect2014.Services
{
	public interface ICouchbaseService
	{
		Task InitializeDatabase ();
		Database Db { get; }
		void DebugLocalDatabaseInfo ();
		string GetUserId ();
		bool DoesUserExistOnServer ();
	}

	public class CouchbaseService : ICouchbaseService
	{
		#if (USE_COUCHBASE_SERVER)        
		const string COUCHBASE_ADDRESS = "https://demo-mobile.couchbase.com/connect2014/";
		#else
		const string COUCHBASE_ADDRESS = "http://192.168.217.213:4984/couchbase-connect";
		#endif
		const string DATABASE_NAME = "couchbase-connect";

		Database _db;
		string _userId = null;

		public async Task InitializeDatabase ()
		{
			CreateDatabase ();

			HandleFirstRunScenario ();
			await EnsureCouchbaseUserExists ();

			SetupViews ();
			SetupReplication (new Uri (COUCHBASE_ADDRESS));

			Console.WriteLine ("UserId:[{0}]", GetUserId());

			DebugLocalDatabaseInfo ();
		}

		void CreateDatabase ()
		{
			_db = Manager.SharedInstance.GetExistingDatabase (DATABASE_NAME);

			if (_db == null)
			{
				var assembly = Assembly.GetCallingAssembly ();
				var resourceName = assembly.GetManifestResourceNames ().Where (x => x.EndsWith ("couchbase-connect.cblite", StringComparison.CurrentCultureIgnoreCase)).Single ();
				Console.WriteLine ("resourceName: {0}", resourceName);
				var stream = assembly.GetManifestResourceStream (resourceName);
				Console.WriteLine ("Stream length: {0}", stream.Length);
				Manager.SharedInstance.ReplaceDatabase (DATABASE_NAME, stream, null);
				_db = Manager.SharedInstance.GetExistingDatabase (DATABASE_NAME);
			}	
		}

		public bool DoesUserExistOnServer()
		{
			var configDocumentId = "my-local-config";
			var existsKey = "remote-user-exists";

			IDictionary<string, object> dict = _db.GetExistingLocalDocument (configDocumentId);
			if (dict == null)
			{
				return false;
			}

			return dict.ContainsKey (existsKey);
		}

		async Task EnsureCouchbaseUserExists()
		{
			#if (!CB_USER_AUTH_ENABLED)
				Console.WriteLine ("Skipping creation of couchbase user.");
				return;
			#endif

			#if (USE_COUCHBASE_SERVER)
			var userCreationService = "https://demo-mobile.couchbase.com/connect2014/";
			#else
			var userCreationService = "http://cb.t.proxylocal.com/couchbase-connect/";
			#endif
			var configDocumentId = "my-local-config";
			var existsKey = "remote-user-exists";

			IDictionary<string, object> dict = _db.GetExistingLocalDocument (configDocumentId);
			if (dict == null)
			{
				return;
			}

			if (dict.ContainsKey (existsKey))
			{
				Console.WriteLine ("Couchbase user already exists");
				return;
			}

			var userId = GetUserId ();
			var service = TinyIoCContainer.Current.Resolve<ICouchbaseUserService> ();
			if (await service.CreateUser (userCreationService, userId))
			{
				Console.WriteLine ("Couchbase user created");
				dict [existsKey] = true;
				_db.PutLocalDocument (configDocumentId, dict);
			}
		}

		public Database Db
		{
			get
			{
				return _db;
			}
		}

		public void DebugLocalDatabaseInfo ()
		{
			Console.WriteLine ("db.DocumentCount = {0}", _db.DocumentCount);
			var results = _db.CreateAllDocumentsQuery ().Run ();
			foreach (var item in results)
			{
				Console.WriteLine ("item.DocumentId = {0}", item.DocumentId);
			}
		}

		void HandleFirstRunScenario ()
		{
			var configDocumentId = "my-local-config";

			IDictionary<string, object> dict = _db.GetExistingLocalDocument (configDocumentId);
			if (dict == null)
			{
				dict = new Dictionary<string, object> ();
				var userId = Guid.NewGuid ();
				dict.Add ("user-id", userId);
				_db.PutLocalDocument (configDocumentId, dict);
			}
		}

		public string GetUserId ()
		{
			if (_userId == null)
			{
				var configDocumentId = "my-local-config";
				IDictionary<string, object> dict = _db.GetExistingLocalDocument (configDocumentId);
				if (dict == null)
					return null;

				_userId = (string) dict ["user-id"];
			}
			return _userId;
		}

		void SetupViews ()
		{
			var userId = GetUserId ();
			var allContactsView = _db.GetView ("all-contacts");
			allContactsView.SetMapReduce (
				(doc, emit) =>
				{
					//Console.WriteLine("(contacts) Keys: {0}\nValues: {1}", string.Join( ", ", doc.Keys), string.Join( ", ", doc.Values));

					if (!doc.ContainsKey("type")) 
					{
						return;
					}

					if (doc["type"].ToString() == "contact" || doc["type"].ToString() == "contactexchange")
					{
						if (doc["type"].ToString() == "contact" && doc["userId"].ToString() == userId)
						{
//							Console.WriteLine("Skip: {0} {1} {2} {3}", doc["last"].ToString(),
//								doc["first"].ToString(),
//								doc["userId"].ToString(),
//								doc["type"].ToString() == "contact" ? "real" : "proxy");
							return;
						}

//						Console.WriteLine("Emit: {0} {1} {2} {3}", doc["last"].ToString(),
//							doc["first"].ToString(),
//							doc["userId"].ToString(),
//							doc["type"].ToString() == "contact" ? "real" : "proxy");

						emit (
							new string[]
							{
								doc["userId"].ToString(),
								doc["type"].ToString() == "contact" ? "real" : "proxy",
							},
							doc["_id"]);
					}
				}, 
				(keys, values, rereduce) => 
				{
//					Console.WriteLine("(keys, values, rereduce) =>: keys: {0}\nvalues: {1}", 
//						string.Join( ", ", keys), string.Join( ", ", values));

					int i = 0;
					string proxyDocId = null;
					foreach (JArray key in keys) {
						var x = key.ToObject<string[]>();
						if (x[1] == "real")
						{
							return null;
						}
						else
						{
							var valueList = values.ToList();
							proxyDocId = valueList[i].ToString();
						}
						i++;
					}
					return proxyDocId;
				},
				"20");

			var scavengerHuntView = _db.GetView ("scavenger-hunt-list");
			scavengerHuntView.SetMapReduce (
				(doc, emit) =>
				{
					//Console.WriteLine("Keys: {0}\nValues: {1}", string.Join( ", ", doc.Keys), string.Join( ", ", doc.Values));

					if (!doc.ContainsKey("type")) 
					{
						return;
					}

					if (doc["type"].ToString() == "scavengerhuntitem" || doc["type"].ToString() == "scavengerhuntcapture")
					{

						emit (
							new object[]
							{
								doc["type"].ToString() == "scavengerhuntitem" ? doc["_id"] : doc["itemId"],
								doc["type"].ToString(),
								doc["type"].ToString() == "scavengerhuntitem" ? doc["description"] : doc["_id"],
								doc["type"].ToString() == "scavengerhuntitem" ? doc["order"] : null,
							},
							doc["_id"]);
					}
				},
				(keys, values, rereduce) => 
				{
					//Console.WriteLine("(keys, values, rereduce) =>: keys: {0}\nvalues: {1}", 
					//	string.Join( ", ", keys), string.Join( ", ", values));

					//expected
					object itemId = null;
					object description = null;
					object order = null;

					//optional
					object captureId = null;

					int i = 0;
					foreach (JArray key in keys) {

						var x = key.ToObject<object[]>();
						if (x[1].ToString() == "scavengerhuntitem")
						{
							itemId = x[0];
							description = x[2];
							order = x[3];
						}
						else
						{
							captureId = x[2];
						}
						i++;
					}

					var val = new object[] {
						itemId,
						description,
						order,
						captureId
					};

					// Console.WriteLine("reduce out: " + val.ToString());

					return val;
				},
				"8");

		}

		private void SetupReplication(Uri server)
		{
			#if (!CB_SYNC_ENABLED)
			Console.WriteLine ("Sync is DISABLED");
			return;
			#endif

			Console.WriteLine ("Setting up replication");

			var pull = _db.CreatePullReplication (server);
			var push = _db.CreatePushReplication (server);

			pull.Continuous = true;
			push.Continuous = true;

			#if (CB_USER_AUTH_ENABLED)
			var userId = GetUserId ();
			pull.Authenticator = AuthenticatorFactory.CreateBasicAuthenticator (userId, userId);
			push.Authenticator = AuthenticatorFactory.CreateBasicAuthenticator (userId, userId);
			#endif

			pull.Start();
			push.Start();
		}
	}
}
