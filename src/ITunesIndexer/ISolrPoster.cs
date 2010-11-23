namespace ITunesIndexer
{
    public interface ISolrPoster<in T>
    {
        string PostToSolr(T item);
    }
}