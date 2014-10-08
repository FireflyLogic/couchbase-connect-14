using System;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CouchbaseConnect2014.Tests.Mocks
{
	public class MockRepository : IRepository
    {
        public MockRepository ()
        {
            GetSessionsResults = new Dictionary<IEnumerable<string>, IEnumerable<Session>> ();
        }

		public Task<AppConfig> GetAppConfig ()
		{
            return Task.FromResult (GetAppConfigResult);
		}

		public void SaveContactExchange (ContactExchange contactExchange)
		{
			throw new NotImplementedException ();
		}

		public void SaveContact (Contact contact)
		{
			throw new NotImplementedException ();
		}

		public void SaveProfile (Contact profile)
		{
			throw new NotImplementedException ();
		}

		public Task<IEnumerable<Session>> GetSessions (IEnumerable<string> sessionIds)
		{
            return Task.FromResult (GetSessionsResults [sessionIds]);
		}

        public Session GetSession (string sessionId)
        {
            throw new NotImplementedException ();
        }

        public IEnumerable<string> GetTrackFilters ()
        {
            return GetTrackFiltersResult;
        }

        public void SaveTrackFilters (IEnumerable<string> selections)
        {
            throw new NotImplementedException ();
        }

        public IList<Day> GetDaysResult {
            get;
            set;
        }

        public Schedule GetScheduleResult {
            get;
            set;
        }

        public IEnumerable<string> GetTrackFiltersResult {
            get;
            set;
        }

        public AppConfig GetAppConfigResult {
            get;
            set;
        }

		public ObservableCollection<Contact> GetAllContacts ()
		{
			throw new NotImplementedException ();
		}

		public Speaker GetSpeaker (string speakerId)
		{
			throw new NotImplementedException ();
		}

		public void SaveConferenceSurveyResult (ConferenceSurveyResult result)
		{
			throw new NotImplementedException ();
		}

		public Task<ConferenceSurveyResult> GetConferenceSurveyResult ()
		{
			throw new NotImplementedException ();
		}

		public void SaveSessionRatingResult (SessionRatingResult result)
		{
			throw new NotImplementedException ();
		}

		public Task<SessionRatingResult> GetSessionRatingResult (string sessionId)
		{
			throw new NotImplementedException ();
		}

        public Agenda GetAgendaResult {
            get;
            set;
        }

        public Contact GetProfileResult {
            get;
            set;
        }

		public ObservableCollection<Contact> GetContactsResult {
			get;
			set;
		}

        public IDictionary<IEnumerable<string>, IEnumerable<Session>> GetSessionsResults {
            get;
            set;
        }

        public Task<Agenda> GetAgenda ()
        {
            return Task.FromResult<Agenda> (GetAgendaResult);
        }

		public void SaveAgenda (Agenda agenda)
		{
			throw new NotImplementedException ();
		}

        public Task<Contact> GetProfile ()
        {
            return Task.FromResult<Contact> (GetProfileResult);
        }

//		public ObservableCollection<Contact> GetContacts() {
//			return new ObservableCollection<Contact> (GetContactsResult);
//		}

		public Task<Schedule> GetSchedule ()
		{
            return Task.FromResult (GetScheduleResult);
		}

		public ObservableCollection<ScavengerHuntView> GetScavengerHuntItems ()
		{
			throw new NotImplementedException ();
		}

		public void SaveScavengerHuntCapture (ScavengerHuntCapture capture)
		{
			throw new NotImplementedException ();
		}
    }
}

