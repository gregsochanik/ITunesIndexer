
using System;
using System.Collections.Specialized;
using System.Xml;
using log4net;
using Sochanik.Framework.Xml;

namespace ITunesIndexer
{
    public class SolrPoster<T> : ISolrPoster<T> where T : class
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SolrPoster<T>));
        private readonly IHttpPoster _httpPoster;

        public SolrPoster(IHttpPoster httpPoster)
        {
            _httpPoster = httpPoster;
        }

        public SolrPoster(ILog logger, IHttpPoster httpPoster)
        {
            _logger = logger;
            _httpPoster = httpPoster;
        }

        public string PostToSolr(T item)
        {
            var doc = SerializationHelper<T>.Serialize(item) as XmlDocument;
            if(doc == null)
                throw new ArgumentException("item supplied could not be serialized to xml");

            string xmlToPost = doc.InnerXml;
            string response;
            try
            {
                // post song xml to solr
                response = _httpPoster.Post(xmlToPost);
                _logger.Info("Post Added");
            } 
            catch (Exception ex)
            {
                _logger.Error("There was an error");
                response = ex.Message;
            }
            return response;
        }
    }
}
