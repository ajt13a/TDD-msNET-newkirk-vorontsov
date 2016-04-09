using DataAccessLayer;
using NUnit.Framework;

[TestFixture]
public abstract class RecordingFixture : ConnectionFixture
{
	private RecordingBuilder builder = new RecordingBuilder();
	private RecordingDataSet dataSet;
	private RecordingDataSet.Recording recording;

	[SetUp]
	public void SetUp()
	{
		dataSet = builder.Make(Connection);
		recording = dataSet.Recordings[0];
	}

	[TearDown]
	public void TearDown()
	{		
		builder.Delete(dataSet);
	}

	public RecordingBuilder Builder
	{
		get { return builder; }
	}

	public RecordingDataSet.Recording Recording
	{
		get { return recording; }
	}

	public RecordingDataSet RecordingDataSet
	{
		get { return dataSet; }
	}
}