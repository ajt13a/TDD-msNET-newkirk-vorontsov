using System;
using ServiceGateway;
using NUnit.Framework;

[TestFixture]
public class UpdateCatalogGatewayFixture : RecordingFixture
{
	private static readonly string reviewerName = "DatabaseUpdateReviewFixture";
	private static readonly string reviewContent = "Fake Review Content";
	private static readonly int    rating = 3; 

	private CatalogGateway gateway; 

	[SetUp]
	public new void SetUp()
	{
		base.SetUp();
		gateway = new CatalogGateway();
	}

	[TearDown]
	public new void TearDown()
	{
		base.TearDown();
	}

	[Test]
	public void AddReviewContent()
	{
		ServiceGateway.ReviewDto dto = 
			gateway.AddReview(reviewerName, reviewContent, rating, Recording.Id);

		Assert.AreEqual(reviewerName, dto.reviewerName);
		Assert.AreEqual(reviewContent, dto.reviewContent);
		Assert.AreEqual(rating, dto.rating);

		gateway.DeleteReview(dto.id);
	}

	[Test]
	public void ReviewAddedToRecording()
	{
		int beforeCount = Recording.GetReviews().Length;
		ServiceGateway.ReviewDto dto = 
			gateway.AddReview(reviewerName, reviewContent, rating, Recording.Id);

		ServiceGateway.RecordingDto recordingDto = gateway.FindByRecordingId(Recording.Id);
		Assert.AreEqual(beforeCount+1, recordingDto.reviews.Length);

		gateway.DeleteReview(dto.id);
	}

	[Test]
	public void ReviewDeletedFromRecording()
	{
		ServiceGateway.RecordingDto recordingDto = 
			gateway.FindByRecordingId(Recording.Id);
		Assert.IsNull(recordingDto.reviews);

		ServiceGateway.ReviewDto dto = 
			gateway.AddReview(reviewerName, reviewContent, rating, Recording.Id);
		gateway.DeleteReview(dto.id);

		recordingDto = gateway.FindByRecordingId(Recording.Id);
		Assert.IsNull(recordingDto.reviews);
	}
}

