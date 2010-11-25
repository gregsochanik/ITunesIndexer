using System.IO;

namespace ITunesIndexer.Http
{
    public interface IWebResponse
    {
        Stream GetResponseStream();
    }
}