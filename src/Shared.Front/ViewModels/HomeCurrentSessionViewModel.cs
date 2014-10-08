using System;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Models;
using System.Windows.Input;
using CouchbaseConnect2014.Views;
using Xamarin.Forms;

namespace CouchbaseConnect2014
{
	public class HomeCurrentSessionViewModel : BaseViewModel
	{
		public HomeCurrentSessionViewModel (Slot slot, Session session)
		{
			Session = session;
			CurrentSlot = slot;
		}

		Session _session;
		public Session Session {
			get {
				return _session;
			}
			set {
				_session = value;

				Title = Session.Title;
				Location = Session.Location;
				Time = Session.Time;

				if (value != null) {
					Track = 
						!string.IsNullOrEmpty (value.Track) ? value.Track :
						string.IsNullOrEmpty (value.Title) ? "None" :
						// check title for registration
						value.Title == "Registration" ? value.Title :
						// check title for any breaks
						value.Title == "Break" ||
						value.Title == "Breakfast" ||
						value.Title == "Lunch" ||
						value.Title == "Afternoon Break" ||
						value.Title == "Party" ? "MealBreak" : "NoTrack";
				}
			}
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

		string _track;
		public string Track {
			get {
				return _track;
			}
			set {
				SetObservableProperty (ref _track, value);
			}
		}

		string _location;
		public string Location {
			get {
				return _location;
			}
			set {
				SetObservableProperty (ref _location, value);
			}
		}

		DateTime _time;
		public DateTime Time {
			get {
				return _time;
			}
			set {
				SetObservableProperty (ref _time, value);
			}
		}

		Slot _currentSlot;
		internal Slot CurrentSlot {
			get {
				return _currentSlot;
			}
			set {
				_currentSlot = value;
			}
		}

		public ICommand NavigateToSession {
			get {
				return new Command (() => {
					if (Session == null) return;

					Navigation.PushAsync (new ChooseSessionView (
						CurrentSlot, Session
					));
				});
			}
		}
	}
}

