using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class RecordingArtistFixture : RecordingFixture
	{
		[Test]
		public void RecordingArtistId()
		{
			Assert.AreEqual(Builder.ArtistId, Recording.Artist.Id);
		}
	}
}