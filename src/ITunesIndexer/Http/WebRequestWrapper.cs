using System.IO;
using System.Net;

namespace ITunesIndexer.Http
{
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
}