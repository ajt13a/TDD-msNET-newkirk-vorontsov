using System;
using System.Collections;
using DataModel;
using ServiceLayer;
using NUnit.Framework;

[TestFixture]
public class StubCatalogServiceFixture
{
	private RecordingDataSet.Recording recording;
	private RecordingDto dto; 
	private StubCatalogService service; 

	[SetUp]
	public void SetUp()
	{
		recording = InMemoryRecordingBuilder.Make();
		service = new StubCatalogService(recording);
		dto = service.FindByRecordingId(recording.Id);
	}

	[Test]
	public void CheckId()
	{
		Assert.AreEqual(recording.Id, dto.id);
	}

	[Test]
	public void CheckTitle()
	{
		Assert.AreEqual(recording.Title, dto.title);
	}

	[Test]
	public void InvalidId()
	{
		RecordingDto nullDto = service.FindByRecordingId(2); 
		Assert.IsNull(nullDto, "should be null");
	}

	[Test]
	public void TrackCount()
	{
		Assert.AreEqual(recording.GetTracks().Length, 
			dto.tracks.Length);
	}
	
	[Test]
	public void ReviewCount()
	{
		Assert.AreEqual(recording.GetReviews().Length, 
			dto.reviews.Length);
	}
}