
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
            // Convert Song to XMl
            var doc = SerializationHelper<T>.Serialize(item) as XmlDocument;
            string xmlToPost = doc.InnerXml;
            string response;
            try
            {
                // post song xml to solr
                response = _httpPoster.Post(new Uri(ConfigSettings.SolrUrl), xmlToPost);
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
            byte[] bytes = Encoding.ASCII.GetBytes(string.Format("stream.body=\"{0}\"", xml));
            Stream os = null;
            webRequest.ContentLength = bytes.Length;   //Count bytes to send
            os = webRequest.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);         //Send it

            os.Close();

            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                return sr.ReadToEnd().Trim();
            }
            return null;
        }
    }

    public interface IHttpPoster
    {
        string Post(Uri location, string parameters);
    }
    
}
