using System;

namespace DataAccess
{
	public class ExistingReviewException : ApplicationException
	{
		private long id; 

		public ExistingReviewException(long existingId)
		{
			id = existingId; 
		}

		public long ExistingId
		{
			get 
			{ return id; }
		}
	}
}

