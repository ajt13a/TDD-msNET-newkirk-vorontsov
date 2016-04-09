using DataAccess;
using DataModel;
using NUnit.Framework;

[TestFixture]
public abstract class RecordingFixture : DatabaseFixture
{
	private RecordingBuilder builder = new RecordingBuilder();
	private RecordingDataSet dataSet;
	private RecordingDataSet.Recording recording;

	public override void Insert()
	{
		dataSet = builder.Make(Connection);
		recording = dataSet.Recordings[0];

		CustomizeRecording();
	}

	public virtual void CustomizeRecording()
	{}

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