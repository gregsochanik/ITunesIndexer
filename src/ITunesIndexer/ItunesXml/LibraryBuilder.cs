using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Sochanik.Framework.Xml;

namespace ITunesIndexer.ItunesXml
{
    public class LibraryBuilder<T> : ILibraryBuilder<T> where T : class
    {
        private readonly ILibraryParser _libraryParser;

        public LibraryBuilder() : this(new LibraryParser())
        {}

        public LibraryBuilder(ILibraryParser libraryParser)
        {
            _libraryParser = libraryParser;
        }

        public IEnumerable<T> BuildLibrary(string pathToXml)
        {
            IEnumerable<XElement> items = _libraryParser.ParseXml(pathToXml);
            return BuildLibrary(items);
        }

        public IEnumerable<T> BuildLibrary(IEnumerable<XElement> items)
        {
            return items.Select(x => SerializationHelper<T>.Deserialize(x.ToXmlDocument()));
        }
    }
}