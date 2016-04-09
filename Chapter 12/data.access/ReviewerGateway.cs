using System;
using System.Data;
using System.Data.SqlClient;
using DataModel;

namespace DataAccess
{
	public class ReviewerGateway
	{
		private SqlDataAdapter adapter; 
		private SqlConnection connection;
		private SqlCommand command;
		private SqlCommandBuilder builder;

		private SqlCommand findByNameCommand;
		private SqlDataAdapter findByNameAdapter; 

		public ReviewerGateway(SqlConnection connection)
		{
			this.connection = connection;

			command = new SqlCommand("select id, name from reviewer where id = @id",
				connection);
			command.Parameters.Add("@id",SqlDbType.BigInt);
			command.Transaction = TransactionManager.Transaction();

			adapter = new SqlDataAdapter(command);
			builder = new SqlCommandBuilder(adapter);

			findByNameCommand = new 
				SqlCommand("select id, name from reviewer where name = @name", connection); 
			findByNameCommand.Parameters.Add("@name",SqlDbType.VarChar);
			findByNameCommand.Transaction = TransactionManager.Transaction();
			findByNameAdapter = new SqlDataAdapter(findByNameCommand);			
		}

		public long Insert(RecordingDataSet recordingDataSet, string reviewerName)
		{
			long reviewerId = IdGenerator.GetNextId(recordingDataSet.Reviewers.TableName, connection);
			RecordingDataSet.Reviewer reviewerRow = recordingDataSet.Reviewers.NewReviewer();
			reviewerRow.Id = reviewerId;
			reviewerRow.Name = reviewerName;
			recordingDataSet.Reviewers.AddReviewer(reviewerRow);

			adapter.Update(recordingDataSet,recordingDataSet.Reviewers.TableName);

			return reviewerId;	
		}

		public RecordingDataSet.Reviewer FindById(long reviewerId, RecordingDataSet recordingDataSet)
		{
			command.Parameters["@id"].Value = reviewerId;
			adapter.Fill(recordingDataSet,recordingDataSet.Reviewers.TableName);
			DataRow[] rows = recordingDataSet.Reviewers.Select(String.Format("id={0}",reviewerId));
			if(rows.Length<1) return null;

			RecordingDataSet.Reviewer reviewer = (RecordingDataSet.Reviewer)rows[0];
			return reviewer; 
		}

		public RecordingDataSet.Reviewer FindByName(string reviewerName, 
			RecordingDataSet recordingDataSet)
		{
			SqlCommand command = 
				new SqlCommand("select id, name from reviewer where name = @name",
				connection);
			command.Parameters.Add("@name",SqlDbType.VarChar);
			command.Parameters["@name"].Value = reviewerName;
			command.Transaction = 
				TransactionManager.Transaction();

			SqlDataAdapter adapter = new SqlDataAdapter(command);
			adapter.Fill(recordingDataSet, recordingDataSet.Reviewers.TableName);

			string selectString = 
				String.Format("name='{0}'", reviewerName);

			DataRow[] rows = 
				recordingDataSet.Reviewers.Select(selectString);
			if(rows.Length<1) return null;

			RecordingDataSet.Reviewer reviewer = (RecordingDataSet.Reviewer)rows[0];
			return reviewer; 
		}

		public void Delete(RecordingDataSet recordingDataSet, long reviewerId)
		{
			RecordingDataSet.Reviewer loadedReviewer = FindById(reviewerId, recordingDataSet);
			loadedReviewer.Delete();
			adapter.Update(recordingDataSet,recordingDataSet.Reviewers.TableName);
		}

		public void Update(RecordingDataSet recordingDataSet)
		{
			adapter.Update(recordingDataSet,recordingDataSet.Reviewers.TableName);
		}
	}
}