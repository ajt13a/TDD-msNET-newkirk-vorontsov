using System;
using ServiceLayer;
using WebClient;
using NUnit.Framework;

[TestFixture]
public class RecordingDisplayAdapterFixture
{
	private RecordingDto dto = new RecordingDto();
	private RecordingDisplayAdapter adapter; 

	[SetUp]
	public void SetUp()
	{
		dto.id = 42;
		dto.title = "Fake Title";
		dto.labelName = "Fake Label Name";
		dto.artistName = "Fake Artist Name";
		dto.averageRating = 5; 

		adapter = new RecordingDisplayAdapter(dto);
	}

	[Test]
	public void VerifyTitle()
	{
		Assert.AreEqual(dto.title, adapter.Title);
	}

	[Test]
	public void VerifyArtistName()
	{
		Assert.AreEqual(dto.artistName, adapter.ArtistName);
	}

	[Test]
	public void VerifyAverageRating()
	{
		Assert.AreEqual(dto.averageRating, adapter.AverageRating);
	}

	[Test]
	public void VerifyId()
	{
		Assert.AreEqual(dto.id, adapter.Id);
	}

	[Test]
	public void VerifyLabelName()
	{
		Assert.AreEqual(dto.labelName, adapter.LabelName);
	}
}
