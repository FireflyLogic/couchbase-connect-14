using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Lite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CouchbaseConnect2014.Extensions;
using CouchbaseConnect2014.Models;
using System.IO;

namespace CouchbaseConnect2014.Services
{
	public interface IRepository
	{
		// Simple get and save methods

		Task<AppConfig> GetAppConfig ();

		// Contact
		ObservableCollection<Contact> GetAllContacts ();

		// Schedule: day -> slot -> *sessionid*
		Task<Schedule> GetSchedule ();

		// Session
		Task<IEnumerable<Session>> GetSessions (IEnumerable<string> sessionIds);
        Session GetSession (string sessionId);

		Task<Contact> GetProfile ();
		void SaveProfile(Contact profile);

		Task<Agenda> GetAgenda ();
		void SaveAgenda (Agenda agenda);

		Speaker GetSpeaker (string speakerId);

		void SaveContactExchange (ContactExchange contactExchange);

		void SaveConferenceSurveyResult (ConferenceSurveyResult result);
		Task<ConferenceSurveyResult>GetConferenceSurveyResult ();

		void SaveSessionRatingResult (SessionRatingResult result);
		Task<SessionRatingResult>GetSessionRatingResult (string sessionId);
        IEnumerable<string> GetTrackFilters ();

        void SaveTrackFilters (IEnumerable<string> selections);

		void SaveContact (Contact contact);

		ObservableCollection<ScavengerHuntView> GetScavengerHuntItems ();
		void SaveScavengerHuntCapture (ScavengerHuntCapture capture);
	}

    public class Repository : IRepository
    {
		ICouchbaseService _couchbaseService;
        const string TrackFiltersKey = "track-filters";

		public Task<AppConfig> GetAppConfig ()
		{
			AppConfig appConfig = null;
			var doc = _couchbaseService.Db.GetExistingDocument ("appconfig");
			if (doc != null)
			{
				appConfig = doc.Properties.ToObject<AppConfig> ();
			}
			return FromResult (appConfig);
		}

		public Task<Contact> GetProfile ()
		{
			var profileKey = GetProfileKey ();
			var doc = _couchbaseService.Db.GetExistingDocument (profileKey);
			Contact contact;
			if (doc == null)
			{
				contact = new Contact {
					First = "",
					Last = "",
					Role = "",
					Company = "",
					Email = "",
					Phone = "",
					Twitter = ""
				};
			}
			else
			{
				contact = doc.Properties.ToObject<Contact> ();
			}

			return FromResult (contact);
		}

		public void SaveProfile (Contact profile)
		{
			var profileKey = GetProfileKey ();
			profile.UserId = _couchbaseService.GetUserId ();
			SaveDocument (profileKey, profile.ToDictionary (), "contact");
		}

		string GetProfileKey ()
		{
			return string.Format ("contact-{0}", _couchbaseService.GetUserId());
		}

		public void SaveContact (Contact contact)
		{
			SaveDocument ("contact-" + contact.UserId, contact.ToDictionary (), "contact");
		}

		void SaveDocument (string docId, IDictionary<string, object> dict, string doctype)
		{
			Console.WriteLine ("Saving [{0}] with Id [{1}]", doctype, docId);
			var doc = _couchbaseService.Db.GetExistingDocument (docId);
			if (doc == null)
			{
				doc = _couchbaseService.Db.GetDocument (docId);
				dict.Add ("type", doctype);
				doc.PutProperties (dict);
			}
			else
			{
				var old = doc.Properties;
				var merged = dict.Concat (old.Where (kvp => !dict.ContainsKey (kvp.Key))).ToDictionary (x => x.Key, x => x.Value);
				doc.PutProperties (merged);
			}
		}

		public Repository (ICouchbaseService couchbaseService)
        {
			_couchbaseService = couchbaseService;
        }

        Task<TResult> FromResult<TResult> (TResult result) {
            return Task.FromResult (result);
        }

