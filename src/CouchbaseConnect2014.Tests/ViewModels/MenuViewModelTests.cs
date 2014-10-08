using System;
using NUnit.Framework;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Tests.Mocks;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.Tests.ViewModels
{
    [TestFixture]
    public class MenuViewModelTests
    {
        MenuViewModel _sut;
        MockRepository _repository;

        [SetUp]
        public void SetUp ()
        {
            _repository = new MockRepository ();
            _sut = new MenuViewModel (_repository, null);
        }

        [Test]
        public void should_get_profile_on_init() {
            var profile = new Contact ();
            _repository.GetProfileResult = profile;
            _sut.Initialize ().Wait ();
            Assert.AreSame (profile, _sut.Profile);
        }

        [Test]
        public void should_get_first_name_from_profile() {
            _sut.Profile = new Contact { First = "Fred" };
            Assert.AreEqual ("Fred", _sut.FirstName);
        }

        [Test]
        public void should_get_last_name_from_profile() {
            _sut.Profile = new Contact { Last = "Flintstone" };
            Assert.AreEqual ("Flintstone", _sut.LastName);
        }
    }
}

