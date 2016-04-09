using System;
using DataAccess;
using DataModel;
using NUnit.Framework;

namespace Entities
{
	[TestFixture]
	public class RecordingGatewayFixture : RecordingFixture
	{
		private RecordingGateway gateway; 

		public override void CustomizeRecording()
		{
			gateway = Builder.RecordingGateway;
		}

		[Test]
		public void RetrieveRecordingFromDatabase()
		{
			RecordingDataSet loadedFromDB = new RecordingDataSet();
			RecordingDataSet.Recording loadedRecording = 
				gateway.FindById(Builder.RecordingId, loadedFromDB);
			
			Assert.AreEqual(Builder.RecordingId, loadedRecording.Id);
			Assert.AreEqual(Builder.Title, loadedRecording.Title);
			Assert.AreEqual(Builder.ReleaseDate, loadedRecording.ReleaseDate);
			Assert.AreEqual(Builder.ArtistId, loadedRecording.ArtistId);
			Assert.AreEqual(Builder.LabelId, loadedRecording.LabelId);
		}

		[Test]
		public void CheckDelete()
		{
			RecordingDataSet emptyDataSet = new RecordingDataSet();

			long deletedRecordingId = gateway.Insert(emptyDataSet,
				"Deleted Title", new DateTime(1991,8,6), Builder.ArtistId, Builder.LabelId);

			gateway.Delete(emptyDataSet,deletedRecordingId);
			
			RecordingDataSet.Recording deleletedRecording = 
				gateway.FindById(deletedRecordingId, emptyDataSet);
			Assert.IsNull(deleletedRecording);
		}

		[Test]
		public void UpdateTitleFieldInRecording()
		{
			string modifiedTitle = "Modified Title";
	
			RecordingDataSet.Recording recording = Recording;
			recording.Title = modifiedTitle;
			gateway.Update(RecordingDataSet);
	
			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Recording updatedRecording = 
				gateway.FindById(Builder.RecordingId, updatedDataSet);
			Assert.AreEqual(modifiedTitle, updatedRecording.Title);
		}

		[Test]
		public void UpdateReleaseDateFieldInRecording()
		{
			DateTime modifiedDate = new DateTime(1989,5,15);
	
			RecordingDataSet.Recording recording = Recording;
			recording.ReleaseDate = modifiedDate;
			gateway.Update(RecordingDataSet);
	
			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Recording updatedRecording = 
				gateway.FindById(Builder.RecordingId, updatedDataSet);
			Assert.AreEqual(modifiedDate, updatedRecording.ReleaseDate);
		}
	}
}