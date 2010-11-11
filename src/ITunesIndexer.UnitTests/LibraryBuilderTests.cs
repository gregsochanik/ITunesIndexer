using System.Collections.Generic;
using System.Linq;
using ITunesIndexer.Models;
using NUnit.Framework;

namespace ITunesIndexer.UnitTests
{
    public class LibraryBuilderTests
    {
        [Test]
        [Category("Integration")]
        public void Should_return_list_of_type_song()
        {
            string pathToItunesLibrary = ConfigSettings.PathToXml;

            IEnumerable<Song> songs = new LibraryBuilder<Song>().BuildLibrary(pathToItunesLibrary);

            Assert.IsNotNull(songs);
            int count = songs.Count();
            Assert.Greater(count, 0);

            var song = songs.First();

            Assert.IsNotNull(song);

            Assert.IsInstanceOfType(typeof(Song), song);
        }

    }
}