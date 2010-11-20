using System.IO;
using System.Net;

namespace ITunesIndexer
{
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
}