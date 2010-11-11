using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using ITunesIndexer.Models;
using NUnit.Framework;
using System.Linq;

namespace ITunesIndexer.UnitTests
{
    public class XNodeExtensionsTests
    {
        [Test]
        public void SuccessToXmlDocument()
        {
            var xNode = new XElement(XName.Get("test"));
            XmlDocument navigable = xNode.ToXmlDocument();
            Assert.IsNotNull(navigable);
            Assert.AreEqual("<test />", navigable.OuterXml);
        }
    }

    public class SongSelectorTests
    {
        [Test]
        public void Should_return_1_song_based_on_name()
        {
            var librarybuilder = new LibraryBuilder<Song>();
            var selector = new SongSelector(librarybuilder);

            Song song = selector.FindSingle(x => x.Name == "Come Ray And Come Charles");

            Assert.IsNotNull(song);
            Assert.AreEqual("Come Ray And Come Charles", song.Name);
        }

        [Test]
        public void Should_return_songs_based_on_bitrate()
        {
            var librarybuilder = new LibraryBuilder<Song>();
            var selector = new SongSelector(librarybuilder);

            IEnumerable<Song> songs = selector.FindMany(x => x.BitRate > 40000);
            IEnumerable<Song> allSongs = librarybuilder.BuildLibrary(ConfigSettings.PathToXml);

            Assert.Greater(1, songs.Count());

            Assert.Less(songs.Count(), allSongs.Count());
        }
    }
}