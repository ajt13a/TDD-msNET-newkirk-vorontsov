using System;
using System.Collections;

namespace WebClient
{
	public abstract class CatalogServiceGateway
	{
		public ArrayList Search(SearchCriteria criteria)
		{
			ArrayList results = new ArrayList();

			ArrayList dtos = GetDtos(criteria);
			foreach(RecordingDto dto in dtos)
			{
				RecordingDisplayAdapter adapter = 
					new RecordingDisplayAdapter(dto);
				results.Add(adapter);
			}

			return results;
		}

		protected abstract ArrayList GetDtos(SearchCriteria criteria);
	}
}
