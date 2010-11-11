using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Sochanik.Framework.Xml;

namespace ITunesIndexer
{
    public class LibraryBuilder<T> : ILibraryBuilder<T> where T : class
    {
        public IEnumerable<T> BuildLibrary(string pathToXml)
        {
            IEnumerable<XElement> items = new LibraryParser().ParseXml(pathToXml);
            return BuildLibrary(items);
        }

        public IEnumerable<T> BuildLibrary(IEnumerable<XElement> items)
        {
            return items.Select(x => SerializationHelper<T>.Deserialize(x.ToXmlDocument()));
        }
    }
}