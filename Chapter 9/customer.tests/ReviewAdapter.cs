using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
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
		private long existingReviewId;
      
		public void AddReview(string nothing)
		{
			try
			{
				review = Gateway.AddReview(name, content, rating, Recording.id);
			}
			catch(SoapException soapException)
			{
				review = null;
				existingReviewId = Int64.Parse(
					XPathQuery(soapException.Detail, "//existing-id"));
			}
		}

		public void DeleteReview(string reviewerName)
		{
			Gateway.DeleteReview(review.id);
		}

		public long ExistingReviewId()
		{ return existingReviewId; }

		public bool ReviewAdded()
		{ return (review != null); }

		public void SetReviewerName(string name)
		{ this.name = name; }

		public void SetRating(int rating)
		{ this.rating = rating; }

		public void SetContent(string content)
		{ this.content = content; }

		private string XPathQuery(XmlNode node, string expression)
		{
			XPathNavigator navigator = node.CreateNavigator();
         
			string selectExpr = expression;
			navigator.MoveToRoot();
			XPathExpression expr = navigator.Compile(selectExpr);
         
			XPathNodeIterator index = 
				navigator.Select(expr);

			index.MoveNext();

			return index.Current.Value.Trim(); 
		}
	}
}

