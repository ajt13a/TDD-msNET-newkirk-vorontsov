using System;

namespace ServerExceptions
{
	public class ExistingReviewException : ApplicationException
	{
		private long id; 

		public ExistingReviewException(long existingId)
		{
			id = existingId; 
		}

		public long Id
		{
			get 
			{ return id; }
		}
	}
}

