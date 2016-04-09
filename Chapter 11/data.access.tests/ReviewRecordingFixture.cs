using DataAccess;
using DataModel;
using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class ReviewRecordingFixture : RecordingFixture
	{
		private ReviewGateway reviewGateway;
		private long reviewId;

		public override void CustomizeRecording()
		{
			reviewGateway = new ReviewGateway(Connection);

			reviewId = reviewGateway.Insert(RecordingDataSet, 1, "Review");
			RecordingDataSet.Review review = 
				reviewGateway.FindById(reviewId, RecordingDataSet);

			review.Recording = Recording;
			reviewGateway.Update(RecordingDataSet);
		}

		[Test]
		public void RecordingKey()
		{
			long recordingId = Builder.RecordingId;

			foreach(RecordingDataSet.Review review in 
				reviewGateway.FindByRecordingId(recordingId, RecordingDataSet))
			{
				Assert.AreEqual(recordingId, review.RecordingId);
			}
		}
	}
}