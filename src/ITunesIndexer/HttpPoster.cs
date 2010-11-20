using System.IO;
using System.Text;

namespace ITunesIndexer
{
    public class HttpPoster : IHttpPoster
    {
        private readonly IWebRequest _webRequest;

        public HttpPoster(IWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public string Post(string content)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(content);
            _webRequest.SetContentLength(bytes.Length);

            using(Stream requestStream = _webRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);    

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
}