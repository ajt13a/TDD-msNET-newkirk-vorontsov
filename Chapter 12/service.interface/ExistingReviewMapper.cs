using System;
using System.Web.Services.Protocols;
using System.Xml;
using ServerExceptions;

namespace ServiceInterface
{
	public class ExistingReviewMapper
	{
		private static readonly string operation = "AddReview";

		public static readonly string rootElement = "detail";
		public static readonly string faultCodeElement = "fault-code";
		public static readonly string existingIdElement = "existing-id";
		public static readonly string invalidFieldElement = "invalid-field";
		public static readonly string existingReviewFault = "ExistingReview";

		private static XmlDocument Make(string faultCode, 
			string fieldName, string fieldValue)
		{
			XmlDocument document = new XmlDocument();
			XmlElement detail = document.CreateElement(rootElement);
			document.AppendChild(detail);

			XmlElement code = document.CreateElement(faultCodeElement);
			detail.AppendChild(code);
			XmlText codeNode = document.CreateTextNode(faultCode.ToString());
			code.AppendChild(codeNode);

			XmlElement field = document.CreateElement(fieldName);
			detail.AppendChild(field);
			XmlText fieldNode = document.CreateTextNode(fieldValue);
			field.AppendChild(fieldNode);

			return document;
		}

		public static SoapException Map(ExistingReviewException exception)
		{
			XmlDocument document = 
				Make(existingReviewFault, existingIdElement, 
				exception.ExistingId.ToString());

			return new SoapException(exception.Message, 
				SoapException.ClientFaultCode,
				operation, document.DocumentElement);
		}
	}
}
