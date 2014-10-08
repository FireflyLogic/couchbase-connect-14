using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Models;
using System.Linq;
using System.Threading;
using CouchbaseConnect2014.Views;

namespace CouchbaseConnect2014.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
        readonly IRepository _repository;
		readonly ITimeService _timeService;
		readonly ITwitterService _twitterService;

        public HomeViewModel (
            IRepository repository, 
            ITimeService timeService, 
            ITwitterService twitterService)
		{
            _repository = repository;
            _timeService = timeService;
			_twitterService = twitterService;

			CurrentSessions = new List<HomeCurrentSessionViewModel> ();
			Tweets = new List<TweetCellViewModel> ();

		}

        internal async override Task Initialize (params object[] args)
        {
            await base.Initialize ();

			var config = await _repository.GetAppConfig ();
			if (config != null)
			{
				var getScheduleTask = _repository.GetSchedule ();
				var getTweetsTask = _twitterService.GetTweets (
					config.TwitterListName,
					config.TwitterListOwnerScreenName,
					config.TwitterAccessToken);

				IsBusy = true;
				Schedule = await getScheduleTask;

				// should timeout after 8 seconds
				if (await Task.WhenAny (getTweetsTask, Task.Delay (8000)) == getTweetsTask && !getTweetsTask.IsFaulted)
				{
					var tweets = getTweetsTask.Result;

					if (tweets == null)
						TimedOut = true;
					else
					{
						Tweets = tweets.Select (tweet => 
						new TweetCellViewModel (tweet)).ToList ();
						TimedOut = false;
					}
				}
				else
				{
					TimedOut = true;
				}
				IsBusy = false;
			}
			else
			{
				TimedOut = true;
			}
        }

		bool _isBusy;
		public bool IsBusy {
			get {
				return _isBusy;
			}
			set {
				SetObservableProperty (ref _isBusy, value);
			}
		}
			
		bool _timedOut;
		public bool TimedOut {
			get {
				return _timedOut;
			}
			set {
				SetObservableProperty (ref _timedOut, value);
			}
		}

        Schedule _schedule;
        public Schedule Schedule {
            get {
                return _schedule;
            }
            set {
                _schedule = value;
                UpdateCurrentSlot ();
            }
        }

        void UpdateCurrentSlot ()
        {
            var slots = Schedule.Days.SelectMany (day => day.Slots).ToList ();

            CurrentSlot = slots
                .SkipWhile (slot => slot.EndTime < _timeService.Now)
                .FirstOrDefault ()
                ?? slots.LastOrDefault ();
        }

        Slot _currentSlot;
        internal Slot CurrentSlot {
            get {
                return _currentSlot;
            }
            set {
				_currentSlot = value;
				SetCurrentSession ();
            }
        }

		void SetCurrentSession ()
		{
			if (_currentSlot == null) return;

			CurrentSessions = _repository.GetSessions(_currentSlot.SessionIds).Result.Select(session => 
				new HomeCurrentSessionViewModel(CurrentSlot, session)).ToList();
		}
			
		IEnumerable<HomeCurrentSessionViewModel> _currentSessions;
		public IEnumerable<HomeCurrentSessionViewModel> CurrentSessions {
            get {
                return _currentSessions;
            }
            set {
                SetObservableProperty (ref _currentSessions, value);
                CurrentSession = CurrentSessions.FirstOrDefault ();
            }
        }

		HomeCurrentSessionViewModel _currentSession;
		public HomeCurrentSessionViewModel CurrentSession {
            get {
                return _currentSession;
            }
            set {
                SetObservableProperty (ref _currentSession, value);
            }
        }

		string _currentTrack;
		public string CurrentTrack
		{
			get { return _currentTrack;}
			set { SetObservableProperty (ref _currentTrack, value); }
		}

		IList<TweetCellViewModel> _tweets;
		public IList<TweetCellViewModel> Tweets {
			get { return _tweets; }
			set { SetObservableProperty (ref _tweets, value); }
		}

	}
}

