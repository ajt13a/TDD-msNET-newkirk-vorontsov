using System;
using System.Collections;
using WebClient;
using NUnit.Framework;

[TestFixture]
public class SearchFixture
{
	[Test]
	public void SearchById()
	{
		SearchCriteria criteria = new SearchCriteria();
		criteria.id = 42; 

		CatalogServiceStub stub = 
			new CatalogServiceStub();
		ArrayList results = stub.Search(criteria);

		Assert.AreEqual(1, results.Count);

		RecordingDisplayAdapter adapter = 
			(RecordingDisplayAdapter)results[0];
		Assert.AreEqual(criteria.id, adapter.Id);
	}

	[Test]
	public void SearchByArtistName()
	{
		SearchCriteria criteria = new SearchCriteria();
		criteria.artistName = "Fake Artist Name"; 

		CatalogServiceStub stub = 
			new CatalogServiceStub();
		ArrayList results = stub.Search(criteria);

		Assert.AreEqual(2, results.Count);

		foreach(RecordingDisplayAdapter adapter in results)
			Assert.AreEqual(criteria.artistName, adapter.ArtistName);
	}
}
