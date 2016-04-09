using System;
using DataAccess;
using DataModel;

namespace ServiceInterface
{
	public class DatabaseCatalogService : CatalogService
	{
		protected override
			RecordingDataSet.Recording FindById(long id)
		{
			RecordingDataSet dataSet = new RecordingDataSet();
			return Catalog.FindByRecordingId(dataSet, id);
		}

		protected override RecordingDataSet.Review AddReviewToRecording(
				string reviewerName, string content, int rating, long recordingId)
		{
			RecordingDataSet dataSet = new RecordingDataSet();
			return Catalog.AddReview(dataSet, reviewerName, content, rating, recordingId);
		}

		protected override void DeleteReviewFromRecording(long reviewId)
		{
			Catalog.DeleteReview(reviewId);
			return;
		}
	}
}
