
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using log4net;
using Sochanik.Framework.Xml;

namespace ITunesIndexer
{
    public interface ISolrPoster<in T>
    {
        string PostToSolr(T item);
    }

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
            string xmlToPost = doc.InnerXml;
            string response;
            try
            {
                // post song xml to solr
                response = _httpPoster.Post(new Uri(ConfigSettings.SolrUrl+@"/update"), xmlToPost);
                _logger.Info("Post Added");
            } 
            catch (Exception ex)
            {
                _logger.Error("There was an error");
                response = ex.Message;
            }
            return response;
        }

        private NameValueCollection CreateParams(string xml)
        {
            return new NameValueCollection{};
        }
    }

    public class HttpPoster : IHttpPoster
    {
        public string Post(Uri location, string xml)
        {
            WebRequest webRequest = WebRequest.Create(location);
            webRequest.ContentType = "text/xml";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(xml);
            webRequest.ContentLength = bytes.Length;
            using(Stream os = webRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);    
                WebResponse webResponse = webRequest.GetResponse();
                if (webResponse == null)
                    return "Error: Response cannot be read";

                Stream responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                    return "Error: Response stream cannot be read";

                using (var sr = new StreamReader(responseStream))
                {
                    return sr.ReadToEnd().Trim();
                }
            }
        }
    }

    public interface IHttpPoster
    {
        string Post(Uri location, string parameters);
    }
    
}
