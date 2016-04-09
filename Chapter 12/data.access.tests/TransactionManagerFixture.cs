using System;
using System.Data.SqlClient;
using DataAccess;
using NUnit.Framework;

namespace Utilities
{
	[TestFixture]
	public class TransactionManagerFixture : ConnectionFixture
	{
		private SqlTransaction transaction;

		[SetUp]
		public void BeginTransaction()
		{
			transaction = TransactionManager.Begin(Connection);
		}

		[TearDown]
		public void CommitTransaction()
		{
			if(TransactionManager.Transaction() != null)
			{
				TransactionManager.Commit();
			}
		}

		[Test]
		public void BeginNewTransaction()
		{
			Assert.IsNotNull(transaction);
		}

		[Test]
		public void GetCurrentTransaction()
		{
			Assert.AreSame(transaction, TransactionManager.Transaction());
		}

		[Test]
		public void GetCurrentTransactionNoTransactionInProgress()
		{
			TransactionManager.Commit();
			Assert.IsNull(TransactionManager.Transaction());
		}

		[Test]
		public void Commit()
		{
			TransactionManager.Commit();
			Assert.IsNull(TransactionManager.Transaction());
		}

		[Test]
		public void Rollback()
		{
			TransactionManager.Rollback();
			Assert.IsNull(TransactionManager.Transaction());
		}

		[Test, ExpectedException(typeof(ApplicationException))]
		public void CommitNullTransaction()
		{
			TransactionManager.Commit();
			TransactionManager.Commit();
		}

		[Test,ExpectedException(typeof(ApplicationException))]
		public void BeginNewTransactionTwice()
		{
			TransactionManager.Begin(Connection);	
		}
	}
}
