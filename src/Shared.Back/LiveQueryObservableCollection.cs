using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Couchbase.Lite;

namespace CouchbaseConnect2014
{
	public class LiveQueryObservableCollection<T> : ObservableCollection<T>,IDisposable
	{
		LiveQuery query;

		public LiveQueryObservableCollection(LiveQuery query, Func<QueryRow,T> transform)
			: base() {
			this.query = query;

			query.Changed += (sender, e) => {
				Items.Clear ();
				foreach (var row in e.Rows) {
					var o = transform (row);
					if (o != null)
						Items.Add (o);
				}
				OnCollectionChanged (new NotifyCollectionChangedEventArgs (NotifyCollectionChangedAction.Add, Items.ToList ()));
				OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
			};

			query.Start ();
		}

		#region IDisposable implementation
		public void Dispose ()
		{
			query.Stop ();
		}
		#endregion
	}
}

