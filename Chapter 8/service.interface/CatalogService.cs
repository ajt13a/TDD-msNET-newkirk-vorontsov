using System;
using System.Collections;
using DataModel;


namespace ServiceInterface
{
	public abstract class CatalogService
	{
		public RecordingDto FindByRecordingId(long id)
		{
			RecordingDataSet.Recording recording = FindById(id);
			if(recording == null) return null;

			return RecordingAssembler.WriteDto(recording);
		}

		public ReviewDto AddReview(string reviewerName, string content, 
			int rating, long recordingId)
		{
			RecordingDataSet.Review review = AddReviewToRecording(reviewerName, 
				content, rating, recordingId);
			
			return RecordingAssembler.WriteReview(review);
		}

		public void DeleteReview(long reviewId)
		{
			DeleteReviewFromRecording(reviewId);
		}

		protected abstract 
			RecordingDataSet.Recording FindById(long recordingId);

		protected virtual RecordingDataSet.Review AddReviewToRecording(
			string reviewerName, string content, int rating, long recordingId)
		{
			return null;
		}

		protected virtual void DeleteReviewFromRecording(long reviewId)
		{}
	}
}
