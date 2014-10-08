using System;
using System.Linq;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Models;
using System.Collections.Generic;
using CouchbaseConnect2014.Views;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace CouchbaseConnect2014.ViewModels
{
    public class ScavengerHuntViewModel : BaseViewModel
    {
		readonly IRepository _repository;
		ObservableCollection<Models.ScavengerHuntView> _items;
		bool _hasInitialized = false;

		public ScavengerHuntViewModel (IRepository repository)
        {
			_repository = repository;

			ScavengerHuntItems = new List<ScavengerHuntCellViewModel> ();
        }

		internal async override Task Initialize (params object[] args)
		{
			await base.Initialize (args);
		
			if (_hasInitialized == false)
			{ 
				_items = _repository.GetScavengerHuntItems ();

				_items.CollectionChanged += 
				(object sender, NotifyCollectionChangedEventArgs e) => 
					ScavengerHuntItems = _items.Select (item => new ScavengerHuntCellViewModel (item)).ToList ();
			}
			_hasInitialized = true;
		}

		List<ScavengerHuntCellViewModel> _scavengerHuntItems;
		public List<ScavengerHuntCellViewModel> ScavengerHuntItems
		{
			get { return _scavengerHuntItems; }
			set { 
                SetObservableProperty (ref _scavengerHuntItems, value); 
                ItemsRemaining = ScavengerHuntItems.Count (i => !i.IsCaptured);

				CaptureStats = new CaptureStatistics { 
					ItemsRemaining = ItemsRemaining, 
					TotalItems = value.Count
				};
            }
		}

		int _itemsRemaining;
		public int ItemsRemaining
		{
			get { return _itemsRemaining; }
			set { SetObservableProperty (ref _itemsRemaining, value); }
		}

		CaptureStatistics _captureStats;
		public CaptureStatistics CaptureStats
		{
			get
			{ 
				return _captureStats;
			}
			set
			{ 
				SetObservableProperty (ref _captureStats, value);
			}
		}

		ScavengerHuntCellViewModel _selectedItem;
		public ScavengerHuntCellViewModel SelectedItem 
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;

				if (_selectedItem != null)
					Navigation.PushAsync (new ScavengerHuntCaptureView (_selectedItem.HuntItem));

			}
		}
    }
}

