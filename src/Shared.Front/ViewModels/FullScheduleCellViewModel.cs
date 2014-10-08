using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.ViewModels;
using System.Linq;
using CouchbaseConnect2014.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ViewModels
{
    public class FullScheduleCellViewModel : BaseViewModel
	{
		IRepository _repository;
		Agenda _agenda;
		Slot _slot;

		public FullScheduleCellViewModel (
            IRepository repository, Session session, Agenda agenda, Slot slot,
            IEnumerable<string> trackFilters
        )
        {
			this._repository = repository;
			this._slot = slot;
			this._agenda = agenda;
            Session = session;
            Title = session.Title;
            Location = session.Location;
//            Track = session.Track;
			IsOptional = slot.SessionIds.Skip(1).Any();
			IsSelected = IsOptional && agenda.IsSelected (slot.StartTime, session.Id);

			Track = !string.IsNullOrEmpty (session.Track) ? session.Track :
				string.IsNullOrEmpty (session.Title) ? "None" :
			// check title for registration
				session.Title == "Registration" ? session.Title :
			// check title for any breaks
				session.Title == "Break" ||
			session.Title == "Breakfast" ||
			session.Title == "Lunch" ||
			session.Title == "Afternoon Break" ||
			session.Title == "Party" ? "MealBreak" : "NoTrack";
        }

        public Session Session {
            get;
            private set;
        }

        public string Title {
            get;
            set;
        }

        public string Location {
            get;
            set;
        }

        public string Track {
            get;
            set;
        }

        bool _isSelected;
        public bool IsSelected {
            get {
                return _isSelected;
            }
            set {
                SetObservableProperty (ref _isSelected, value);
            }
        }

        public ICommand ToggleSelection {
            get {
                return new Command (sender => {
					if (IsSelected) {
						IsSelected = false;
						// delete key
						_agenda.SelectedSessions.Remove (_slot.StartTime);
					}
					else {
						IsSelected = true;
						MessagingCenter.Send (this, "SessionSelected");
						// update value
						_agenda.SelectedSessions [_slot.StartTime] = Session.Id;
					}

					_repository.SaveAgenda (_agenda);
                });
            }
        }

        public bool IsOptional {
            get;
            set;
        }
	}
}

