using System.Collections.Generic;

namespace ITunesIndexer.Solr
{
	public interface IBatcher<T>
	{
		IEnumerable<T> GetBatch(IEnumerable<T> itemsToIndex, int start, int position);
		int BatchBy { get; set; }
		int NumberOfBatches { get; }
		int Remainder { get; }
	}
}