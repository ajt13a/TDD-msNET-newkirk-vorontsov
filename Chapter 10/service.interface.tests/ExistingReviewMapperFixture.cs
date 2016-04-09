using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using ServiceInterface;
using DataAccess;
using NUnit.Framework;

[TestFixture]
public class ExistingReviewMapperFixture
{
	private static readonly long id = 12;
	private SoapException soapException; 

	[SetUp]
	public void SetUp()
	{
		ExistingReviewException exception = new ExistingReviewException(id);
		soapException = ExistingReviewMapper.Map(exception);
	}

	[Test]
	public void ClientFaultCode()
	{
		Assert.AreEqual(SoapException.ClientFaultCode, soapException.Code);
	}

	[Test]
	public void FaultCode()
	{
		string faultCode = XPathQuery(soapException.Detail, "//fault-code");
		Assert.AreEqual(ExistingReviewMapper.existingReviewFault, faultCode);
	}

	[Test]
	public void ExistingIdField()
	{
		string existingId = XPathQuery(soapException.Detail, "//existing-id");
		Assert.AreEqual(id, Int64.Parse(existingId));
	}

	private static string XPathQuery(XmlNode node, string expression)
	{
		XPathNavigator navigator = node.CreateNavigator();
						
		string selectExpr = expression;
		navigator.MoveToRoot();
		XPathExpression expr = navigator.Compile(selectExpr);
						
		XPathNodeIterator index = 
			navigator.Select(expr);
			
		if(index.Count == 0) return null;
			
		index.MoveNext();
		return index.Current.Value.Trim(); 
	}
}

