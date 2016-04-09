using System;
using System.Collections;
using ServiceLayer;

namespace WebClient
{
	public class CatalogServiceImplementation : CatalogServiceGateway
	{
		private CatalogService service = new CatalogService();

		protected override ArrayList GetDtos(SearchCriteria criteria)
		{
			return service.Search(criteria);
		}
	}
}
