using System;
using NUnit.Framework;
using CouchbaseConnect2014.ViewModels;
using CouchbaseConnect2014.Models;
using CouchbaseConnect2014.Tests.Mocks;

namespace CouchbaseConnect2014.Tests.ViewModels
{
	[TestFixture]
	public class EditProfileViewModelTests
	{
		MockRepository _repository;
		EditProfileViewModel _sut;

		[SetUp]
		public void SetUp ()
		{
			_repository = new MockRepository ();
			_sut = new EditProfileViewModel (_repository);
		}

		[Test]
		public void should_get_profile_from_repo_on_initialize() {
			var profile = new Contact {
				First = "Jimi",
				Last = "Jamison",
				Role = "Lead Vocalist"
			};

			_repository.GetProfileResult = profile;
			_sut.Initialize ().Wait ();
			Assert.AreSame (profile, _sut.Profile);
		}

		[Test]
		public void should_get_name_from_profile_model() {
			_sut.Profile = new Contact { First = "Foo", Last = "Bar" };
			Assert.AreEqual ("Foo Bar", _sut.FullName);
		}

		[Test]
		public void should_get_title_from_profile_model() {
			_sut.Profile = new Contact { Role = "Bar" };
			Assert.AreEqual ("Bar", _sut.Title);
		}

		[Test]
		public void should_derive_image_uri_from_email() {
			_sut.Email = "hey@diddle.com";
			Assert.AreEqual (
				"http://www.gravatar.com/avatar/ea943ca92f48502d1435bb2b3f0fced1.jpg?s=90", 
				_sut.ImageUri);
		}
	}
}



