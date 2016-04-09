using System;
using DataAccess;
using DataModel;
using NUnit.Framework;

[TestFixture]
public class ReviewUpdateFixture : RecordingFixture
{
	private static readonly string reviewerName = "ReviewUpdateFixture"; 
	
	private RecordingDataSet recordingDataSet = new RecordingDataSet();
	private RecordingDataSet.Recording loadedRecording;
	
	[SetUp]
	public new void SetUp()
	{
		base.SetUp();

		RecordingDataSet loadedDataSet = new RecordingDataSet();
		loadedRecording = Catalog.FindByRecordingId(loadedDataSet, Recording.Id);
	}

	[TearDown]
	public new void TearDown()
	{
		base.TearDown();
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

		RecordingDataSet.Review review = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, Recording.Id);
		Assert.IsNotNull(review);

		RecordingDataSet loadedFromDBDataSet = new RecordingDataSet();
		RecordingDataSet.Recording loadedFromDBRecording = Catalog.FindByRecordingId(loadedFromDBDataSet, Recording.Id);
		Assert.AreEqual(1, loadedFromDBRecording.GetReviews().Length);

		RecordingDataSet.Review loadedFromDBReview = 
			loadedFromDBRecording.GetReviews()[0];

		ReviewGateway reviewGateway = new ReviewGateway(Connection);
		reviewGateway.Delete(loadedFromDBDataSet, loadedFromDBReview.Id); 
		reviewerGateway.Delete(recordingDataSet, reviewerId);
	}

	[Test]
	public void AddReviewWithoutExistingReviewer()
	{
		int rating = 1;
		string content = "Review content";

		RecordingDataSet.Review review = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, Recording.Id);
		Assert.IsNotNull(review);

		RecordingDataSet loadedFromDBDataSet = new RecordingDataSet();
		RecordingDataSet.Recording loadedFromDBRecording = Catalog.FindByRecordingId(loadedFromDBDataSet, Recording.Id);
		Assert.AreEqual(1, loadedFromDBRecording.GetReviews().Length);

		RecordingDataSet.Review loadedFromDBReview = 
			loadedFromDBRecording.GetReviews()[0];

		ReviewGateway reviewGateway = new ReviewGateway(Connection);
		reviewGateway.Delete(loadedFromDBDataSet, loadedFromDBReview.Id); 

		ReviewerGateway ReviewerGateway = 
			new ReviewerGateway(Connection);
		long reviewerId = review.ReviewerId;
		ReviewerGateway.Delete(recordingDataSet, reviewerId);
	}

	[Test]
	public void DeleteReview()
	{
		int rating = 1;
		string content = "Review content";

		ReviewerGateway ReviewerGateway = 
			new ReviewerGateway(Connection);
		long reviewerId = ReviewerGateway.Insert(recordingDataSet, reviewerName);
		RecordingDataSet.Reviewer reviewer = 
			ReviewerGateway.FindById(reviewerId, recordingDataSet);

		RecordingDataSet.Review review = Catalog.AddReview(recordingDataSet, reviewerName, content, rating, Recording.Id);
		Catalog.DeleteReview(review.Id);
      
		RecordingDataSet loadedFromDB = new RecordingDataSet();
		RecordingDataSet.Recording loadedFromDBRecording = Catalog.FindByRecordingId(loadedFromDB, Recording.Id);
		Assert.AreEqual(0, loadedFromDBRecording.GetReviews().Length);
	}

	[Test]
	public void AddTwoReviewsWithExistingReviewer()
	{
		int rating = 1;
		string content = "Review content";

		ReviewerGateway reviewerGateway = 
			new ReviewerGateway(Connection);
		long reviewerId = 
			reviewerGateway.Insert(recordingDataSet, reviewerName);
		RecordingDataSet.Reviewer reviewer = 
			reviewerGateway.FindById(reviewerId, recordingDataSet);

		RecordingDataSet.Review reviewOne = 
			Catalog.AddReview(recordingDataSet, reviewerName, 
			content, rating, Recording.Id);

		try
		{
			RecordingDataSet.Review reviewTwo = 
				Catalog.AddReview(recordingDataSet, 
				reviewerName, content, rating, Recording.Id);
			Assert.Fail("Expected an ExistingReviewException");
		}
		catch(ExistingReviewException exception)
		{
			Assert.AreEqual(reviewOne.Id, exception.ExistingId);
		}
		finally
		{
			RecordingDataSet dbDataSet = new RecordingDataSet();
			RecordingDataSet.Recording dbRecording = 
				Catalog.FindByRecordingId(dbDataSet,Recording.Id);
			RecordingDataSet.Review[] reviews = 
				dbRecording.GetReviews();

			ReviewGateway reviewGateway = new ReviewGateway(Connection);
			foreach(RecordingDataSet.Review existingReview in reviews)
			{
				reviewGateway.Delete(dbDataSet, existingReview.Id); 
			}
			reviewerGateway.Delete(recordingDataSet, reviewerId);
		}
	}
}

