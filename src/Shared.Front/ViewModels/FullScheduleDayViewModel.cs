using System;
using CouchbaseConnect2014.Models;
using System.Linq;
using System.Collections.Generic;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.ViewModels
{
	public class FullScheduleDayViewModel: BaseViewModel
	{
		readonly IRepository _repository;
        readonly IEnumerable<string> _trackFilters;

		public FullScheduleDayViewModel (IRepository repository, Day day, Agenda agenda,
            IEnumerable<string> trackFilters)
        {
            this._trackFilters = trackFilters;
			this._repository = repository;
            Date = day.Date;
            Slots = day.Slots
                .Select (slot => CreateFullScheduleSessionGroup (slot, agenda)).ToList ();
        }

        public DateTime Date {
            get;
            set;
        }

        FullScheduleSessionGroup CreateFullScheduleSessionGroup (Slot slot, Agenda agenda) {
            var group = new FullScheduleSessionGroup (slot);
            group.StartTime = slot.StartTime;

            var sessions = slot.SessionIds.Select (sessionId => _repository.GetSession (sessionId));

            if (_trackFilters.Any ())
                sessions = sessions.Where (session => IsInFilter (session));

			group.AddRange (sessions.Select (session => 
				new FullScheduleCellViewModel (
                    _repository, session, agenda, slot, _trackFilters)));

            return group;
        }

        bool IsInFilter (Session session)
        {
            return string.IsNullOrWhiteSpace (session.Track) || 
                _trackFilters.Contains (session.Track);
        }

        public IList<FullScheduleSessionGroup> Slots {
            get;
            set;
        }
	}
}

