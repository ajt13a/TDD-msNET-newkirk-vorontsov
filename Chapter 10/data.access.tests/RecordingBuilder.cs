using System;
using System.Data.SqlClient;
using DataAccess;
using DataModel;

public class RecordingBuilder
{
	private static readonly string title = "Title";
	private static readonly DateTime releaseDate = 
		new DateTime(1999,1,12);
	
	private long recordingId;
	private long artistId;
	private long labelId;

	private RecordingGateway recordingGateway;
	private ArtistGateway artistGateway;
	private LabelGateway labelGateway;

	public RecordingDataSet Make(SqlConnection connection)
	{
		RecordingDataSet recordingDataSet = new RecordingDataSet();

		recordingGateway = new RecordingGateway(connection);
		artistGateway = new ArtistGateway(connection);
		labelGateway = new LabelGateway(connection);
		
		artistId = artistGateway.Insert(recordingDataSet, "Artist");
		labelId = labelGateway.Insert(recordingDataSet, "Label");
		recordingId = recordingGateway.Insert(recordingDataSet,title,
			releaseDate,artistId,labelId);

		recordingGateway.FindById(recordingId, recordingDataSet);

		return recordingDataSet;
	}

	public void Delete(RecordingDataSet dataSet)
	{
		artistGateway.Delete(dataSet,artistId);
		labelGateway.Delete(dataSet,labelId);
		recordingGateway.Delete(dataSet,recordingId);
	}

	#region properties
	public long LabelId
	{
		get { return labelId; }
	}

	public long ArtistId
	{
		get { return artistId; }
	}

	public string Title
	{
		get { return title; }
	}

	public DateTime ReleaseDate
	{
		get { return releaseDate; }
	}			

	public long RecordingId
	{
		get { return recordingId; }
	}

	public RecordingGateway RecordingGateway
	{
		get { return recordingGateway; }
	}
	#endregion
}
