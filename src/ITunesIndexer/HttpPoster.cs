using System;
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
                if (requestStream == null)
                    return "Error: no requestStream";

                requestStream.Write(bytes, 0, bytes.Length);    

                IWebResponse webResponse = CheckWebResponse();

                Stream responseStream = CheckResponseStream(webResponse);

                return ConvertResponseToString(responseStream);
            }
        }

        private static string ConvertResponseToString(Stream responseStream)
        {
            using (var sr = new StreamReader(responseStream))
            {
                return sr.ReadToEnd().Trim();
            }
        }

        private static Stream CheckResponseStream(IWebResponse webResponse)
        {
            Stream responseStream = webResponse.GetResponseStream();
            if (responseStream == null)
                throw new Exception("Error: Response stream cannot be read");

            return responseStream;
        }

        private IWebResponse CheckWebResponse()
        {
            IWebResponse webResponse = _webRequest.GetResponse();
            if (webResponse == null)
                throw new Exception("Error: Response cannot be read");

            return webResponse;
        }
    }
}