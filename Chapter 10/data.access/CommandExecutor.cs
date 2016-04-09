using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class CommandExecutor
	{
		public void Execute(Command command)
		{
			bool isTransactionInProgress = 
				(TransactionManager.Transaction() != null);
			
			if(isTransactionInProgress)
			{
				command.Execute();
			}
			else
			{
				SqlConnection connection = new SqlConnection(
					ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
				connection.Open();
				TransactionManager.Begin(connection);

				try
				{
					command.Execute();
					TransactionManager.Commit();
				}
				catch(Exception exception)
				{
					TransactionManager.Rollback();
					throw exception; 
				}
				finally
				{
					connection.Close();
				}
			}
		}
	}
}