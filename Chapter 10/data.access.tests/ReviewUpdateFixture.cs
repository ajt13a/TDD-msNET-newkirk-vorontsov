using System;
using DataAccess;
using DataModel;
using NUnit.Framework;

[TestFixture]
public class ReviewUpdateFixture : DatabaseFixture
{
	private long artistId;
	private long labelId;
	private long recordingId;

	private RecordingGateway recordingGateway;
	private LabelGateway labelGateway;
	private ArtistGateway artistGateway;

	private RecordingDataSet recordingDataSet;
	private RecordingDataSet.Recording loadedRecording;
	private static readonly string reviewerName = "ReviewerUpdateFixture"; 
	
	public override void Insert()
	{
		recordingDataSet = new RecordingDataSet();

		recordingGateway = new RecordingGateway(Connection);
		labelGateway = new LabelGateway(Connection);
		artistGateway = new ArtistGateway(Connection);

		artistId = artistGateway.Insert(recordingDataSet, "Artist");
		labelId = labelGateway.Insert(recordingDataSet, "Label");
		recordingId = recordingGateway.Insert(recordingDataSet, 
			"Recording Title", new DateTime(1999,1,12), artistId, labelId);

		RecordingDataSet.Recording recording = 
			recordingGateway.FindById(recordingId, recordingDataSet);


		RecordingDataSet.Label label = 
			labelGateway.FindById(labelId, recordingDataSet);

		RecordingDataSet.Artist artist = 
			artistGateway.FindById(artistId, recordingDataSet);

		recording.Artist = artist;
		recording.Label = label;

		recordingGateway.Update(recordingDataSet);

		RecordingDataSet loadedDataSet = new RecordingDataSet();
		loadedRecording = Catalog.FindByRecordingId(loadedDataSet,recordingId);
	}

	[Test]
	public void AddReviewWithExistingReviewer()
	{
		int rating = 1;
		string content = "Review content";

		ReviewerGateway reviewerGateway = 
			new ReviewerGateway(Connection);
		long reviewerId = reviewerGateway.Insert(recordingDataSet, reviewerName);
		RecordingDataSet.Reviewer reviewer = 
			reviewerGateway.FindById(reviewerId, recordingDataSet);

		RecordingDataSet.Review review = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, recordingId);
		Assert.IsNotNull(review);

		RecordingDataSet loadedFromDBDataSet = new RecordingDataSet();
		RecordingDataSet.Recording loadedFromDBRecording = Catalog.FindByRecordingId(loadedFromDBDataSet,recordingId);
		Assert.AreEqual(1, loadedFromDBRecording.GetReviews().Length);
	}

	[Test]
	public void AddReviewWithoutExistingReviewer()
	{
		int rating = 1;
		string content = "Review content";

		RecordingDataSet.Review review = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, recordingId);
		Assert.IsNotNull(review);

		RecordingDataSet loadedFromDBDataSet = new RecordingDataSet();
		RecordingDataSet.Recording loadedFromDBRecording = Catalog.FindByRecordingId(loadedFromDBDataSet,recordingId);
		Assert.AreEqual(1, loadedFromDBRecording.GetReviews().Length);
	}

	[Test]
	public void DeleteReview()
	{
		int rating = 1;
		string content = "Review content";

		ReviewerGateway reviewerGateway = 
			new ReviewerGateway(Connection);
		long reviewerId = reviewerGateway.Insert(recordingDataSet, reviewerName);
		RecordingDataSet.Reviewer reviewer = 
			reviewerGateway.FindById(reviewerId, recordingDataSet);

		RecordingDataSet.Review review = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, recordingId);
		Catalog.DeleteReview(review.Id);
		
		RecordingDataSet loadedFromDB = new RecordingDataSet();
		RecordingDataSet.Recording loadedFromDBRecording = Catalog.FindByRecordingId(loadedFromDB,recordingId);
		Assert.AreEqual(0, loadedFromDBRecording.GetReviews().Length);
	}

	[Test]
	public void AddTwoReviewsWithExistingReviewer()
	{
		int rating = 1;
		string content = "Review content";

		ReviewerGateway reviewerGateway = new ReviewerGateway(Connection);
		long reviewerId = reviewerGateway.Insert(recordingDataSet, reviewerName);
		RecordingDataSet.Reviewer reviewer = 
			reviewerGateway.FindById(reviewerId, recordingDataSet);

		RecordingDataSet.Review reviewOne = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, recordingId);

		try
		{
			RecordingDataSet.Review reviewTwo = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, recordingId);
			Assert.Fail("Expected an ExistingReviewException");
		}
		catch(ExistingReviewException exception)
		{
			Assert.AreEqual(reviewOne.Id, exception.ExistingId);
		}
	}
}
