using System;
using ServiceGateway;
using NUnit.Framework;

[TestFixture]
public class CatalogGatewayFixture : RecordingFixture
{
	private CatalogGateway gateway = new CatalogGateway(); 
	private ServiceGateway.RecordingDto dto; 

	[SetUp]
	public new void SetUp()
	{
		base.SetUp();
		dto = gateway.FindByRecordingId(Recording.Id);
	}

	[Test]
	public void Id()
	{
		Assert.AreEqual(Recording.Id, dto.id);
	}

	[Test]
	public void Title()
	{
		Assert.AreEqual(Recording.Title, dto.title);
	}

	[Test]
	public void TrackCount()
	{
		Assert.AreEqual(0, Recording.GetTracks().Length);
		Assert.IsNull(dto.tracks);
	}

	[Test]
	public void ReviewCount()
	{
		Assert.AreEqual(0, Recording.GetReviews().Length);
		Assert.IsNull(dto.reviews);
	}

	[Test]
	public void InvalidId()
	{
		ServiceGateway.RecordingDto nullDto = gateway.FindByRecordingId(2); 
		Assert.IsNull(nullDto, "should be null");
	}
}
