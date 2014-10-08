using System;
using System.Collections.Generic;

namespace CouchbaseConnect2014.Extensions
{
	public static class IDictionary_ToObject
	{
		public static T ToObject<T>(this IDictionary<string, object> source)
			where T : class, new()
		{
			T someObject = new T();
			Type someObjectType = someObject.GetType();

			foreach (KeyValuePair<string, object> item in source)
			{
				var p = someObjectType.GetProperty (ToPascal(item.Key));
				if (p != null)
				{
					p.SetValue (someObject, item.Value, null);
				}
			}

			return someObject;
		}

		static string ToPascal(string camel)
		{
			if (camel == null || camel.Length == 0)
				return camel;

			string firstCharacterAsUpper = camel.Substring (0, 1).ToUpper();

			if (camel.Length == 1)
				return firstCharacterAsUpper;

			return firstCharacterAsUpper + camel.Substring (1);
		}
	}
}

