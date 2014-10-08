using System;
using NUnit.Framework;
using System.Collections.Generic;
using CouchbaseConnect2014.Extensions;

namespace CouchbaseConnect2014.Tests
{
	[TestFixture]
	public class IDictionary_ToObjectTests
	{
		[TestCase]
		public void converts_property_names_to_camel_case()
		{
			var d = new Dictionary<string, object> () {
				{ "a", "1" },
				{ "ab", "2" },
				{ "abCd", "3" },
			};

			var o = d.ToObject<GoodNames> ();

			Assert.AreEqual ("1", o.A);
			Assert.AreEqual ("2", o.Ab);
			Assert.AreEqual ("3", o.AbCd);
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

