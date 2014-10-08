using System;
using NUnit.Framework;
using CouchbaseConnect2014.ViewModels;
using System.Collections.Generic;
using CouchbaseConnect2014.Services;
using CouchbaseConnect2014.Tests.Mocks;
using System.Linq;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.Tests.ViewModels
{
    [Ignore("until day workaround is fixed")]
    [TestFixture]
    public class AgendaViewModelTests
    {
        AgendaViewModel _sut;
        AgendaDayViewModel _firstDay;
        AgendaDayViewModel _lastDay;
        MockRepository _repository;
        MockTimeService _timeService;

        [SetUp]
        public void SetUp ()
        {
            _repository = new MockRepository ();
            _timeService = new MockTimeService ();
            _sut = new AgendaViewModel (_repository, _timeService) {
                AgendaDays = new List<AgendaDayViewModel> {
                    new AgendaDayViewModel {
                        Date = new DateTime(2014, 10, 6)
                    },
                    new AgendaDayViewModel {
                        Date = new DateTime(2014, 10, 7)
                    },
                    new AgendaDayViewModel {
                        Date = new DateTime(2014, 10, 8)
                    }
                }
            };
            
            _firstDay = _sut.AgendaDays.First ();
            _lastDay = _sut.AgendaDays.Last ();
        }

        [Test]
        public void should_select_first_day_before_first_day() {
            _timeService.Today = _firstDay.Date.AddDays (-1);
            _sut.Initialize ().Wait ();
            Assert.AreSame (_firstDay, _sut.SelectedDay);
        }

        [Test]
        public void should_select_first_day_on_first_day() {
            _timeService.Today = _firstDay.Date;
            _sut.Initialize ().Wait ();
            Assert.AreSame (_firstDay, _sut.SelectedDay);
        }

        [Test]
        public void should_select_second_day_on_second_day() {
            var day2 = _sut.AgendaDays.Skip (1).First ();
            _timeService.Today = day2.Date;
            _sut.Initialize ().Wait ();
            Assert.AreSame (day2, _sut.SelectedDay);
        }

        [Test]
        public void should_select_last_day_on_last_day() {
            _timeService.Today = _lastDay.Date;
            _sut.Initialize ().Wait ();
            Assert.AreSame (_lastDay, _sut.SelectedDay);
        }

        [Test]
        public void should_select_last_day_after_last_day() {
            _timeService.Today = _lastDay.Date.AddDays (1);
            _sut.Initialize ().Wait ();
            Assert.AreSame (_lastDay, _sut.SelectedDay);
        }

        [Test]
        public void should_get_days_from_repo_on_initialize() {
            var days = new List<Day> ();
            _repository.GetDaysResult = days;
            _sut.Initialize ().Wait();
            Assert.AreSame (days, _sut.Days);
        }

        [Test]
        public void should_get_agenda_from_repo_on_initialize() {
            _repository.GetDaysResult = new List<Day> ();
            var agenda = new Agenda();
            _repository.GetAgendaResult = agenda;
            _sut.Initialize ().Wait ();
            Assert.AreSame (agenda, _sut.Agenda);
        }

        [Test]
        public void should_create_day_view_model_for_each_day_model() {
            _sut.Days = new List<Day> {
                new Day (),
                new Day (),
                new Day ()
            };

			Assert.AreEqual (_sut.Days.Count(), _sut.AgendaDays.Count());
        }

        [Test]
        public void should_create_agenda_days_from_days_and_agenda() {
            var days = new List<Day> {
                new Day {
                    Date = new DateTime(2014, 10, 9),
					Slots = new List<Slot> {
                        new Slot {
                            StartTime = new DateTime(2014, 10, 9, 9, 15, 0),
							SessionIds = new List<string>{ "1", "2"}
							
							//	{
							//        new Session {
							//            Id = "1",
							//            Title = "Session One",
							//            Location = "Location One"
							//        },
							//        new Session {
							//            Id = "2",
							//            Title = "Session Two",
							//            Location = "Location Two"
							//        }
							//    }
                        }
                    }
                }
            };

            var agenda = new Agenda {
//				Sessions = new List<UserSession> {
//                    new UserSession {
//                        SlotId = "2",
//                        SessionId = "1"
//                    }
//                }
            };

            var agendaDays = _sut.CreateAgendaDays (days, agenda);

			Assert.AreEqual (days.Count, agendaDays.Count());
			Assert.AreEqual (days.First ().Slots.Count(), agendaDays.First ().Slots.Count());

            Assert.AreEqual (days.First ().Slots.First ().SessionIds.First (),
                agendaDays.First ().Slots.First ());
        }

        [Test]
        public void should_add_session_to_agenda_when_no_other_options_in_slot() {
			var days = new List<Day> {
				new Day {
					Date = new DateTime (2014, 10, 9),
					Slots = new List<Slot> {
						new Slot {
							StartTime = new DateTime (2014, 10, 9, 9, 15, 0),
							SessionIds = new List<string> () { "1" }
							// new Session {
							//    Id = "1",
							//    Title = "Session One",
							//    Location = "Location One"                                
						}
					}
				}
			};

            var agenda = new Agenda {
//                Sessions = {}
            };

            var agendaDays = _sut.CreateAgendaDays (days, agenda);

			Assert.AreEqual (days.Count, agendaDays.Count());
			Assert.AreEqual (days.First ().Slots.Count(), agendaDays.First ().Slots.Count());

            Assert.AreEqual (days.First ().Slots.First ().SessionIds.First (),
                agendaDays.First ().Slots.First ());
        }
    }
}

