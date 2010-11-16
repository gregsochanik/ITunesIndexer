
using System;
using System.Xml;
using log4net;
using Sochanik.Framework.Xml;

namespace ITunesIndexer
{
    public interface ISolrPoster<in T>
    {
        void PostToSolr(T item);
    }

    public class SolrPoster<T> : ISolrPoster<T> where T : class
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SolrPoster<T>));
        private readonly IXmlPoster _xmlPoster;

        public SolrPoster(ILog logger, IXmlPoster xmlPoster)
        {
            _logger = logger;
            _xmlPoster = xmlPoster;
        }

        public void PostToSolr(T item)
        {
            // Convert Song to XMl
            var doc = SerializationHelper<T>.Serialize(item) as XmlDocument;
            string xmlToPost = doc.InnerXml;

            try
            {
                // post song xml to solr
                _xmlPoster.PostXml(xmlToPost, new Uri("test"));
                _logger.Info("Post Added");
            } 
            catch (Exception ex)
            {
                _logger.Error("There was an error");
            }
        }
    }

    public class XmlPoster : IXmlPoster
    {
        public void PostXml(string xml, Uri location)
        {
            // create http post
        }
    }

    public interface IXmlPoster
    {
        void PostXml(string xml, Uri location);
    }

    
}
