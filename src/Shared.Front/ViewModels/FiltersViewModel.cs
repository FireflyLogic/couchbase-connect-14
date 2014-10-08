using System;
using CouchbaseConnect2014.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using CouchbaseConnect2014.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CouchbaseConnect2014.ViewModels
{
	public class FiltersViewModel: BaseViewModel
	{
        IRepository _repository;

        public FiltersViewModel (IRepository repository)
		{
            this._repository = repository;
		}

        internal override Task Initialize (params object[] args)
        {
            var task = base.Initialize (args);
            var trackNames = new List<string> {
                "Mobile", "Enterprise", "Developer", "Operations", "Architecture", "Customer"
            };

            var filterTrackNames = _repository.GetTrackFilters ();

            TrackFilters = trackNames
                .Select (name => CreateFiltersCellViewModel (name, filterTrackNames.Contains (name)))
                .ToList ();

            return task;
        }

        FiltersCellViewModel CreateFiltersCellViewModel (string name, bool isSelected)
        {
            var filtersCellViewModel = new FiltersCellViewModel {
                Track = name,
                IsSelected = isSelected
            };
            filtersCellViewModel.PropertyChanged += FiltersCellViewModelPropertyChanged;
            return filtersCellViewModel;
        }

        void FiltersCellViewModelPropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected") {
                SaveSelections ();
            }
        }

        void SaveSelections ()
        {
            var selections = TrackFilters
                .Where (f => f.IsSelected)
                .Select (f => f.Track);

            _repository.SaveTrackFilters (selections);
        }

        IList<FiltersCellViewModel> _trackFilters;
        public IList<FiltersCellViewModel> TrackFilters
		{
			get { return _trackFilters; }
            set { SetObservableProperty (ref _trackFilters, value); }
		}

		public ICommand ClearTrackFilters 
		{
			get 
			{
				return new Command (() =>
				{
					foreach(var filter in TrackFilters)
					{
						filter.IsSelected = false;
					}
				});
			}
		}
	}
}