		public ObservableCollection<Contact> GetAllContacts ()
		{
			var db = _couchbaseService.Db;

			var allContactsView = db.GetExistingView("all-contacts");
			var q = allContactsView.CreateQuery ();
			var query = q.ToLiveQuery ();
			query.GroupLevel = 1;

			Func<QueryRow, Contact> transform = row => {

				var k = row.Key as JArray;
				var key = k.ToObject<string[]> ();

				Contact contact;

				if (row.Value == null)
				{
					var docId = "contact-" + key[0].ToString();
					var doc = _couchbaseService.Db.GetExistingDocument (docId);
					if (doc == null)
						return null;
					contact = doc.Properties.ToObject<Contact> ();
				}
				else
				{
					var doc = _couchbaseService.Db.GetExistingDocument (row.Value.ToString());
					if (doc == null)
						return null;
					contact = doc.Properties.ToObject<ContactProxy> ();
				}

				return contact;
			};

			return new LiveQueryObservableCollection<Contact> (query, transform);
		}



		public Task<Schedule> GetSchedule ()
		{
			var schedule = GetExistingDocumentAsObject<Schedule>("schedule", "schedule");

			return FromResult(schedule);
		}

		public Task<IEnumerable<Session>> GetSessions (IEnumerable<string> sessionIds)
		{
			var sessions = new List<Session> ();

			foreach (var sessionId in sessionIds)
			{
                var session = GetSession (sessionId);
				sessions.Add (session);
			}

            return Task.FromResult((IEnumerable<Session>)sessions);
		}

        public Session GetSession (string sessionId)
        {
            return GetExistingDocumentAsObject<Session> (sessionId, "session");
        }

		public Speaker GetSpeaker (string speakerId)
		{
			var speaker = GetExistingDocumentAsObject<Speaker> (speakerId, "speaker");
			return speaker;
		}

		string GetAgendaKey ()
		{
			return string.Format ("agenda-{0}", _couchbaseService.GetUserId());
		}

        public Task<Agenda> GetAgenda ()
        {
			var agendaKey = GetAgendaKey ();
			var agenda = GetExistingDocumentAsObject<Agenda> (agendaKey, "agenda");
			if (agenda == null)
			{
				agenda = new Agenda ();
			}
			return FromResult(agenda);
        }

		public void SaveAgenda (Agenda agenda)
		{
			var agendaKey = GetAgendaKey ();
			var doc = _couchbaseService.Db.GetExistingDocument (agendaKey);

			IDictionary<string, object> dict;
			if (doc == null)
			{
				doc = _couchbaseService.Db.GetDocument (agendaKey);
				dict = new Dictionary<string, object> ();
				dict.Add ("type", "agenda");
				dict.Add ("agenda", agenda);
			}
			else
			{
				dict = doc.Properties;
				dict["agenda"] = agenda;
			}

			doc.PutProperties (dict);
		}

		public void SaveContactExchange (ContactExchange contactExchange)
		{
			var key = "contactexchange-" + Guid.NewGuid ().ToString ();
			SaveDocument (key, contactExchange.ToDictionary (), "contactexchange");
		}

		public void SaveConferenceSurveyResult (ConferenceSurveyResult result)
		{
			var key = "surveyresult-" + _couchbaseService.GetUserId ();
			var doc = _couchbaseService.Db.GetExistingDocument (key);

			IDictionary<string, object> dict;
			if (doc == null)
			{
				doc = _couchbaseService.Db.GetDocument (key);
				dict = new Dictionary<string, object> ();
				dict.Add ("type", "surveyresult");
				dict.Add ("conferencesurveyresult", result);
			}
			else
			{
				dict = doc.Properties;
				dict["conferencesurveyresult"] = result;
			}

			doc.PutProperties (dict);
		}

		public Task<ConferenceSurveyResult>GetConferenceSurveyResult ()
		{
			var key = "surveyresult-" + _couchbaseService.GetUserId ();
			var result = GetExistingDocumentAsObject<ConferenceSurveyResult> (key, "conferencesurveyresult");

			return FromResult(result);
		}

