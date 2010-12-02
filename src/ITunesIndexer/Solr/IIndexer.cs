using System.Collections.Generic;

namespace ITunesIndexer.Solr
{
	public interface IIndexer<in T>
	{
		void GenerateIndex(IEnumerable<T> itemsToIndex);
	}
}