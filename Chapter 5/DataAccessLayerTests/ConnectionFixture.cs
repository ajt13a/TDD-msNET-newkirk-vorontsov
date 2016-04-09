using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

[TestFixture]
public abstract class ConnectionFixture
{
	private SqlConnection connection;

	[TestFixtureSetUp]
	public void OpenConnection()
	{
		connection = new SqlConnection(
			ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
		connection.Open();
	}

	[TestFixtureTearDown]
	public void CloseConnection()
	{
		connection.Close();
	}

	public SqlConnection Connection
	{
		get { return connection; }
	}
}
