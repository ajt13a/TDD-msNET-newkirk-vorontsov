using System;
using DataAccess;
using DataModel;
using ServiceGateway;
using NUnit.Framework;

namespace Gateway
{
	[TestFixture]
	public class CatalogGatewayFixture : DatabaseFixture
	{
		private CatalogGateway gateway = new CatalogGateway(); 
		private ServiceGateway.RecordingDto dto; 

		private long artistId;
		private long labelId;
		private long recordingId;

		private RecordingGateway recordingGateway;
		private LabelGateway labelGateway;
		private ArtistGateway artistGateway;

		private RecordingDataSet recordingDataSet;
		private RecordingDataSet.Recording recording;
	
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

			recording = recordingGateway.FindById(recordingId, recordingDataSet);
			RecordingDataSet.Label label = 
				labelGateway.FindById(labelId, recordingDataSet);

			RecordingDataSet.Artist artist = 
				artistGateway.FindById(artistId, recordingDataSet);

			recording.Artist = artist;
			recording.Label = label;

			recordingGateway.Update(recordingDataSet);

			dto = gateway.FindByRecordingId(recording.Id);
		}

		[Test]
		public void Id()
		{
			Assert.AreEqual(recording.Id, dto.id);
		}

		[Test]
		public void Title()
		{
			Assert.AreEqual(recording.Title, dto.title);
		}

		[Test]
		public void TrackCount()
		{
			Assert.AreEqual(0, recording.GetTracks().Length);
			Assert.IsNull(dto.tracks);
		}

		[Test]
		public void ReviewCount()
		{
			Assert.AreEqual(0, recording.GetReviews().Length);
			Assert.IsNull(dto.reviews);
		}

		[Test]
		public void InvalidId()
		{
			ServiceGateway.RecordingDto nullDto = gateway.FindByRecordingId(2); 
			Assert.IsNull(nullDto, "should be null");
		}
	}
}
