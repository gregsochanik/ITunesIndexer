
using log4net;

namespace ITunesIndexer
{
    public class SolrPoster<T> : ISolrPoster<T>
    {
        private readonly ILog _logger;

        public SolrPoster(ILog logger)
        {
            _logger = logger;
        }

        // COnvert SOng to XMl
        // cretae http post
        // post song xml to solr
    }


    public interface ISolrPoster<T>
    {
    }
}
