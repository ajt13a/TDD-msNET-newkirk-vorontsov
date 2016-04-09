using System;
using ServiceLayer;

namespace WebClient
{
	public class RecordingDisplayAdapter
	{
		private RecordingDto dto;

		public RecordingDisplayAdapter(RecordingDto dto)
		{
			this.dto = dto;
		}

		public string Title
		{ 
			get { return dto.title; } 
		}

		public string ArtistName
		{
			get { return dto.artistName; } 
		}

		public string LabelName
		{
			get { return dto.labelName; } 
		}

		public int AverageRating
		{
			get { return dto.averageRating; } 
		}

		public long Id
		{
			get { return dto.id; }
		}
	}
}
