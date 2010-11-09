using System.Collections.Generic;
using System.Xml.Linq;

namespace ITunesIndexer
{
    public interface ILibraryParser
    {
        IEnumerable<XElement> ParseXml(string pathToXml);
    }
}