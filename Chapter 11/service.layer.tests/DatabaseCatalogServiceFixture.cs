using System;
using ServiceLayer;
using NUnit.Framework;

namespace Database
{
	[TestFixture]
	public class DatabaseCatalogServiceFixture : RecordingFixture 
	{
		private DatabaseCatalogService service; 
		private RecordingDto dto; 

		public override void CustomizeRecording()
		{
			service = new DatabaseCatalogService();
			dto = service.FindByRecordingId(Recording.Id);
		}

		[Test]
		public void CheckId()
		{
			Assert.AreEqual(Recording.Id, dto.id);
		}

		[Test]
		public void CheckTitle()
		{
			Assert.AreEqual(Recording.Title, dto.title);
		}

		[Test]
		public void TrackCount()
		{
			Assert.AreEqual(Recording.GetTracks().Length, 
				dto.tracks.Length);
		}

		[Test]
		public void ReviewCount()
		{
			Assert.AreEqual(Recording.GetReviews().Length, 
				dto.reviews.Length);
		}

		[Test]
		public void InvalidId()
		{
			RecordingDto nullDto = service.FindByRecordingId(2); 
			Assert.IsNull(nullDto, "should be null");
		}
	}
}
