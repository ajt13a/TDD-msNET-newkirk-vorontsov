using System;
using System.Collections;
using fit;
using ServiceGateway;

namespace CustomerTests
{
	public class TrackDisplayAdapter
	{
		private TrackDto dto;
		
		public TrackDisplayAdapter(TrackDto trackDto)
		{
			dto = trackDto;
		}

		public string Title()
		{
			return dto.title;
		}

		public string Duration()
		{
			return dto.duration.ToString();
		}

		public string GenreName()
		{
			return dto.genreName;
		}

		public string ArtistName()
		{
			return dto.artistName;
		}
	}

	public class TrackDisplay : RowFixture 
	{
		protected override Type getTargetClass() 
		{
			return typeof(CustomerTests.TrackDisplayAdapter);
		}

		public override object[] query() 
		{
			TrackDto[] dtoTracks = CatalogAdapter.Tracks();
			
			TrackDisplayAdapter[] adapters = 
				new TrackDisplayAdapter[dtoTracks.Length];

			for(int index = 0; index < dtoTracks.Length; index++)
			{
				adapters[index] = 
					new TrackDisplayAdapter(dtoTracks[index]);
			}

			return adapters;
		}
	}
}