using System.Collections.Generic;
using System.Xml.Linq;

namespace ITunesIndexer.ItunesXml
{
    public interface ILibraryBuilder<out T> where T : class
    {
        IEnumerable<T> BuildLibrary(string pathToXml);
        IEnumerable<T> BuildLibrary(IEnumerable<XElement> items);
    }
}