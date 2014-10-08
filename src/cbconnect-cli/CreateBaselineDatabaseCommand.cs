using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using Couchbase.Lite;
using System.Linq;
using System.Threading;
using System.IO;

namespace cbconnectcli
{
	public class CreateBaselineDatabaseCommand : ICommand
	{
		const string TARGET_DB_NAME = "couchbase-connect";

		public CreateBaselineDatabaseCommand ()
		{
		}

		public void Run ()
		{
			// Delete any old databases
			DeleteDatabase (TARGET_DB_NAME);

			Console.WriteLine ("Read the tab-separated-value data into data structures.");

			List<Session> sessions;
			Schedule schedule;
			List<Speaker> speakers;
			GetData (out sessions, out schedule, out speakers);

			Console.WriteLine ("Unpacked day, session, and speaker data.");

			Console.WriteLine ("Creating a new local couchbase lite database (couchbase-connect-baseline)");
			CreateDatabase (TARGET_DB_NAME);

			Console.WriteLine ("Save the data to the couchbase-connect-baseline database");
			SaveSchedule (schedule);
			SaveSessions (sessions);
			SaveSpeakers (speakers);

			SaveAppConfig ();

			CopyDatabaseToResourceDirectory ("iOS");
			CopyDatabaseToResourceDirectory ("Android");
		}

		static void CopyDatabaseToResourceDirectory (string appDirectory)
		{
			var sourceDbFilename = Path.Combine (Manager.SharedInstance.Directory, TARGET_DB_NAME + ".cblite");
			var location = System.Reflection.Assembly.GetExecutingAssembly ().Location;
			var destDbFilename = Path.Combine (location.Substring (0, location.LastIndexOf ("src/")), 
				"src/" + appDirectory + "/Resources/" + TARGET_DB_NAME + ".cblite");

			File.Delete (destDbFilename);
			File.Copy (sourceDbFilename, destDbFilename);

			Console.WriteLine ("Copied new database to : [{0}]", destDbFilename);
		}

		static void DeleteDatabase (string dbName)
		{

			if (Manager.SharedInstance.GetExistingDatabase (dbName) != null)
			{
				Manager.SharedInstance.GetDatabase (dbName).Delete ();
				Console.WriteLine ("Deleted database [{0}].", dbName);
				Thread.Sleep (500);
			}
		}

		void CreateDatabase (string dbName)
		{
			var db = Manager.SharedInstance.GetDatabase (dbName);

			Console.WriteLine ("Created database [{0}] at [{1}].", dbName, db.ToString ());

			Thread.Sleep (500);
		}


		static Database GetTargetDatabase ()
		{
			Thread.Sleep (500);
			return Manager.SharedInstance.GetExistingDatabase (TARGET_DB_NAME);
		}

