using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CouchbaseConnect2014.ViewModels
{
	public class ChooseSessionViewModel: BaseViewModel
	{
		IRepository _repository;
		bool _initalized = false;

		public ChooseSessionViewModel(IRepository repository)
		{
			_repository = repository;
		}

        internal override async Task Initialize (params object[] args)
        {
			if (!_initalized) {
				await base.Initialize (args);
				Slot = (Slot)args [0];
				Session = (Session)args [1];

				Agenda = await _repository.GetAgenda ();
			}
			_initalized = true;
        }

        string _title;
        public string Title {
            get {
                return _title;
            }
            set {
                SetObservableProperty (ref _title, value);
            }
        }

        Slot _slot;
        internal Slot Slot {
            get {
                return _slot;
            }
            set {
                _slot = value;
                UpdateSlotSessions ();
            }
        }

        Session _session;
        public Session Session {
            get {
                return _session;
            }
            set {
                _session = value;
                UpdateSlotSessions ();
            }
        }

        Agenda _agenda;
        internal Agenda Agenda {
            get {
                return _agenda;
            }
            set {
                _agenda = value;
                UpdateSlotSessions ();
            }
        }

        bool _firstTime = true;

        void UpdateSlotSessions ()
        {
            if (Slot == null || Agenda == null || Session == null) return;

            var sessions = _repository.GetSessions (Slot.SessionIds).Result;

       	    SlotSessions = sessions
				.Select (session => new SessionInfoViewModel (_repository, session, Slot) {
                    IsSelected = Agenda.IsSelected (Slot.StartTime, session.Id)
                }).ToList ();

            if (_firstTime) {
                _firstTime = false;
                SlotSession = SlotSessions
                   .FirstOrDefault (session => session.Session.Id == Session.Id);
            }
        }

        IList<SessionInfoViewModel> _slotSessions;
        public IList<SessionInfoViewModel> SlotSessions {
            get {
                return _slotSessions;
            }
            set {
                SetObservableProperty (ref _slotSessions, value);
            }
        }

        SessionInfoViewModel _slotSession;
        public SessionInfoViewModel SlotSession {
            get {
                return _slotSession;
            }
            set {
                SetObservableProperty (ref _slotSession, value);
                SetSelectButtonText ();
            }
        }

        void SetSelectButtonText ()
        {
            if (!Slot.SessionIds.Skip (1).Any ()) {
                Title = "Session";
                SelectButtonText = "";
            }
            else
                if (SlotSession.IsSelected) {
                    Title = "Choose a Session";
                    SelectButtonText = "Deselect";
                }
                else {
                    Title = "Choose a Session";
                    SelectButtonText = "Select";
                }
        }

        public ICommand ToggleSessionSelection {
            get {
                return new Command (() => {
                    if (!Slot.SessionIds.Skip (1).Any ()) return;

                    if (SlotSession.IsSelected) {
                        SlotSession.IsSelected = false;
                        Agenda.SelectedSessions.Remove (SlotSession.Time);
                    } else {
                        foreach (var session in SlotSessions) {
                            session.IsSelected = session == SlotSession;
                        }
                        Agenda.SelectedSessions [SlotSession.Time] = SlotSession.Session.Id;
                    }

                    SetSelectButtonText ();
                    _repository.SaveAgenda (Agenda);

                    if (SlotSession.IsSelected) Navigation.PopAsync ();
                });
            }
        }

        string _selectButtonText;
        public string SelectButtonText {
            get {
                return _selectButtonText;
            }
            set {
                SetObservableProperty (ref _selectButtonText, value);
            }
        }
	}
}

