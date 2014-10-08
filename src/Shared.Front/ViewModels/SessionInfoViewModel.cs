using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using System.Linq;
using System.Threading.Tasks;

namespace CouchbaseConnect2014.ViewModels
{
	public class SessionInfoViewModel : BaseViewModel
	{
		public SessionInfoViewModel (IRepository repository, Session session, Slot slot )
        {
            Session = session;
            Title = session.Title;
            Location = session.Location;
//            Track = session.Track;
            Time = slot.StartTime;
			Abstract = session.Abstract;

			Track = session == null ? "None" :
				!string.IsNullOrEmpty (session.Track) ? session.Track :
				string.IsNullOrEmpty (session.Title) ? "None" :
				// check title for registration
				session.Title == "Registration" ? session.Title :
				// check title for any breaks
				session.Title == "Break" ||
				session.Title == "Breakfast" ||
				session.Title == "Lunch" ||
				session.Title == "Afternoon Break" ||
				session.Title == "Party" ? "MealBreak" : "NoTrack";

			var speakerId = session.SpeakerIds.FirstOrDefault ();
			if (String.IsNullOrWhiteSpace (speakerId) == false)
			{
				Speaker speaker = repository.GetSpeaker (speakerId);
				if (speaker != null)
				{
					PrimarySpeakerName = String.Format ("{0} {1}", speaker.First, speaker.Last);
					PrimarySpeakerCompany = speaker.Company;
					PrimarySpeakerRole = speaker.Role;
					PrimarySpeakerHeadshot = speaker.HeadshotUrl;
				}
			}
		}

		public string PrimarySpeakerName {get; set;}
		public string PrimarySpeakerCompany {get; set;}
		public string PrimarySpeakerRole {get; set;}
		public string PrimarySpeakerHeadshot {get; set;}

        internal Session Session {
            get;
            private set;
        }

		public string Title {
			get;
			set;
		}

        public string Abstract {
            get;
            set;
        }

        public string Location {
            get;
            set;
        }

        public DateTime Time {
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
	}
}

