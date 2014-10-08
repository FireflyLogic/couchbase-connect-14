using System;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using System.Windows.Input;

namespace CouchbaseConnect2014.ViewModels
{
	public class RateSessionViewModel : BaseViewModel
	{
		SessionRatingResult _surveyResult;
		IRepository _repository;

		internal override async System.Threading.Tasks.Task Initialize (params object[] args)
		{
			await base.Initialize (args);
			CheckRatingExistence ((string)args [0]);
		}

		public RateSessionViewModel (IRepository repository)
		{
			_repository = repository;

			_surveyResult = new SessionRatingResult ();

			SessionId = "";
			ContentRating = 0;
			SpeakerRating = 0;
			Comments = "";
		}

		async void CheckRatingExistence (string sessionId)
		{
			SessionId = sessionId;
			var rating = await _repository.GetSessionRatingResult(SessionId);

			if(rating != null)
			{
				ContentRating = rating.ContentRating;
				SpeakerRating = rating.SpeakerRating;
				Comments = rating.Comments;
			}
		}

		string _sessionId;
		public string SessionId
		{
			get { return _sessionId; }
			set
			{
				SetObservableProperty (ref _sessionId, value);
				_surveyResult.SessionId = _sessionId;
			}
		}

		int _contentRating;
		public int ContentRating {
			get { return _contentRating; }
			set 
			{
				SetObservableProperty (ref _contentRating, value);
				_surveyResult.ContentRating = _contentRating;
			}
		}

		int _speakerRating;
		public int SpeakerRating {
			get { return _speakerRating; }
			set 
			{
				SetObservableProperty (ref _speakerRating, value);
				_surveyResult.SpeakerRating = _speakerRating;
			}
		}

		string _comments;
		public string Comments
		{
			get { return _comments; }
			set
			{
				SetObservableProperty (ref _comments, value);
				_surveyResult.Comments = _comments;
			}
		}

		string _alertMessage;
		public string AlertMessage 
		{
			get { return _alertMessage; }
			set { SetObservableProperty (ref _alertMessage, value); }
		}

		public ICommand SaveRating
		{
			get {
				return new Command ((sender) =>
				{
					/* LOGIC: Check ratings to make sure they were rated
					 * - if no --> show alert to tell user to complete those ratings
					 * - if yes --> submit session rating and go back to previous view
					 */
					if (_surveyResult.ContentRating == 0 | _surveyResult.SpeakerRating == 0)
					{
						AlertMessage = "Uh oh... We're missing some ratings. Please make sure you rate both the Content and the Speaker!";
					}
					else
					{
						_repository.SaveSessionRatingResult (_surveyResult);

                        Navigation.PopAsync ();
					}
				});
			}
		}
	}
}

