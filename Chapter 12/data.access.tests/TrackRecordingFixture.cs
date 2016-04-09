using DataAccess;
using DataModel;
using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class TrackRecordingFixture : RecordingFixture
	{
		private TrackGateway trackGateway;
		private long trackId;

		public override void CustomizeRecording()
		{
			trackGateway = new TrackGateway(Connection);
			trackId = trackGateway.Insert(RecordingDataSet, "Track", 120);
			RecordingDataSet.Track track = trackGateway.FindById(trackId, RecordingDataSet);
			track.Recording = Recording;
			trackGateway.Update(RecordingDataSet);
		}

		[Test]
		public void Count()
		{
			Assert.AreEqual(1, Recording.GetTracks().Length);
		}

		[Test]
		public void ParentId()
		{
			foreach(RecordingDataSet.Track track in Recording.GetTracks())
			{
				Assert.AreEqual(Recording.Id, track.RecordingId);
			}
		}
	}
}