using System;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using CouchbaseConnect2014.Views;

namespace CouchbaseConnect2014.ViewModels
{
	public class FullScheduleSessionGroup : List<FullScheduleCellViewModel>
	{
        public FullScheduleSessionGroup (Slot slot)
        {
            StartTime = slot.StartTime;
            Slot = slot;

            MessagingCenter.Subscribe<FullScheduleCellViewModel> (this, "SessionSelected",
                sender => {
                    if (!Contains(sender)) return;
                    foreach (var item in this.Except (new [] {sender})) {
                        item.IsSelected = false;
                    }
                });
        }

        public DateTime StartTime {
            get;
            set;
        }

        internal Slot Slot { get; private set; }
	}
}

