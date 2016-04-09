using System;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
	public class IdGenerator
	{
		public static long GetNextId(string tableName, SqlConnection connection)
		{
			SqlCommand selectCommand = 
				new SqlCommand("select nextId from PKSequence where tableName = @tableName",connection);
			selectCommand.Parameters.Add("@tableName",SqlDbType.VarChar).Value=tableName;
			selectCommand.Transaction = 
				TransactionManager.Transaction();

			long nextId = (long)selectCommand.ExecuteScalar();

			SqlCommand updateCommand = 
				new SqlCommand("update PKSequence set nextId = @nextId where tableName=@tableName",connection);
			updateCommand.Parameters.Add("@tableName",SqlDbType.VarChar).Value=tableName;
			updateCommand.Parameters.Add("@nextId",SqlDbType.BigInt).Value=nextId+1;
			updateCommand.Transaction = 
				TransactionManager.Transaction();
			updateCommand.ExecuteNonQuery();

			return nextId;
		}
	}
}
