using System.Data.SqlClient;
using System.Collections;
using System.Threading;
using System;

namespace DataAccess
{
	public class TransactionManager
	{
		private static Hashtable transactions = new Hashtable();
		
		public static SqlTransaction Begin(SqlConnection connection)
		{
			SqlTransaction transaction = Transaction();
			
			if(transaction == null)
			{
				transaction = connection.BeginTransaction();
				transactions[Thread.CurrentThread] = transaction;
			}
			else
			{
				throw new ApplicationException("There is a transaction in progress");
			}
			
			return transaction;
		}

		public static SqlTransaction Transaction()
		{
			Thread currentThread = Thread.CurrentThread;
			SqlTransaction transaction = (SqlTransaction)transactions[currentThread];
			return transaction;
		}

		public static void Commit()
		{
			SqlTransaction transaction = Transaction();
			
			if(transaction == null)
			{
				throw new ApplicationException("No transaction in progress");
			}

			transaction.Commit();
			EndCurrentTransaction();
		}

		public static void Rollback()
		{
			SqlTransaction transaction = Transaction();
			
			if(transaction == null)
			{
				throw new ApplicationException("No transaction in progress");
			}
			
			transaction.Rollback();
			EndCurrentTransaction();
		}

		private static void EndCurrentTransaction()
		{
			transactions.Remove(Thread.CurrentThread);
		}
	}
}
