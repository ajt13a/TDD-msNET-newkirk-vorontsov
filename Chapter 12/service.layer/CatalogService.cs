using System;
using System.Collections;
using DataAccess;
using DataModel;

namespace ServiceLayer
{
	public class CatalogService
	{
		public RecordingDto FindByRecordingId(long id)
		{
			RecordingDataSet dataSet = new RecordingDataSet();
			RecordingDataSet.Recording recording = 
				Catalog.FindByRecordingId(dataSet, id);

			if(recording == null) return null;

			return RecordingAssembler.WriteDto(recording);
		}

		public ReviewDto AddReview(string reviewerName, string content, 
			int rating, long recordingId)
		{
			RecordingDataSet dataSet = new RecordingDataSet();
			RecordingDataSet.Review review = Catalog.AddReview(dataSet, 
				reviewerName, content, rating, recordingId);
			
			return RecordingAssembler.WriteReview(review);
		}

		public void DeleteReview(long reviewId)
		{
			Catalog.DeleteReview(reviewId);
			return;
		}

		public ArrayList Search(SearchCriteria criteria)
		{
			ArrayList searchResults = new ArrayList();

			ArrayList recordings = Catalog.Search(criteria);
			foreach(RecordingDataSet.Recording recording in recordings)
			{
				searchResults.Add(RecordingAssembler.WriteDto(recording));
			}

			return searchResults;
		}
	}
}
