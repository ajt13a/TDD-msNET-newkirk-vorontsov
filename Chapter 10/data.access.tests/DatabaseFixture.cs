using System;
using System.Data.SqlClient;
using DataAccess;
using NUnit.Framework;

[TestFixture]
public abstract class DatabaseFixture : ConnectionFixture
{
	[SetUp]
	public void StartTransaction()
	{
		TransactionManager.Begin(Connection);
		Insert();
	}

	public abstract void Insert();

	[TearDown]
	public void Rollback()
	{
		TransactionManager.Rollback();
	}
}