		public void SaveSessionRatingResult (SessionRatingResult result)
		{
			var key = "sessionrating-" + result.SessionId + "-" + _couchbaseService.GetUserId ();
			var doc = _couchbaseService.Db.GetExistingDocument (key);

			IDictionary<string, object> dict;
			if (doc == null)
			{
				doc = _couchbaseService.Db.GetDocument (key);
				dict = new Dictionary<string, object> ();
				dict.Add ("type", "ratingresult");
				dict.Add ("sessionratingresult", result);
			}
			else
			{
				dict = doc.Properties;
				dict["sessionratingresult"] = result;
			}

			doc.PutProperties (dict);
		}

		public Task<SessionRatingResult>GetSessionRatingResult (string sessionId)
		{
			var key = "sessionrating-" + sessionId + "-" + _couchbaseService.GetUserId ();
			var result = GetExistingDocumentAsObject<SessionRatingResult> (key, "sessionratingresult");

			return FromResult(result);
		}
        public IEnumerable<string> GetTrackFilters ()
        {
            var props = _couchbaseService.Db.GetExistingLocalDocument (TrackFiltersKey);
            return props == null 
                ? new List<string> () 
                : GetTypedObject<IEnumerable<string>> (props ["tracks"]);
        }

        public void SaveTrackFilters (IEnumerable<string> selections)
        {
            var props = new Dictionary<string, object> {
                { "type", "track-filters" },
                { "tracks", selections.ToList () }
            };

            _couchbaseService.Db.PutLocalDocument (TrackFiltersKey, props);
        }

		T GetExistingDocumentAsObject<T> (string documentId, string propertyName) where T : class
		{
			var doc = _couchbaseService.Db.GetExistingDocument (documentId);
            return GetDocumentProperty<T> (doc, propertyName);
		}

        T GetDocumentProperty<T> (Document doc, string propertyName) where T : class
        {
            return doc == null ? null : GetTypedObject<T> (doc.GetProperty (propertyName));
        }

		T GetTypedObject<T>(object obj) 
		{
			return obj is T ? (T)obj : JsonConvert.DeserializeObject<T> (obj.ToString ());
		}

		byte [] GetBytes(object obj)
		{
			if (obj == null)
				return null;

			if (obj is byte[])
				return obj as byte[];

			if (obj is string)
				return Convert.FromBase64String(obj.ToString());

			return null;
		}

		public ObservableCollection<ScavengerHuntView> GetScavengerHuntItems ()
		{
			var db = _couchbaseService.Db;

			var shList = db.GetExistingView("scavenger-hunt-list");
			var q = shList.CreateQuery ();
			var query = q.ToLiveQuery ();
			query.GroupLevel = 1;

			Func<QueryRow, ScavengerHuntView> transform = row => {

				var k = row.Key as JArray;
				var key = k.ToObject<string[]> ();

				var v = row.Value as object[];
				var itemId = key[0].ToString();
				var itemDescription = v[1].ToString();
				var order = (int) ((long) v[2]);
				var captureDocId = (v[3] == null) ? null : v[3].ToString();

				var itemDoc = _couchbaseService.Db.GetExistingDocument (itemId);
				var itemImage = GetBytes(itemDoc.Properties["image"]);

				byte[] captureImage = null;

				if (String.IsNullOrWhiteSpace(captureDocId) == false)
				{
					Document captureDoc = _couchbaseService.Db.GetExistingDocument (captureDocId);
				
					if (captureDoc != null)
					{
						var imageData = captureDoc.Properties["image"];
						if (imageData != null)
						{
							captureImage = GetBytes(imageData);
						}
					}
				}

				var item = new ScavengerHuntView()
				{
					ItemId = itemId,
					ItemDescription = itemDescription,
					Order = order,
					ItemImage = itemImage,
					CaptureImage = captureImage
				};

				return item;
			};

			var result = new LiveQueryObservableCollection<ScavengerHuntView> (query, transform);
			return result;
		}

		public void SaveScavengerHuntCapture (ScavengerHuntCapture capture)
		{
			var key = capture.ItemId + "-" + _couchbaseService.GetUserId ();
			var dict = capture.ToDictionary ();
			SaveDocument (key, dict, "scavengerhuntcapture");
		}
	
	}
}
