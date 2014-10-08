using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.ViewModels;

namespace CouchbaseConnect2014.ViewModels
{
	public class FiltersCellViewModel : BaseViewModel
	{
		public FiltersCellViewModel ()
		{
		}

		string _track;
		public string Track
		{
			get { return _track; }
			set { _track = value; }
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

		public ICommand ToggleSelection
		{
			get {
                return new Command (() => {
					IsSelected = !IsSelected;
				});
			}
		}
	}
}

