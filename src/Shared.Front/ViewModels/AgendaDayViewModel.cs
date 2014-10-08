using System;
using System.Collections.Generic;
using System.Linq;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.ViewModels
{
    public class AgendaDayViewModel : BaseViewModel
    {
        public AgendaDayViewModel ()
        {
            Slots = new List<AgendaCellViewModel> ();
        }

        public AgendaDayViewModel (Day day) : this ()
        {
            Date = day.Date;
            Slots = day.Slots.Select (s => new AgendaCellViewModel (s)).ToList();
        }

        public DateTime Date 
		{
            get;
            set;
        }

        public IEnumerable<AgendaCellViewModel> Slots {
            get;
            set;
        }
    }
}

