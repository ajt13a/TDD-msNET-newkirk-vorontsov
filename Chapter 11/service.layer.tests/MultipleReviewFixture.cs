using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using ServiceGateway;
using NUnit.Framework;

namespace Gateway
{
	[TestFixture]
	public class MultipleReviewFixture : RecordingFixture
	{
		private static readonly string reviewerName = "DatabaseUpdateReviewFixture";
		private static readonly string reviewContent = "Fake Review Content";
		private static readonly int    rating = 3; 

		private CatalogGateway gateway;
		private ServiceGateway.ReviewDto review;
		private SoapException soapException;

		public override void CustomizeRecording()
		{
			gateway = new CatalogGateway();

			review = gateway.AddReview(reviewerName, reviewContent, rating, Recording.Id);

			try
			{
				gateway.AddReview(reviewerName, reviewContent, rating, Recording.Id);
			}
			catch(SoapException exception)
			{
				soapException = exception;
			}
		}

		[Test]
		public void ExceptionThrown()
		{
			Assert.IsNotNull(soapException);
		}

		[Test]
		public void ClientFaultCode()
		{
			Assert.AreEqual(
				SoapException.ClientFaultCode, 
				soapException.Code);
		}

		[Test]
		public void DetailFaultCode()
		{
			Assert.AreEqual("ExistingReviewFault", 
				XPathQuery(soapException.Detail, "//fault-code"));
		}

		[Test]
		public void ExistingReviewId()
		{
			Assert.AreEqual(review.id,
				Int64.Parse(XPathQuery(soapException.Detail, 
				"//existing-id")));
		}


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

