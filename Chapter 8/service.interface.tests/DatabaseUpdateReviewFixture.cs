using System;
using DataModel;
using ServiceInterface;
using NUnit.Framework;

[TestFixture]
public class DatabaseUpdateReviewFixture : RecordingFixture
{
	private static readonly string reviewerName = "DatabaseUpdateReviewFixture";
	private static readonly string reviewContent = "Fake Review Content";
	private static readonly int    rating = 3; 

	private CatalogService service; 

	[SetUp]
	public new void SetUp()
	{
		base.SetUp();
		service = new DatabaseCatalogService();
	}

	[TearDown]
	public new void TearDown()
	{
		base.TearDown();
	}

	[Test]
	public void AddReviewContent()
	{
		ReviewDto dto = service.AddReview(reviewerName, reviewContent, rating, Recording.Id);

		Assert.AreEqual(reviewerName, dto.reviewerName);
		Assert.AreEqual(reviewContent, dto.reviewContent);
		Assert.AreEqual(rating, dto.rating);
	}

	[Test]
	public void ReviewAddedToRecording()
	{
		int beforeCount = Recording.GetReviews().Length;
		ReviewDto dto = service.AddReview(reviewerName, reviewContent, rating, 
			Recording.Id);

		RecordingDto recordingDto = service.FindByRecordingId(Recording.Id);
		Assert.AreEqual(beforeCount+1, recordingDto.reviews.Length);
	}

	[Test]
	public void ReviewDeletedFromRecording()
	{
		int beforeCount = Recording.GetReviews().Length;

		ReviewDto dto = service.AddReview(reviewerName, reviewContent, rating, 
			Recording.Id);

		service.DeleteReview(dto.id);

		RecordingDto recordingDto = service.FindByRecordingId(Recording.Id);
		Assert.AreEqual(beforeCount, recordingDto.reviews.Length);
	}
}
