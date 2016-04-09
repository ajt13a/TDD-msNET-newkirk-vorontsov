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
	public class GenreFixture : ConnectionFixture
	{
		private static readonly string genreName = "Rock";
		private GenreGateway gateway; 
		private RecordingDataSet recordingDataSet;
		private long genreId; 

		[SetUp]
		public void SetUp()
		{
			recordingDataSet = new RecordingDataSet();
			gateway = new GenreGateway(Connection);

			genreId = gateway.Insert(recordingDataSet, genreName);
		}

		[TearDown]
		public void TearDown()
		{
			gateway.Delete(recordingDataSet, genreId);
		}

		[Test]
		public void RetrieveGenreFromDatabase()
		{
			RecordingDataSet loadedFromDB = new RecordingDataSet();
			RecordingDataSet.Genre loadedGenre = 
				gateway.FindById(genreId, loadedFromDB);

			Assert.AreEqual(genreId, loadedGenre.Id);
			Assert.AreEqual(genreName, loadedGenre.Name);	
		}

		[Test]
		public void DeleteGenreFromDatabase()
		{
			RecordingDataSet emptyDataSet = new RecordingDataSet();
			long deletedGenreId = gateway.Insert(emptyDataSet,"Deleted Genre");
			gateway.Delete(emptyDataSet, deletedGenreId);

			RecordingDataSet.Genre deletedGenre = 
				gateway.FindById(deletedGenreId, emptyDataSet);
			Assert.IsNull(deletedGenre);
		}

		[Test]
		public void UpdateGenreInDatabase()
		{
			RecordingDataSet.Genre genre = recordingDataSet.Genres[0];
			genre.Name = "Modified Name";
			gateway.Update(recordingDataSet);
   
			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Genre updatedGenre = 
				gateway.FindById(genreId, updatedDataSet);
			Assert.AreEqual("Modified Name", updatedGenre.Name);
		}
	}
}