		static void GetData (out List<Session> sessions, out Schedule schedule, out List<Speaker> speakers)
		{
			var rawData = RawScheduleData.GetData();

			var lines = rawData.Split (new char[] {'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);

			var cells = lines.Select (x => x.Split (new char[] {'\t'}, StringSplitOptions.None));

			// SessionId	Date	StartTime	EndTime	Name	Abstract	Track	Presenter1	Company1	Position1	Presenter2	Company2	Position2	Presenter3	Company3	Position3
			var rawSessions = cells.Select (x => new {
				Id = "session-" + x [0],
				Date = DateTime.Parse (x [1]),
				StartTime = DateTime.Parse (x [1] + " " + x [2]),
				EndTime = DateTime.Parse (x [1] + " " + x [3]),
				Name = x [4],
				Abstract = x[5],
				Track = x [6],
				Presenter1Name = x [7],
				Presenter1Company = x [8],
				Presenter1Role = x [9],
				Presenter1Headshot = x [10],
				Presenter2Name = x [11],
				Presenter2Company = x [12],
				Presenter2Role = x [13],
				Presenter3Name = x [14],
				Presenter3Company = x [15],
				Presenter3Role = x [16],
			}).ToList ();

			var rawSpeakers = 
				rawSessions.Select (x => new SpeakerRaw {
					Name = x.Presenter1Name,
					Company = x.Presenter1Company,
					Role = x.Presenter1Role,
					HeadshotUrl = x.Presenter1Headshot
				})
					.Concat (rawSessions.Select (x => new SpeakerRaw {
						Name = x.Presenter2Name,
						Company = x.Presenter2Company,
						Role = x.Presenter2Role,
						HeadshotUrl = x.Presenter1Headshot
				}))
					.Concat (rawSessions.Select (x => new SpeakerRaw {
						Name = x.Presenter2Name,
						Company = x.Presenter2Company,
						Role = x.Presenter2Role,
						HeadshotUrl = x.Presenter1Headshot
					}))
					.GroupBy(x => x.Name)
					.Select(x => x.FirstOrDefault())
					.Where (x => String.IsNullOrWhiteSpace (x.Name) == false);
			
			Func<string, string> speakerNameToId = x => String.IsNullOrWhiteSpace(x) ? "" : "speaker-" + x.ToLower ().Replace (" ", "-");

			speakers = rawSpeakers
				.Select (x => new Speaker {
					Id = speakerNameToId (x.Name),
					First = x.Name.Trim ().Substring (0, x.Name.Trim ().LastIndexOf (" ")),
					Last = x.Name.Trim ().Substring (x.Name.Trim ().LastIndexOf (" ") + 1),
					Company = x.Company,
					Role = x.Role,
					HeadshotUrl = x.HeadshotUrl
				}).ToList ();

			var sessionSpeakers = rawSessions.Select (x => new SessionSpeaker {
				SessionId = x.Id,
				SpeakerId = speakerNameToId (x.Presenter1Name)
			}).Concat (rawSessions.Select (x => new SessionSpeaker {
				SessionId = x.Id,
				SpeakerId = speakerNameToId (x.Presenter2Name)
			})).Concat (rawSessions.Select (x => new SessionSpeaker {
				SessionId = x.Id,
				SpeakerId = speakerNameToId (x.Presenter3Name)
			})).Distinct ()
				.Where (x => String.IsNullOrWhiteSpace (x.SpeakerId) == false)
				.ToList ();

			sessions = rawSessions.Select( rs =>
				new Session {
					Id = rs.Id,
					Time = rs.StartTime,
					Title = rs.Name,
					Track = rs.Track,
					Abstract = rs.Abstract.Replace("<br>", Environment.NewLine),
					Location = "TBA"
				}).ToList ();

			var groupedSessionSpeakers = sessionSpeakers.GroupBy (x => x.SessionId, y => y.SpeakerId).ToList();

			foreach (var gs in groupedSessionSpeakers)
			{
				sessions.Where (x => x.Id == gs.Key).Single ().SpeakerIds = gs.ToList();
			}

			var groupedSessionIds = sessions.GroupBy (x => x.Time, y => y.Id).ToList();

			var timeSlots = rawSessions
				.Select (x => new StartAndEnd
					{ 
						StartTime = x.StartTime, 
						EndTime = x.EndTime
					}).Distinct()
				.ToList();

			var slots = timeSlots
				.Join (groupedSessionIds, s => s.StartTime, gs => gs.Key, (s, gs) => new Slot {
					StartTime = s.StartTime,
					EndTime = s.EndTime,
					SessionIds = gs.ToList ()
				}).ToList ();

			var groupedSlots = slots.GroupBy (x => x.StartTime.Date).ToList();

			var days = groupedSlots.Select (x => new Day {
				Date = x.Key,
				Slots = x.ToList ()
			}).ToList();

			schedule = new Schedule{ Days = days };
		}

		private struct SessionSpeaker
		{
			public string SessionId;
			public string SpeakerId;
		}

		private struct SpeakerRaw
		{
			public string Name;
			public string Company;
			public string Role;
			public string HeadshotUrl;
		}

		private struct StartAndEnd
		{
			public DateTime StartTime { get; set;}
			public DateTime EndTime { get; set;}
		}

		void SaveSchedule (Schedule schedule)
		{
			var db = GetTargetDatabase ();
			var scheduleDoc = db.GetExistingDocument("schedule");
			if (scheduleDoc == null)
			{
				scheduleDoc = db.GetDocument ("schedule");
			}
			var dict = new Dictionary<string, object> ();
			dict.Add ("type", "schedule");
			dict.Add ("schedule", schedule);

			scheduleDoc.PutProperties (dict);
			Console.WriteLine ("Saving {0}", scheduleDoc.Id);

			Thread.Sleep (100);
		}

		void SaveSessions (List<Session> sessions)
		{
			var db = GetTargetDatabase ();

			foreach (var item in sessions) 
			{
				var doc = db.GetExistingDocument(item.Id);
				if (doc == null)
				{
					doc = db.GetDocument (item.Id);
				}
				var dict = new Dictionary<string, object> ();
				dict.Add ("session", item);
				dict.Add ("type", "session");
				doc.PutProperties (dict);
				Console.WriteLine ("Saving {0}", item.Id);
				Thread.Sleep (100);
			}
		}

		void SaveSpeakers (List<Speaker> speakers)
		{
			var db = GetTargetDatabase ();

			foreach (var item in speakers)
			{
				var doc = db.GetExistingDocument (item.Id);
				if (doc == null)
				{
					doc = db.GetDocument (item.Id);
				}
				var dict = new Dictionary<string, object> ();
				dict.Add ("speaker",item);
				dict.Add ("type", "speaker");
				doc.PutProperties (dict);
				Console.WriteLine ("Saving {0}", item.Id);
				Thread.Sleep (100);
			}
		}

		void SaveScavengerHuntItems ()
		{
			IEnumerable<ScavengerHuntItem> items = GetScavengerHuntItems ();

			var db = GetTargetDatabase ();

			foreach (var item in items)
			{
				var doc = db.GetExistingDocument(item.Id);
				if (doc == null)
				{
					doc = db.GetDocument (item.Id);
				}
				var dict = new Dictionary<string, object> ();
				dict.Add ("type", "scavengerhuntitem");
				dict.Add ("description", item.Description);
				dict.Add ("image", item.Image);
				dict.Add ("order", item.Order);

				doc.PutProperties (dict);
				Console.WriteLine ("Saving {0}", item.Id);

				Thread.Sleep (100);
			}
		}

		IEnumerable<ScavengerHuntItem> GetScavengerHuntItems ()
		{
			var items = new List<ScavengerHuntItem> (
				new [] {
					new ScavengerHuntItem {
						Order = 10,
						Id = "scavenger-hunt-item-001",
						Description = "Picture with Bob",
						Image = GetImage ("with-bob"),
					}, new ScavengerHuntItem {
						Order = 20,
						Id = "scavenger-hunt-item-002",
						Description = "Picture on the Red Couch",
						Image = GetImage ("red-couch"),
					}, new ScavengerHuntItem {
						Order = 30,
						Id = "scavenger-hunt-item-003",
						Description = "Picture with the Firefly Crew",
						Image = GetImage ("firefly-crew"),
					}, new ScavengerHuntItem {
						Order = 40,
						Id = "scavenger-hunt-item-004",
						Description = "Picture with a Connect Sponsor",
						Image = GetImage ("connect-sponsor"),
					}, new ScavengerHuntItem {
						Order = 50,
						Id = "scavenger-hunt-item-005",
						Description = "Picture at a Genius Bar",
						Image = GetImage ("genius-bar"),
					}, new ScavengerHuntItem {
						Order = 60,
						Id = "scavenger-hunt-item-006",
						Description = "Picture with a Couchbase Customer",
						Image = GetImage ("couchbase-customer"),
					}, new ScavengerHuntItem {
						Order = 70,
						Id = "scavenger-hunt-item-007",
						Description = "Picture with Your Couchbase Bag",
						Image = GetImage ("couchbase-bag"),
					}, new ScavengerHuntItem {
						Order = 80,
						Id = "scavenger-hunt-item-008",
						Description = "Picture in the Couchbase Booth",
						Image = GetImage ("couchbase-booth"),
					}, new ScavengerHuntItem {
						Order = 90,
						Id = "scavenger-hunt-item-009",
						Description = "Selfie",
						Image = GetImage ("selfie"),
					}
				});

			return items;
		}

		private byte[] GetImage(string name)
		{
			return new byte[0];
		}

		void SaveAppConfig ()
		{
			var db = GetTargetDatabase ();
			var itemId = "appconfig";
			var doc = db.GetExistingDocument (itemId);
			if (doc == null)
			{
				doc = db.GetDocument (itemId);
			}
			var dict = new Dictionary<string, object> ();
			dict.Add ("twitterAccessToken", "AAAAAAAAAAAAAAAAAAAAAI8caQAAAAAA11sGE7k7NbRrgYcuHkDCFtxmvas%3DlOBjwbru32uLUHiVybiRu9WFpp69R52jWXnhqIAuXdLg0RORPX");
			dict.Add ("twitterListName", "couchbase-connect-2014");
			dict.Add ("twitterListOwnerScreenName", "aobendorf");
			dict.Add ("type", "appconfig");
			doc.PutProperties (dict);
			Console.WriteLine ("Saving {0}", itemId);
			Thread.Sleep (100);
		}

//
//		private static void StartPushReplication()
//		{
//			var db = GetBaselineDatabase ();
//			var push = db.CreatePushReplication (new Uri(SYNC_SERVER_ADDRESS));
//			push.Continuous = true;
//			push.Start();
//
//			var pull = db.CreatePullReplication (new Uri(SYNC_SERVER_ADDRESS));
//			pull.Continuous = true;
//			pull.Start();
//
//			Thread.Sleep (500);
//		}
//
//		private static void StartPullReplication()
//		{
//			var db = GetTargetDatabase ();
//
//			var pull = db.CreatePullReplication (new Uri(SYNC_SERVER_ADDRESS));
//			pull.Continuous = true;
//			pull.Start();
//
//			var push = db.CreatePushReplication (new Uri(SYNC_SERVER_ADDRESS));
//			push.Continuous = true;
//			push.Start();
//
//			Thread.Sleep (500);
//		}
//
//		static Database GetBaselineDatabase ()
//		{
//			Thread.Sleep (500);
//			return Manager.SharedInstance.GetExistingDatabase (BASELINE_DB_NAME);
//
//		}
//
//

	}
}

