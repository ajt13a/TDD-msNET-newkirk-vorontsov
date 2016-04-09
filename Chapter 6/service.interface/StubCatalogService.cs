using System;
using DataAccessLayer;

namespace ServiceInterface
{
	public class StubCatalogService : CatalogService
	{
		private RecordingDataSet.Recording recording; 

		public StubCatalogService(RecordingDataSet.Recording recording)
		{
			this.recording = recording;
		}

		protected override
			RecordingDataSet.Recording FindById(long id)
		{
			if(id != recording.Id) return null;

			return recording;
		}
	}
}
