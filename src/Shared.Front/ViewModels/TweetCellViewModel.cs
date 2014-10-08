using System;
using System.Windows.Input;
using CouchbaseConnect2014.Models;
using Xamarin.Forms;
using CouchbaseConnect2014.Extensions;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014
{
	public class TweetCellViewModel : BaseViewModel
	{
		public TweetCellViewModel (Tweet tweet)
		{
			Tweet = tweet;
		}

		Tweet _tweet;
		public Tweet Tweet {
			get {
				return _tweet;
			}
			set {

				_tweet = value;

				Icon = Tweet.Icon;
				Twitter = Tweet.Twitter;
				Url = Tweet.Url;
				Name = Tweet.Name;
				Content = Tweet.Content;
				CreatedAtDate = Tweet.CreatedAtDate;
				FirstEmbeddedUrl = Tweet.FirstEmbeddedUrl;
			}
		}

		string _icon;
		public string Icon {
			get {
				return _icon;
			}
			set {
				SetObservableProperty (ref _icon, value);
			}
		}

		string _twitter;
		public string Twitter {
			get {
				return _twitter;
			}
			set {
				SetObservableProperty (ref _twitter, value);
			}
		}

		string _url;
		public string Url
		{
			get {
				return _url;
			}
			set {
				SetObservableProperty (ref _url, value);
			}
		}

		string _name;
		public string Name
		{
			get {
				return _name;
			}
			set {
				SetObservableProperty (ref _name, value);
			}
		}

		public string Time 
		{
			get
			{
				return _createdAtDate.TimeSince();
			}
		}

		string _content;
		public string Content 
		{
			get {
				return _content;
			}
			set {
				SetObservableProperty (ref _content, value);
			}
		}

		DateTime _createdAtDate;
		public DateTime CreatedAtDate
		{
			get {
				return _createdAtDate;
			}
			set {
				SetObservableProperty (ref _createdAtDate, value);
			}
		}

		string _firstEmbeddedUrl;
		public string FirstEmbeddedUrl
		{
			get {
				return _firstEmbeddedUrl;
			}
			set {
				SetObservableProperty (ref _firstEmbeddedUrl, value);
			}
		}

		public ICommand TweetTapped {
			get {
				return new Command (() => {
					if(Url != null)
						Device.OpenUri(new Uri(Url));
				});
			}
		}
	}
}

