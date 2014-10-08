using System;
using NUnit.Framework;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Tests.Mocks;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using System.Linq;

namespace CouchbaseConnect2014.Tests.ViewModels
{
    [TestFixture]
    public class FullScheduleViewModelTests
    {
        FullScheduleViewModel _sut;
        MockRepository _repository;

        [SetUp]
        public void SetUp ()
        {
            _repository = new MockRepository ();
            _sut = new FullScheduleViewModel (_repository);
        }

        [Test]
        public void should_get_schedule_from_repo_on_initialize () {
            var schedule = new Schedule ();
            _repository.GetScheduleResult = schedule;
            _repository.GetTrackFiltersResult = new List<string> ();
            _sut.Initialize ().Wait ();
            Assert.AreSame (schedule, _sut.Schedule);
        }

        [Test]
        public void should_create_day_view_model_for_each_day () {
            _sut.Schedule = new Schedule {
                Days = new List<Day> {
                    new Day (),
                    new Day (),
                    new Day ()
                }
            };
            _sut.Agenda = new Agenda ();
            _sut.TrackFilters = new List<string> ();
            Assert.AreEqual (_sut.Schedule.Days.Count (), _sut.FullScheduleDays.Count ());
        }

        [Test]
        public void should_select_first_day () {
            var agenda = new Agenda ();
            var trackFilters = new List<string> ();
            _sut.FullScheduleDays = new List<FullScheduleDayViewModel> {
                new FullScheduleDayViewModel (_repository, new Day (), agenda, trackFilters),
                new FullScheduleDayViewModel (_repository, new Day (), agenda, trackFilters)
            };
            Assert.AreSame (_sut.FullScheduleDays.First (), _sut.SelectedDay);
        }
    }
}

