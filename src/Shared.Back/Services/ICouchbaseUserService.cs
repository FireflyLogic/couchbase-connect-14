using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CouchbaseConnect2014.Models;

namespace CouchbaseConnect2014.Services
{
	public interface ICouchbaseUserService
	{
		Task<bool> CreateUser (string authServerUrl, string username);
		Task<CouchbaseUser> GetUser(string authServerUrl, string name);
	}
}

