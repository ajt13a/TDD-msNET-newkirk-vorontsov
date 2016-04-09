using DataAccessLayer;
using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class TrackGenreFixture : ConnectionFixture
	{
		private long trackId;
		private long genreId;
		private RecordingDataSet recordingDataSet;
		private GenreGateway genreGateway;
		private TrackGateway trackGateway;
		private RecordingDataSet.Track track;

		[SetUp]
		public void Create()
		{
			recordingDataSet = new RecordingDataSet();
		
			genreGateway = new GenreGateway(Connection);
			genreId = genreGateway.Insert(recordingDataSet, "Genre");
		
			trackGateway = new TrackGateway(Connection);
			trackId = trackGateway.Insert(recordingDataSet, "Title", 120);

			track = trackGateway.FindById(trackId, recordingDataSet);
		
			track.GenreId = genreId;
			trackGateway.Update(recordingDataSet);
		}

		[Test]
		public void GenreId()
		{
			Assert.AreEqual(genreId,track.Genre.Id);
		}

		[TearDown]
		public void Delete()
		{
			trackGateway.Delete(recordingDataSet, trackId);
			genreGateway.Delete(recordingDataSet, genreId);
		}
	}
}