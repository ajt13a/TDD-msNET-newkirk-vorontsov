using System;
using DataModel;

namespace ServiceLayer
{
	public class RecordingAssembler
	{
		public static RecordingDto 
			WriteDto(RecordingDataSet.Recording recording)
		{
			RecordingDto dto = new RecordingDto();
			dto.id = recording.Id;
			dto.title = recording.Title;
			dto.releaseDate = recording.ReleaseDate.ToShortDateString();
			dto.artistName = recording.Artist.Name;
			dto.labelName = recording.Label.Name;

			WriteTracks(dto, recording);
			WriteTotalRuntime(dto, recording);
			
			WriteReviews(dto, recording);
			WriteAverageRating(dto, recording);

			return dto;
		}

		private static void 
			WriteTracks(RecordingDto recordingDto, 
						RecordingDataSet.Recording recording)
		{
			recordingDto.tracks = new TrackDto[recording.GetTracks().Length];
			
			int index = 0;
			foreach(RecordingDataSet.Track track in recording.GetTracks())
			{
				recordingDto.tracks[index++] = WriteTrack(track);
			}
		}

		public static TrackDto WriteTrack(RecordingDataSet.Track track)
		{			
			TrackDto trackDto = new TrackDto();
				
			trackDto.id = track.Id;
			trackDto.title = track.Title;
			
			trackDto.duration = FormatDuration(track.Duration);
			
			trackDto.genreName = track.Genre.Name;
			trackDto.artistName = track.Artist.Name;
		
			return trackDto;
		}

		public static string FormatDuration(int duration)
		{
			int minutes = duration / 60;
			int seconds = duration % 60; 

			return String.Format("{0}:{1}",
				minutes, seconds.ToString("00"));
		}

		private static void WriteTotalRuntime(RecordingDto dto, 
			RecordingDataSet.Recording recording)
		{
			int runTime = 0; 
			foreach(RecordingDataSet.Track track in recording.GetTracks())
			{
				runTime += track.Duration;
			}
		
			dto.totalRunTime = FormatDuration(runTime);
		}
		
		private static void WriteReviews(RecordingDto recordingDto, 
			RecordingDataSet.Recording recording)
		{
			recordingDto.reviews = 
				new ReviewDto[recording.GetReviews().Length];

			int index = 0;
			foreach(RecordingDataSet.Review review in recording.GetReviews())
			{
				recordingDto.reviews[index++] = WriteReview(review);
			}
		}
		
		public static ReviewDto WriteReview(RecordingDataSet.Review review)
		{			
			ReviewDto reviewDto = new ReviewDto();
		
			reviewDto.id = review.Id;
			reviewDto.reviewContent = review.Content;
			reviewDto.rating = review.Rating;
			reviewDto.reviewerName = review.Reviewer.Name;
		
			return reviewDto;
		}
		
		private static void WriteAverageRating(RecordingDto recordingDto, 
			RecordingDataSet.Recording recording)
		{
			if(recording.GetReviews().Length == 0)
			{ 
				recordingDto.averageRating = 0;
			}
			else
			{
				int totalRating = 0;
				foreach(RecordingDataSet.Review review in recording.GetReviews())
				{
					totalRating+=review.Rating;
				}
				recordingDto.averageRating = totalRating/recording.GetReviews().Length;
			}
		}
	}
}