using SolrNet;

namespace ITunesIndexer.Solr
{
	public interface ISolrResolver<T>
	{
		ISolrOperations<T> GetSolrOperationInstance();
	}
}