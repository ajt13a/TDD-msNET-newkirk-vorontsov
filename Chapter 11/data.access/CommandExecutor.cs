using System;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess;

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
				Participate(command);
			}
			else
			{
				CreateNewAndExecute(command);
			}
		}

		private void CreateNewAndExecute(Command command)
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
			catch(Exception e)
			{
				TransactionManager.Rollback();
				throw e;
			}
			finally
			{
				connection.Close();
			}
		}

		private void Participate(Command command)
		{
			command.Execute();
		}
	}
}
