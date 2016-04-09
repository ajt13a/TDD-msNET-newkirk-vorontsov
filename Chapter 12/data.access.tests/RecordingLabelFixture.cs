using NUnit.Framework;

namespace Relationships
{
	[TestFixture]
	public class RecordingLabelFixture : RecordingFixture
	{
		[Test]
		public void RecordingLabelId()
		{
			Assert.AreEqual(Builder.LabelId,Recording.Label.Id);
		}
	}
}