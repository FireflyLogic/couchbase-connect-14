using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Services;

namespace CouchbaseConnect2014.ViewModels
{
	public class ConferenceSurveyViewModel: BaseViewModel
	{
		ConferenceSurveyResult _surveyResult;
		IRepository _repository;

		internal override async System.Threading.Tasks.Task Initialize (params object[] args)
		{
			await base.Initialize (args);
			CheckSurveyExistence ();
		}

		public ConferenceSurveyViewModel (IRepository repository)
		{
			_repository = repository;

			_surveyResult = new ConferenceSurveyResult ();

			ConferenceEvaluationRating = 0;
			ConferenceLengthRating = "";
			MostValuableTracks = new List<string> ();
			ConferenceSurveyComments = "";
		}

		async void CheckSurveyExistence ()
		{
			// Query Couchbase to see if user has already submitted survey
			var survey = await _repository.GetConferenceSurveyResult ();

			SurveyExists = survey != null;
		}

		bool _surveyExists;
		public bool SurveyExists 
		{
			get { return _surveyExists; }
			set { SetObservableProperty (ref _surveyExists, value); }
		}

		int _conferenceEvaluationRating;
		public int ConferenceEvaluationRating 
		{
			get { return _conferenceEvaluationRating; }
			set 
			{ 
				SetObservableProperty (ref _conferenceEvaluationRating, value); 
				_surveyResult.EvaluationRating = _conferenceEvaluationRating;
			}
		}

		string _conferenceLengthRating;
		public string ConferenceLengthRating
		{
			get { return _conferenceLengthRating; }
			set 
			{ 
				SetObservableProperty (ref _conferenceLengthRating, value); 
				_surveyResult.LengthRating = _conferenceLengthRating;
			}
		}

		List<string> _mostValuableTracks;
		public List<string> MostValuableTracks
		{
			get { return _mostValuableTracks; }
			set 
			{ 
				SetObservableProperty (ref _mostValuableTracks, value);
				if (_surveyResult != null)
					_surveyResult.ValuableTracks = _mostValuableTracks;
			}
		}

		string _conferenceSurveyComments;
		public string ConferenceSurveyComments
		{
			get { return _conferenceSurveyComments; }
			set
			{
				SetObservableProperty (ref _conferenceSurveyComments, value);

				if (_conferenceSurveyComments != "Add Comment")
					_surveyResult.Comments = _conferenceSurveyComments;
				else
					_surveyResult.Comments = "";
			}
		}

		string _alertMessage;
		public string AlertMessage 
		{
			get { return _alertMessage; }
			set { SetObservableProperty (ref _alertMessage, value); }
		}

		public ICommand SubmitSurvey 
		{
			get 
			{
				return new Command ((sender) =>
				{
					MessagingCenter.Send(this, "DismissKeyboard");
					/* LOGIC: Check first 3 answers to make sure they were answered
					 * - if no --> show alert to tell user to complete those questions
					 * - if yes --> submit survey, show t-shirt screen
					 */
					if ((_surveyResult.EvaluationRating == 0) 
						| (_surveyResult.LengthRating == null | _surveyResult.LengthRating == "") 
						| (_surveyResult.ValuableTracks == null | !_surveyResult.ValuableTracks.Any()))
					{
						AlertMessage = "Uh oh... We're missing some answers. Please make sure you answer all of the questions!";
					} 
					else if(!SurveyExists)
					{
						// Submit/save survey result
						_repository.SaveConferenceSurveyResult(_surveyResult);

						AlertMessage = "Your survey has been submitted. \nThank you!";
						SurveyExists = true;
					} else {
						AlertMessage = "What are you waiting for!? Go get your t-shirt already!";
					}
				});
			}
		}
	}
}

