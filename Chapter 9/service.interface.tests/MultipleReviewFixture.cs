//using System;
//using System.Web.Services.Protocols;
//using System.Xml;
//using System.Xml.XPath;
//using DataAccess;
//using ServiceGateway;
//using NUnit.Framework;
//
//[TestFixture]
//public class MultipleReviewFixture : RecordingFixture
//{
//	private static readonly string reviewerName = 
//		"DatabaseUpdateReviewFixture";
//	private static readonly string reviewContent = 
//		"Fake Review Content";
//	private static readonly int    rating = 3; 
//	
//	private CatalogGateway gateway;
//	private ServiceGateway.ReviewDto review;
//
//	[Test]
//	public void AddSecondReview()
//	{
//		gateway = new CatalogGateway();
//		
//		review = gateway.AddReview(reviewerName, reviewContent, 
//			rating, Recording.Id);
//		
//		try
//		{
//			gateway.AddReview(reviewerName, reviewContent, 
//				rating, Recording.Id);
//			Assert.Fail("AddReview should have thrown an exception");
//		}
//		catch(SoapException exception)
//		{
//			Assert.AreEqual(
//				SoapException.ClientFaultCode, 
//				exception.Code);
//		}
//		finally
//		{
//			ReviewGateway reviewGateway = new ReviewGateway(Connection);
//			reviewGateway.Delete(RecordingDataSet, review.id);
//		}
//	}
//}




using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using DataAccess;
using ServiceGateway;
using NUnit.Framework;


[TestFixture]
public class MultipleReviewFixture : RecordingFixture
{
	private static readonly string reviewerName = "DatabaseUpdateReviewFixture";
	private static readonly string reviewContent = "Fake Review Content";
	private static readonly int    rating = 3; 

	private CatalogGateway gateway;
	private ServiceGateway.ReviewDto review;
	private SoapException soapException;

	[SetUp]
	public new void SetUp()
	{
		base.SetUp();

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

	[TearDown]
	public new void TearDown()
	{
		ReviewGateway reviewGateway = new ReviewGateway(Connection);
		reviewGateway.Delete(RecordingDataSet, review.id);

		base.TearDown();
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
		Assert.AreEqual("ExistingReview", 
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

