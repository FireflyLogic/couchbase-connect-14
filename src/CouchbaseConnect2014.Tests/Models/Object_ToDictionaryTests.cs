using System;
using NUnit.Framework;
using CouchbaseConnect2014.Extensions;

namespace CouchbaseConnect2014.Tests
{
	[TestFixture]
	public class Object_ToDictionaryTests
	{
		[TestCase]
		public void converts_property_names_to_pascel_case()
		{
			var o = new GoodNames()
			{
				A = "1",
				Ab = "2",
				AbCd = "3",
			};

			var d = o.ToDictionary ();

			Assert.AreEqual ("1", d["a"] as string);
			Assert.AreEqual ("2", d["ab"] as string);
			Assert.AreEqual ("3", d["abCd"] as string);
		}

		class GoodNames
		{
			public string A { get; set; }
			public string Ab { get; set; }
			public string AbCd { get; set; }
		}

		class BadNames
		{
			public string A { get; set; }
			public string a { get; set; }
		}
	}
}

