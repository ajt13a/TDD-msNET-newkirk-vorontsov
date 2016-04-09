using DataAccessLayer;
using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class RecordingReviewsFixture : RecordingFixture
	{
		private ReviewGateway reviewGateway;
		private long reviewId;

		[SetUp]
		public new void SetUp()
		{
			base.SetUp();

			reviewGateway = new ReviewGateway(Connection);

			reviewId = reviewGateway.Insert(RecordingDataSet,1,"Review");
			RecordingDataSet.Review review = reviewGateway.FindById(reviewId, RecordingDataSet);
			review.Recording = Recording;
			reviewGateway.Update(RecordingDataSet);
		}

		[TearDown]
		public new void TearDown()
		{
			reviewGateway.Delete(RecordingDataSet,reviewId);
			base.TearDown();
		}

		[Test]
		public void Count()
		{
			Assert.AreEqual(1,Recording.GetReviews().Length);
		}

		[Test]
		public void ParentId()
		{
			foreach(RecordingDataSet.Review review in Recording.GetReviews())
			{
				Assert.AreEqual(Recording.Id, review.RecordingId);
			}
		}
	}
}