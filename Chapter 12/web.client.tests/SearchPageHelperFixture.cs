using System;
using WebClient;
using NUnit.Framework;

[TestFixture]
public class SearchPageHelperFixture
{
	private static readonly string idText = "42";
	private static readonly string titleText = "Fake Title";
	private static readonly string artistNameText = "Fake Artist Name";
	private static readonly string averageRating = "3";
	private static readonly string labelText = "Fake Label Name";

	private SearchCriteria criteria;
	private SearchPageHelper helper = new SearchPageHelper(); 

	[SetUp]
	public void SetUp()
	{
		criteria = helper.Translate(
			idText, titleText, artistNameText, averageRating, labelText);	
	}

	[Test]
	public void VerifyId()
	{
		Assert.AreEqual(Int64.Parse(idText), criteria.id);
	}

	[Test]
	public void VerifyTitle()
	{
		Assert.AreEqual(titleText, criteria.title);
	}

	[Test]
	public void VerifyLabel()
	{
		Assert.AreEqual(labelText, criteria.labelName);
	}

	[Test]
	public void VerifyArtistName()
	{
		Assert.AreEqual(artistNameText, criteria.artistName);
	}

	[Test]
	public void VerifyAverageRating()
	{
		Assert.AreEqual(Int32.Parse(averageRating), criteria.averageRating);
	}

	[Test]
	public void IdFieldNotSpecified()
	{
		criteria = helper.Translate(
			null, titleText, artistNameText, averageRating, labelText);	
		Assert.AreEqual(0, criteria.id);
	}

	[Test]
	public void AverageRatingFieldNotSpecified()
	{
		criteria = helper.Translate(
			null, titleText, artistNameText, null, labelText);	
		Assert.AreEqual(0, criteria.averageRating);
	}

}
