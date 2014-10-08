using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace CouchbaseConnect2014.Extensions
{
	public static class Object_ToDictionary
	{
		public static IDictionary<string, object> ToDictionary(this object source, 
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | 
			BindingFlags.Public | 
			BindingFlags.Instance)
		{
			return source.GetType().GetProperties(bindingAttr).ToDictionary
				(
					propInfo => ToCamel(propInfo.Name),
					propInfo => propInfo.GetValue(source, null)
				);

		}

		static string ToCamel(string pascal)
		{
			if (pascal == null || pascal.Length == 0)
				return pascal;

			string firstCharacterAsLower = pascal.Substring (0, 1).ToLower();

			if (pascal.Length == 1)
				return firstCharacterAsLower;

			return firstCharacterAsLower + pascal.Substring (1);
		}
	}
}

