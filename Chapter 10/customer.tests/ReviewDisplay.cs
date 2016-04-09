using System;
using System.Collections;
using fit;
using ServiceGateway;

namespace CustomerTests
{
	public class ReviewDisplayAdapter
	{
		private ReviewDto dto;
		
		public ReviewDisplayAdapter(ReviewDto reviewDto)
		{
			dto = reviewDto;
		}

		public string ReviewerName()
		{
			return dto.reviewerName;
		}

		public string Content()
		{
			return dto.reviewContent;
		}

		public int Rating()
		{
			return dto.rating;
		}
	}

	public class ReviewDisplay : RowFixture 
	{
		protected override Type getTargetClass() 
		{
			return typeof(CustomerTests.ReviewDisplayAdapter);
		}

		public override object[] query() 
		{
			ReviewDto[] dtoReviews = CatalogAdapter.Reviews();
			
			ReviewDisplayAdapter[] adapters = 
				new ReviewDisplayAdapter[dtoReviews.Length];

			for(int index = 0; index < dtoReviews.Length; index++)
			{
				adapters[index] = 
					new ReviewDisplayAdapter(dtoReviews[index]);
			}

			return adapters;
		}
	}
}