using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DataAccess;

namespace Utilities
{
	[TestFixture]
	public class IdGeneratorFixture : DatabaseFixture
	{
		private long nextId;
		private long nextIdFromGenerator;
		private SqlCommand command; 

		public override void Insert()
		{
			command = 
				new SqlCommand("select nextId from PKSequence where tableName=@tableName",Connection);
			command.Parameters.Add("@tableName",SqlDbType.VarChar).Value="Recording";
			command.Transaction = 
				TransactionManager.Transaction();

			nextId = (long)command.ExecuteScalar();

			nextIdFromGenerator = IdGenerator.GetNextId("Recording", Connection);
		}

		[Test]
		public void GetNextIdIncrement()
		{
			Assert.AreEqual(nextId,nextIdFromGenerator);
			nextId = (long)command.ExecuteScalar();
			Assert.AreEqual(nextId, nextIdFromGenerator + 1);
		}
	}
}

