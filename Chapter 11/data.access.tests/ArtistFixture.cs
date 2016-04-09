using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using DataModel;
using NUnit.Framework;

namespace Entities
{
	[TestFixture]
	public class ArtistFixture : DatabaseFixture
	{
		private static readonly string artistName = "Artist";
		private ArtistGateway gateway; 
		private RecordingDataSet recordingDataSet;
		private long artistId; 

		public override void Insert()
		{
			recordingDataSet = new RecordingDataSet();
			gateway = new ArtistGateway(Connection);

			artistId = gateway.Insert(recordingDataSet,artistName);
		}

		[Test]
		public void RetrieveArtistFromDatabase()
		{
			RecordingDataSet loadedFromDB = new RecordingDataSet();
			RecordingDataSet.Artist loadedArtist = 
				gateway.FindById(artistId, loadedFromDB);

			Assert.AreEqual(artistId,loadedArtist.Id);
			Assert.AreEqual(artistName, loadedArtist.Name);	
		}

		[Test]
		public void DeleteArtistFromDatabase()
		{
			RecordingDataSet emptyDataSet = new RecordingDataSet();
			long deletedArtistId = gateway.Insert(emptyDataSet,"Deleted Artist");
			gateway.Delete(emptyDataSet,deletedArtistId);

			RecordingDataSet.Artist deleletedArtist = 
				gateway.FindById(deletedArtistId, emptyDataSet);
			Assert.IsNull(deleletedArtist);
		}

		[Test]
		public void UpdateArtistInDatabase()
		{
			RecordingDataSet.Artist artist = recordingDataSet.Artists[0];
			artist.Name = "Modified Name";
			gateway.Update(recordingDataSet);
   
			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Artist updatedArtist = gateway.FindById(artistId, updatedDataSet);
			Assert.AreEqual("Modified Name", updatedArtist.Name);
		}
	}
}

