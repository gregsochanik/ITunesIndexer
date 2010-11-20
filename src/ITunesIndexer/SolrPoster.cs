
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

        private NameValueCollection CreateParams(string xml)
        {
            return new NameValueCollection{};
        }
    }

    public class HttpPoster : IHttpPoster
    {
        private readonly IWebRequest _webRequest;

        public HttpPoster(IWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public string Post(string xml)
        {
            
            byte[] bytes = Encoding.ASCII.GetBytes(xml);
            _webRequest.SetContentLength(bytes.Length);
            using(Stream os = _webRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);    
                IWebResponse webResponse = _webRequest.GetResponse();
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

    public interface IWebRequest
    {
        Stream GetRequestStream();
        IWebResponse GetResponse();
        void SetContentLength(long contentLength);
    }

    public class WebRequestWrapper : IWebRequest
    {
        private readonly WebRequest _webRequest;

        public string ContentType
        {
            get { return _webRequest.ContentType; }
            set { _webRequest.ContentType = value; }
        }

        public string Method
        {
            get { return _webRequest.Method; }
            set { _webRequest.Method = value; }
        }

        public long ContentLength
        {
            get { return _webRequest.ContentLength; }
            set { _webRequest.ContentLength = value; }
        }

        public WebRequestWrapper(WebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public Stream GetRequestStream()
        {
            return _webRequest.GetRequestStream();
        }

        public IWebResponse GetResponse()
        {
            return new WebResponseWrapper(_webRequest.GetResponse());
        }

        public void SetContentLength(long contentLength)
        {
            ContentLength = contentLength;
        }
    }

    public class WebResponseWrapper : IWebResponse
    {
        private readonly WebResponse _response;

        public WebResponseWrapper(WebResponse response)
        {
            _response = response;
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }

    public interface IWebResponse
    {
        Stream GetResponseStream();
    }

    public interface IHttpPoster
    {
        string Post( string parameters);
    }
    
}
