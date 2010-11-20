using System.IO;

namespace ITunesIndexer
{
    public interface IWebResponse
    {
        Stream GetResponseStream();
    }
}