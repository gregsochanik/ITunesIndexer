using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace ITunesIndexer.IntegrationTests
{
    public class LibraryParserTests
    {
        [Test]
        [Category("Integration")]
        public void Parsing_xml_should_produce_valid_list()
        {
            string pathToItunesLibrary = ConfigSettings.PathToXml;

            IEnumerable<XElement> songs = new LibraryParser().ParseXml(pathToItunesLibrary);

            Assert.IsNotNull(songs);
            List<XElement> list = songs.ToList();
            Assert.IsNotEmpty(list);
        }
    }
}
