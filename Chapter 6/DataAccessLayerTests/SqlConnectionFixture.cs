using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Utilities
{
	[TestFixture]
	public class SqlConnectionFixture
	{
		private string connectionString; 

		[SetUp]
		public void RetrieveConnectionString()
		{
			connectionString = 
				ConfigurationSettings.AppSettings.Get("Catalog.Connection");
		}

		[Test]
		public void CanRetrieveConnectionString()
		{
			Assert.IsNotNull(connectionString);
		}

		[Test]
		public void ConnectionIsOpen()
		{
			SqlConnection connection = 
				new SqlConnection(connectionString);
			connection.Open();
			Assert.AreEqual(ConnectionState.Open, connection.State); 
			connection.Close();
		}
	}
}
