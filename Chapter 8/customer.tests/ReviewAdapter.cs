using System;
using fit; 
using ServiceGateway; 

namespace CustomerTests
{
	public class ReviewAdapter : CatalogAdapter
	{
		private string name; 
		private string content;
		private int    rating; 
		private ReviewDto review; 
      
		public void AddReview(string nothing)
		{
			review = Gateway.AddReview(name, content, rating, Recording.id);
		}

		public void DeleteReview(string reviewerName)
		{
			Gateway.DeleteReview(review.id);
		}

		public bool ReviewAdded()
		{ return (review != null); }

		public void SetReviewerName(string name)
		{ this.name = name; }

		public void SetRating(int rating)
		{ this.rating = rating; }

		public void SetContent(string content)
		{ this.content = content; }
	}
}

