using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ITunesIndexer.Models;
using NUnit.Framework;

namespace ITunesIndexer.UnitTests
{
    public class LibraryParserTests
    {
        [Test]
        public void Parsing_xml_should_produce_valid_list()
        {
            const string pathToItunesLibrary = @"F:\Users\Greg\Music\iTunes\iTunes Music Library.xml";

            IEnumerable<XElement> songs = new LibraryParser().ParseXml(pathToItunesLibrary);

            Assert.IsNotNull(songs);
            List<XElement> list = songs.ToList();
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void Should_return_list_of_type_song()
        {
            const string pathToItunesLibrary = @"F:\Users\Greg\Music\iTunes\iTunes Music Library.xml";

            IEnumerable<Song> songs = LibraryParser.GetLibraryAsSongs(pathToItunesLibrary);

            Assert.IsNotNull(songs);
            Assert.Greater(songs.Count(), 0);

            var song = songs.First();

            Assert.IsNotNull(song);

            Assert.IsInstanceOfType(typeof(Song), song);
        }

        [Test]
        public void Song_xml_should_validate_against_schema() {}

    }
}
