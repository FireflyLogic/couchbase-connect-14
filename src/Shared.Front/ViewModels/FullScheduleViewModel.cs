using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;

namespace CouchbaseConnect2014.ViewModels
{
	public class FullScheduleViewModel : BaseViewModel
	{
		IRepository _repository;
		bool _hasInitialized = false;

        public FullScheduleViewModel (IRepository repository)
        {
            _repository = repository;
            FullScheduleDays = new List<FullScheduleDayViewModel> ();
        }

		internal override async Task Initialize (params object[] args)
        {
            await base.Initialize ();

			//if (_hasInitialized == false)
			//{ 
				var scheduleTask = _repository.GetSchedule ();
	            var agendaTask = _repository.GetAgenda ();
				var filtersTask = _repository.GetTrackFilters ().ToList ();
				TrackFilters = filtersTask;
				Schedule = await scheduleTask;
	            Agenda = await agendaTask;
			//}
			//_hasInitialized = true;
        }

        IList<string> _trackFilters;
        internal IList<string> TrackFilters {
            get {
                return _trackFilters;
            }
            set {
                _trackFilters = value;
                if (_trackFilters == null)
                    return;
                FilterIcon = string.Format ("filter_{0}.png", _trackFilters.Count);
                UpdateFullScheduleDays ();
            }
        }

        string _filterIcon;
        public string FilterIcon {
            get {
                return _filterIcon;
            }
            set {
                SetObservableProperty (ref _filterIcon, value);
            }
        }

        Schedule _schedule;
        internal Schedule Schedule {
            get {
                return _schedule;
            }
            set {
                _schedule = value;
                UpdateFullScheduleDays ();
            }
        }

        Agenda _agenda;
        internal Agenda Agenda {
            get {
                return _agenda;
            }
            set {
                _agenda = value;
                UpdateFullScheduleDays ();
            }
        }

        void UpdateFullScheduleDays ()
        {
            if (Schedule == null || Agenda == null || TrackFilters == null) return;
            FullScheduleDays = Schedule.Days
                .Select (day => new FullScheduleDayViewModel (_repository, day, Agenda, TrackFilters))
                .ToList ();
        }

        IList<FullScheduleDayViewModel> _fullScheduleDays;
        public IList<FullScheduleDayViewModel> FullScheduleDays {
            get {
                return _fullScheduleDays;
            }
            set {
                var selectedIndex = -1;

                if (_fullScheduleDays != null && _fullScheduleDays.Any ()) {
                    selectedIndex = _fullScheduleDays.IndexOf (SelectedDay);
                }

                SetObservableProperty (ref _fullScheduleDays, value);

                if (selectedIndex >= 0)
                    SelectedDay = FullScheduleDays [selectedIndex];
                else
                    SelectedDay = FullScheduleDays.FirstOrDefault ();
            }
        }

        FullScheduleDayViewModel _selectedDay;
        public FullScheduleDayViewModel SelectedDay {
            get {
                return _selectedDay;
            }
            set {
                SetObservableProperty (ref _selectedDay, value);
                Slots = _selectedDay == null ? null : _selectedDay.Slots;
            }
        }

        IList<FullScheduleSessionGroup> _slots;
        public IList<FullScheduleSessionGroup> Slots {
            get {
                return _slots;
            }
            set {
                SetObservableProperty (ref _slots, value);
            }
        }

        public ICommand ShowFilters {
            get {
                return new Command (() => Navigation.PushAsync (new FiltersView ()));
            }
        }

        FullScheduleCellViewModel _selectedSession;
        public FullScheduleCellViewModel SelectedSession {
            get {
                return _selectedSession;
            }
            set {
                _selectedSession = value;
                if (_selectedSession != null) {
                    var selectedSlot = SelectedDay.Slots.First (slot => slot.Contains (_selectedSession));
                    Navigation.PushAsync (new ChooseSessionView (
                        selectedSlot.Slot, _selectedSession.Session));
                }
            }
        }
	}
}

