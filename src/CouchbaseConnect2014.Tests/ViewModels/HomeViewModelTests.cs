using System;
using NUnit.Framework;
using CouchbaseConnect2014.ViewModels;
using System.Collections.Generic;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Tests.Mocks;

namespace CouchbaseConnect2014.Tests.ViewModels
{
    [TestFixture]
    public class HomeViewModelTests
    {
        HomeViewModel _sut;
        MockRepository _repository;
        MockTimeService _timeService;
        MockTwitterService _twitterService;
        IList<Day> _days;
        Schedule _schedule;
        Slot _slot1, _slot2;
        IEnumerable<string> _sessionIds;
        IEnumerable<Session> _sessions;
        AppConfig _appConfig;

        [SetUp]
        public void SetUp ()
        {
            _sessionIds = new List<string> ();

            _slot1 = new Slot {
                StartTime = new DateTime (2014, 10, 1, 8, 0, 0),
                EndTime = new DateTime (2014, 10, 1, 8, 50, 0),
                SessionIds = _sessionIds
            };

            _slot2 = new Slot {
                StartTime = new DateTime (2014, 10, 1, 9, 0, 0),
                EndTime = new DateTime (2014, 10, 1, 9, 50, 0),
                SessionIds = _sessionIds
            };

            _days = new List<Day> {
                new Day {
                    Date = new DateTime(2014, 10, 1),
                    Slots = new List<Slot> {
                        _slot1,
                        _slot2
                    }
                }
            };

            _schedule = new Schedule { Days = _days };
            _appConfig = new AppConfig ();

            _repository = new MockRepository {
                GetDaysResult = _days,
                GetScheduleResult = _schedule,
                GetAppConfigResult = _appConfig
            };

            _sessions = new List<Session> ();
            _repository.GetSessionsResults [_sessionIds] = _sessions;

            _timeService = new MockTimeService ();
            _twitterService = new MockTwitterService ();
            _sut = new HomeViewModel (_repository, _timeService, _twitterService);
        }

        [Test]
        public void should_get_schedule_from_repo_on_init () {
            _sut.Initialize ().Wait ();
            Assert.AreSame (_schedule, _sut.Schedule);
        }

        [Test]
        public void should_find_current_slot () {
            _timeService.Now = _slot1.StartTime.AddMinutes (1);
            _sut.Schedule = _schedule;
            Assert.AreSame (_slot1, _sut.CurrentSlot);
        }

        [Test]
        public void should_show_next_slot_when_none_is_current () {
            _timeService.Now = _slot1.EndTime.AddMinutes (1);
            _sut.Schedule = _schedule;
            Assert.AreSame (_slot2, _sut.CurrentSlot);
        }

        [Test]
        public void should_show_last_slot_when_none_is_current () {
            _timeService.Now = _slot2.EndTime.AddMinutes (1);
            _sut.Schedule = _schedule;
            Assert.AreSame (_slot2, _sut.CurrentSlot);
        }

        [Test]
        public void should_get_current_sessions_from_current_slot () {
            var slot = new Slot { SessionIds = _sessionIds };
            _sut.CurrentSlot = slot;
            Assert.AreSame (_sessions, _sut.CurrentSessions);
        }
    }
}

