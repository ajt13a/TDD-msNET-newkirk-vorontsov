using System;
using System.Collections;
using DataModel;


namespace ServiceInterface
{
	public abstract class CatalogService
	{
		public RecordingDto FindByRecordingId(long id)
		{
			RecordingDataSet.Recording recording = FindById(id);
			if(recording == null) return null;

			return RecordingAssembler.WriteDto(recording);
		}

		protected abstract 
			RecordingDataSet.Recording FindById(long recordingId);
	}
}
