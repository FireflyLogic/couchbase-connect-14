using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ViewModels
{
    public class AgendaViewModel : BaseViewModel
    {
        readonly IRepository _repository;
        readonly ITimeService _timeService;
		public bool _initalized = false;
		public bool _fromSession = false;
		public bool _fromFullSchedule = false;

        public AgendaViewModel (IRepository repository, ITimeService timeService)
        {
            _repository = repository;
            _timeService = timeService;
            AgendaDays = new List<AgendaDayViewModel> ();
        }

		internal async override Task Initialize (params object[] args)
		{
			var scheduleTask = _repository.GetSchedule ();
			var agendaTask = _repository.GetAgenda ();

			Days = (await scheduleTask).Days;
			Agenda = await agendaTask;

			// the hack of all hacks!
			// to actually display the agenda list the 
			// 2nd time you go to My Agenda on Android
			if (Device.OS == TargetPlatform.Android) {
			
				if (_fromSession || _fromFullSchedule)
					_initalized = true;
				else
					_initalized = false;

				_fromFullSchedule = false;
				_fromSession = false;

				if (!_initalized)
					AgendaDays.Clear ();

				_initalized = true;
			}

			AgendaDays = CreateAgendaDays (Days, Agenda).ToList();

		}

        IEnumerable<Day> _days;
        internal IEnumerable<Day> Days {
            get {
                return _days;
            }
            set {
                _days = value;
            }
        }

        Agenda _agenda;
        public Agenda Agenda {
            get {
                return _agenda;
            }
            set {
                _agenda = value;
            }
        }

		public IEnumerable<AgendaDayViewModel> CreateAgendaDays (IEnumerable<Day> days, Agenda agenda)
        {
            var sessionIds = days
                .SelectMany (day => day.Slots)
                .SelectMany (slot => slot.SessionIds)
                .ToList ();

			var sessions = _repository.GetSessions (sessionIds).Result;

			var agendaDays = days.Select (day => new AgendaDayViewModel {
                Date = day.Date,
                Slots = day.Slots.Select(slot => new {
                    Slot = slot,
                    Session = SessionForSlot (slot, agenda, sessions)
                })
                    .Select (slotSession => new AgendaCellViewModel (slotSession.Slot) {
                        Title = slotSession.Session != null ? slotSession.Session.Title : "None",
                        Location = slotSession.Session != null ? slotSession.Session.Location : "None",
                        Time = slotSession.Slot.StartTime,
                        IsBooked = slotSession.Session != null,
						Track = slotSession.Session == null ? "None" :
							!string.IsNullOrEmpty(slotSession.Session.Track) ? slotSession.Session.Track :
							string.IsNullOrEmpty(slotSession.Session.Title) ? "None" :
							// check title for registration
							slotSession.Session.Title == "Registration" ? slotSession.Session.Title :
							// check title for any breaks
							slotSession.Session.Title == "Break" ||
							slotSession.Session.Title == "Breakfast" ||
							slotSession.Session.Title == "Lunch" ||
							slotSession.Session.Title == "Afternoon Break" ||
							slotSession.Session.Title == "Party" ? "MealBreak" : "NoTrack"
                    })
            });

			return agendaDays;
        }

        static Session SessionForSlot (Slot slot, Agenda agenda, IEnumerable<Session> sessions)
      	{
			//Console.WriteLine ("Slot: " + slot.StartTime + " Agenda: " + agenda.Id);
            return !slot.SessionIds.Skip (1).Any () 
                ? sessions.Single (session => session.Id == slot.SessionIds.First ()) 
                : sessions.FirstOrDefault (session => agenda.IsSelected (slot.StartTime, session.Id));
        }

        IList<AgendaDayViewModel> _agendaDays;
        public IList<AgendaDayViewModel> AgendaDays {
            get {
                return _agendaDays;
            }
            set {
                var selectedIndex = -1;
                if (_agendaDays != null) {
                    selectedIndex = AgendaDays.IndexOf (SelectedDay);
                }

                SetObservableProperty (ref _agendaDays, value);

                if (selectedIndex >= 0)
                    SelectedDay = AgendaDays [selectedIndex];
                else {
                    SetSelectedDayToCurrentDay ();
                }
            }
        }

        async void SetSelectedDayToCurrentDay ()
        {
            var currentDay = FindCurrentDay ();
            // TODO : Undo this workaround when initialization bug is fixed
            SelectedDay = AgendaDays.SkipWhile (d => d == currentDay).FirstOrDefault ();
            await Task.Delay (100);
            SelectedDay = currentDay;
        }

        AgendaDayViewModel FindCurrentDay ()
        {
            var notPast = AgendaDays.SkipWhile (d => d.Date < _timeService.Today);
            return notPast.Any () ? notPast.First () : AgendaDays.LastOrDefault ();
        }

        AgendaDayViewModel _selectedDay;
        public AgendaDayViewModel SelectedDay {
            get {
                return _selectedDay;
            }
            set {
                SetObservableProperty (ref _selectedDay, value);
                if (_selectedDay != null) Slots = _selectedDay.Slots.ToList ();
            }
        }

        IList<AgendaCellViewModel> _slots;
        public IList<AgendaCellViewModel> Slots {
            get {
                return _slots;
            }
            set {
                SetObservableProperty (ref _slots, value);
            }
        }

        AgendaCellViewModel _selectedSlot;
        public AgendaCellViewModel SelectedSlot {
            get {
                return _selectedSlot;
            }
            set {
                _selectedSlot = value;
                if (_selectedSlot != null) {
                    var slot = _selectedSlot.Slot;
					
                    var sessionId = slot.SessionIds
                        .FirstOrDefault (x => Agenda.IsSelected (slot.StartTime, x))
                        ?? slot.SessionIds.First ();

					_fromSession = true;

                    Navigation.PushAsync (new ChooseSessionView (
						slot,
						_repository.GetSession (sessionId)
					));
                }
            }
        }

        public ICommand ShowFullSchedule
        {
            get {
				return new Command (() => {
					_fromFullSchedule = true;
					Navigation.PushAsync(new FullScheduleView());
				});
            }
        }
    }
}
