using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataModel;
using ServerExceptions;

namespace DataAccess
{
	public class Catalog
	{
		private static CommandExecutor executor = 
			new CommandExecutor();

		private class FindByRecordingIdCommand : Command
		{
			private RecordingDataSet dataSet; 
			private RecordingDataSet.Recording recording;
			private long recordingId;

			public FindByRecordingIdCommand(RecordingDataSet dataSet, 
				long recordingId)
			{
				this.dataSet = dataSet;
				this.recordingId = recordingId;
			}

			public void Execute()
			{
				SqlConnection connection = 
					TransactionManager.Transaction().Connection;
				RecordingGateway recordingGateway = 
					new RecordingGateway(connection);
				recording = recordingGateway.FindById(recordingId, dataSet);
					
				if(recording == null) return;
				long artistId = recording.ArtistId;
				ArtistGateway artistGateway = new ArtistGateway(connection);
				RecordingDataSet.Artist artist = 
					artistGateway.FindById(artistId, dataSet);

				long labelId = recording.LabelId;
				LabelGateway labelGateway = new LabelGateway(connection);
				RecordingDataSet.Label label = 
					labelGateway.FindById(labelId, dataSet);

				GenreGateway genreGateway = new GenreGateway(connection);
					
				TrackGateway trackGateway = new TrackGateway(connection);
				foreach(RecordingDataSet.Track track in 
					trackGateway.FindByRecordingId(recordingId, dataSet))
				{
					artistId = track.ArtistId;
					long genreId = track.GenreId;
					artist = artistGateway.FindById(artistId, dataSet);
					RecordingDataSet.Genre genre = 
						genreGateway.FindById(genreId, dataSet);
				}

				ReviewGateway reviewGateway = new ReviewGateway(connection);
				ReviewerGateway reviewerGateway = new ReviewerGateway(connection);
				foreach(RecordingDataSet.Review review in 
					reviewGateway.FindByRecordingId(recordingId, dataSet))
				{
					long reviewerId = review.ReviewerId;

					RecordingDataSet.Reviewer reviewer = 
						reviewerGateway.FindById(reviewerId, dataSet);
				}
			}

			public RecordingDataSet.Recording Result
			{
				get 
				{
					return recording;
				}
			}
		}

		public static RecordingDataSet.Recording FindByRecordingId(
			RecordingDataSet recordingDataSet, long recordingId)
		{
			FindByRecordingIdCommand command = 
				new FindByRecordingIdCommand(recordingDataSet, recordingId);

			executor.Execute(command);
			return command.Result;
		}


		private class AddReviewCommand : Command
		{
			private RecordingDataSet dataSet;
			private string name;
			private string content;
			private int rating;
			private long recordingId;

			private RecordingDataSet.Review review;

			public AddReviewCommand(RecordingDataSet dataSet,
				string name, string content, int rating, long recordingId)
			{
				this.dataSet = dataSet;
				this.name = name;
				this.content = content;
				this.rating = rating;
				this.recordingId = recordingId;
			}

			public void Execute()
			{
				SqlConnection connection = 
					TransactionManager.Transaction().Connection;

				RecordingDataSet.Recording recording = 
					FindByRecordingId(dataSet, recordingId);
 
				ReviewerGateway reviewerGateway = 
					new ReviewerGateway(connection);

				RecordingDataSet.Reviewer reviewer = 
					reviewerGateway.FindByName(name, dataSet);
			
				if(reviewer == null)
				{
					long reviewerId = reviewerGateway.Insert(dataSet,name);
					reviewer = reviewerGateway.FindById(reviewerId,dataSet);
				}

				foreach(RecordingDataSet.Review existingReview in recording.GetReviews())
				{
					if(existingReview.Reviewer.Name.Equals(name))
						throw new ExistingReviewException(existingReview.Id);
				}

				ReviewGateway reviewGateway = new ReviewGateway(connection);
				long reviewId = reviewGateway.Insert(dataSet, rating, content);
				review = reviewGateway.FindById(reviewId, dataSet);

				review.ReviewerId = reviewer.Id;
				review.Recording = recording;
				reviewGateway.Update(dataSet);
			}

			public RecordingDataSet.Review Result
			{
				get 
				{
					return review;
				}
			}
		}

		public static RecordingDataSet.Review AddReview(RecordingDataSet dataSet,
			string name, string content, int rating, long recordingId)
		{
			AddReviewCommand command = 
				new AddReviewCommand(dataSet, name, content, rating, recordingId);

			executor.Execute(command);

			return command.Result;
		}

		private class DeleteReviewCommand : Command
		{
			private long reviewId;

			public DeleteReviewCommand(long reviewId)
			{
				this.reviewId = reviewId;
			}

			public void Execute()
			{
				SqlConnection connection = 
					TransactionManager.Transaction().Connection;
				
				RecordingDataSet recordingDataSet = new RecordingDataSet();

				ReviewGateway reviewGateway = 
					new ReviewGateway(connection);
				reviewGateway.Delete(recordingDataSet, reviewId);
			}
		}

		public static void DeleteReview(long reviewId)
		{
			DeleteReviewCommand command = 
				new DeleteReviewCommand(reviewId);

			executor.Execute(command);

			return;
		}

		public static ArrayList Search(SearchCriteria criteria)
		{
			return new ArrayList();
		}
	}
}