using System.IO;

namespace ITunesIndexer
{
    public interface IWebRequest
    {
        Stream GetRequestStream();
        IWebResponse GetResponse();
        void SetContentLength(long contentLength);
    }
}