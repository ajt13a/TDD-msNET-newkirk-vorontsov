using System;
using System.Collections;
using ServiceLayer;
using WebClient;

public class CatalogServiceStub : CatalogServiceGateway
{
	protected override ArrayList GetDtos(SearchCriteria criteria)
	{
		ArrayList results = new ArrayList();

		if(criteria.id != 0)
		{
			RecordingDto dto = new RecordingDto();
			dto.id = criteria.id;

			results.Add(dto);
		}
		else if(criteria.artistName != null) 
		{
			RecordingDto dto = new RecordingDto();
			dto.artistName = criteria.artistName;
			results.Add(dto);
			results.Add(dto);
		}

		return results;
	}
}

//public class CatalogServiceStub
//{
//	public ArrayList Search(SearchCriteria criteria)
//	{
//		ArrayList results = new ArrayList();
//
//		RecordingDto dto = new RecordingDto();
//		dto.id = criteria.id;
//
//		results.Add(dto);
//
//		return results;
//	}
//}