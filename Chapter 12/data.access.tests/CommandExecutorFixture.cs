using System;
using System.Data.SqlClient;
using DataAccess;
using NUnit.Framework;

[TestFixture]
public class CommandExecutorFixture : ConnectionFixture
{
	private CommandExecutor commandExecutor = 
		new CommandExecutor();

	private class ExecuteCommand : Command
	{
		internal int executeCount = 0;
 
		public void Execute()
		{
			executeCount++;
		}
	}

	[Test]
	public void RunOnce()
	{
		ExecuteCommand command = new ExecuteCommand();
		commandExecutor.Execute(command);
		Assert.AreEqual(1, command.executeCount);
	}

	private class TransactionCheckCommand : Command
	{
		internal SqlTransaction transaction;

		public void Execute()
		{
			transaction = TransactionManager.Transaction();
		}
	}
	
	[Test]
	public void StartTransaction()
	{
		TransactionCheckCommand command = 
			new TransactionCheckCommand();
		commandExecutor.Execute(command);

		Assert.IsNotNull(command.transaction);
		Assert.IsNull(TransactionManager.Transaction());
	}

	[Test]
	public void ParticipateInTransaction()
	{
		SqlTransaction myTransaction = TransactionManager.Begin(Connection);
		TransactionCheckCommand command = new TransactionCheckCommand();
		commandExecutor.Execute(command);
		Assert.AreSame(myTransaction, command.transaction);
		TransactionManager.Rollback();
	}

	private class ExceptionThrowingCommand : Command
	{
		public void Execute()
		{
			throw new InvalidOperationException();
		}
	}
		
	[Test]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ThrowCommand()
	{
		ExceptionThrowingCommand command = new ExceptionThrowingCommand();
		commandExecutor.Execute(command);
	}
}