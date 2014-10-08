using System;

namespace CouchbaseConnect2014
{
	public class SessionRatingResult
	{
		public string SessionId
		{
			get;
			set;
		}

		int _contentRating;
		public int ContentRating
		{
			get { return _contentRating; }
			set { _contentRating = value; }
		}

		int _speakerRating;
		public int SpeakerRating
		{
			get { return _speakerRating; }
			set { _speakerRating = value; }
		}

		string _comments;
		public string Comments
		{
			get {return _comments; }
			set { _comments = value; }
		}

		public SessionRatingResult ()
		{
			ContentRating = 0;
			SpeakerRating = 0;
			Comments = "";
		}
	}
}

