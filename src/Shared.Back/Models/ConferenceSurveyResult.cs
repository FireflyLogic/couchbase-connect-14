using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Models
{
	public class ConferenceSurveyResult
	{
		int _evaluationRating;
		public int EvaluationRating
		{
			get { return _evaluationRating; }
			set { _evaluationRating = value; }
		}

		string _lengthRating;
		public string LengthRating
		{
			get { return _lengthRating; }
			set { _lengthRating = value; }
		}

		List<string> _valuableTracks;
		public List<string> ValuableTracks
		{
			get { return _valuableTracks; }
			set { _valuableTracks = value; }
		}

		string _comments;
		public string Comments
		{
			get {return _comments; }
			set { _comments = value; }
		}

		public ConferenceSurveyResult ()
		{
			EvaluationRating = 0;
			LengthRating = "";
			ValuableTracks = new List<string> ();
			Comments = "";
		}
	}
}

