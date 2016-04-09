using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataModel;

namespace DataAccess
{
	public class Catalog
	{
		public static RecordingDataSet.Recording FindByRecordingId(
			RecordingDataSet recordingDataSet, long recordingId)
		{
			SqlConnection connection = null;
			RecordingDataSet.Recording recording = null;

			try
			{
				connection = new SqlConnection(
					ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
				connection.Open();

				RecordingGateway recordingGateway = new RecordingGateway(connection);
				recording = recordingGateway.FindById(recordingId, recordingDataSet);
				if(recording != null)
				{
					long artistId = recording.ArtistId;
					ArtistGateway artistGateway = new ArtistGateway(connection);
					RecordingDataSet.Artist artist = 
						artistGateway.FindById(artistId, recordingDataSet);

					long labelId = recording.LabelId;
					LabelGateway labelGateway = new LabelGateway(connection);
					RecordingDataSet.Label label = 
						labelGateway.FindById(labelId, recordingDataSet);

					GenreGateway genreGateway = new GenreGateway(connection);
					TrackGateway trackGateway = new TrackGateway(connection);
					foreach(RecordingDataSet.Track track in 
						trackGateway.FindByRecordingId(recordingId, recordingDataSet))
					{
						artistId = track.ArtistId;
						long genreId = track.GenreId;
						artist = artistGateway.FindById(artistId, recordingDataSet);
						RecordingDataSet.Genre genre = 
							genreGateway.FindById(genreId, recordingDataSet);
					}

					ReviewGateway reviewGateway = new ReviewGateway(connection);
					ReviewerGateway reviewerGateway = new ReviewerGateway(connection);
					foreach(RecordingDataSet.Review review in 
						reviewGateway.FindByRecordingId(recordingId, recordingDataSet))
					{
						long reviewerId = review.ReviewerId;

						RecordingDataSet.Reviewer reviewer = 
							reviewerGateway.FindById(reviewerId, recordingDataSet);
					}
				}
			}
			finally
			{
				if(connection != null)
					connection.Close();
			}

			return recording;
		}

		public static RecordingDataSet.Review AddReview(RecordingDataSet dataSet,
			string name, string content, int rating, long recordingId)
		{
			SqlConnection connection = null;
			RecordingDataSet.Review review = null;

			try
			{
				connection = new SqlConnection(
					ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
				connection.Open();

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

				ReviewGateway reviewGateway = new ReviewGateway(connection);
				long reviewId = reviewGateway.Insert(dataSet, rating, content);
         
				review = reviewGateway.FindById(reviewId, dataSet);
				review.ReviewerId = reviewer.Id;
				review.Recording = recording;
				reviewGateway.Update(dataSet);
			}
			finally
			{
				if(connection != null)
					connection.Close();
			}

			return review; 
		}

		public static void DeleteReview(long reviewId)
		{
			SqlConnection connection = null;

			try
			{
				connection = new SqlConnection(
					ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
				connection.Open();

				RecordingDataSet recordingDataSet = 
					new RecordingDataSet();

				ReviewGateway reviewGateway = new ReviewGateway(connection);
				reviewGateway.Delete(recordingDataSet, reviewId);
			}
			finally
			{
				if(connection != null)
					connection.Close();
			}

			return;
		}
	}
}