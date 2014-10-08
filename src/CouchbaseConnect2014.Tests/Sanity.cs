using System;
using NUnit.Framework;

namespace CouchbaseConnect2014.Tests
{
	[TestFixture]
	public class Sanity
	{
		[Test]
		public void TrueIsTrue()
		{
			Assert.IsTrue (true, "The simple sanity check to make sure tests are working somehow failed");
		}
	}
}

