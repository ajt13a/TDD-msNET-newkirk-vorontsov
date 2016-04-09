using System;

namespace WebClient
{
	public class SearchPageHelper
	{
		public SearchCriteria Translate(
			string id, string title, string artistName, 
			string averageRating, string labelName)
		{
			SearchCriteria criteria = new SearchCriteria();

			try
			{
				criteria.id = Int64.Parse(id);
			}
			catch(Exception)
			{
				criteria.id = 0;
			}

			criteria.title = title;
			criteria.labelName = labelName;
			criteria.artistName = artistName;

			try
			{
				criteria.averageRating = Int32.Parse(averageRating);
			}
			catch(Exception)
			{
				criteria.averageRating = 0;
			}

			return criteria;
		}
	}
}
