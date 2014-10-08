using System;
using NUnit.Framework;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Models;
using System.Collections.Generic;
using System.Linq;

namespace CouchbaseConnect2014.Tests.ViewModels
{
    [TestFixture]
    public class AgendaDayViewModelTests
    {
        AgendaDayViewModel _sut;
        Day _dayModel;

        [SetUp]
        public void SetUp() {
            _dayModel = new Day {
                Date = new DateTime (2014, 8, 26),
                Slots = new List<Slot> {
                    new Slot {
                        StartTime = new DateTime(2014, 8, 26, 7, 30, 0)
                    }
                }
            };
            _sut = new AgendaDayViewModel (_dayModel);
        }

        [Test]
        public void should_get_date_from_model() {
            Assert.AreEqual (_dayModel.Date, _sut.Date);
        }

        [Test]
        public void should_have_a_slot_for_each_model_slot() {
            Assert.AreEqual (_dayModel.Slots.Count(), _sut.Slots.Count());
        }
    }
}

