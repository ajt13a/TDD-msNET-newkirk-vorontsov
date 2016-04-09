using DataAccessLayer;
using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class TrackArtistFixture : ConnectionFixture
	{
		private long trackId;
		private long artistId;

		private RecordingDataSet recordingDataSet;
		private ArtistGateway artistGateway;
		private TrackGateway trackGateway;

		private RecordingDataSet.Track track;

		[SetUp]
		public void Create()
		{
			recordingDataSet = new RecordingDataSet();

			artistGateway = new ArtistGateway(Connection);
			artistId = artistGateway.Insert(recordingDataSet, "Artist");

			trackGateway = new TrackGateway(Connection);
			trackId = trackGateway.Insert(recordingDataSet, "Title", 120);

			track = trackGateway.FindById(trackId, recordingDataSet);
		
			track.ArtistId = artistId;
			trackGateway.Update(recordingDataSet);
		}

		[Test]
		public void ArtistId()
		{
			Assert.AreEqual(artistId, track.Artist.Id);
		}

		[TearDown]
		public void Delete()
		{
			trackGateway.Delete(recordingDataSet,trackId);
			artistGateway.Delete(recordingDataSet,artistId);
		}
	}
}