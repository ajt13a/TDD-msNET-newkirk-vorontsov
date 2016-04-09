using System;
using fit;
using ServiceGateway;

namespace CustomerTests
{
	public class CatalogAdapter : Fixture 
	{
		private CatalogGateway gateway = new CatalogGateway();
		private static RecordingDto recording;

		public void FindByRecordingId(long id) 
		{
			recording = gateway.FindByRecordingId(id);
		}

		public bool Found()
		{
			return recording != null; 
		}

		public string Title()
		{
			return recording.title;
		}

		public string ArtistName()
		{
			return recording.artistName; 
		}
		
		public string ReleaseDate()
		{
			return recording.releaseDate; 
		}
		
		public string LabelName()
		{
			return recording.labelName;
		}

		public string Duration()
		{
			return recording.totalRunTime.ToString();
		}

		public int AverageRating()
		{
			return recording.averageRating;
		}

		public static TrackDto[] Tracks()
		{
			return recording.tracks;
		}

		public static ReviewDto[] Reviews()
		{
			return recording.reviews;
		}
	}
}