namespace ITunesIndexer.Solr
{
    public interface ISolrPoster<in T>
    {
        string PostToSolr(T item);
    }
}