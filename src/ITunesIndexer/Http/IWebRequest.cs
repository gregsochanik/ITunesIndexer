using System.IO;

namespace ITunesIndexer.Http
{
    public interface IWebRequest
    {
        Stream GetRequestStream();
        IWebResponse GetResponse();
        void SetContentLength(long contentLength);
    }
}