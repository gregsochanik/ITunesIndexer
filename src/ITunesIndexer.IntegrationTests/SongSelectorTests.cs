using System.Collections.Generic;
using System.Linq;
using ITunesIndexer.Configuration;
using ITunesIndexer.Controllers;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;
using NUnit.Framework;

namespace ITunesIndexer.IntegrationTests
{
    public class SongSelectorTests
    {
        [Test]
        [Category("Integration")]
        public void Should_return_1_song_based_on_name()
        {
            var librarybuilder = new LibraryBuilder<Song>();
            var selector = new SongSelector(librarybuilder);

            Song song = selector.FindSingle(x => x.Name == "Come Ray And Come Charles");

            Assert.IsNotNull(song);
            Assert.AreEqual("Come Ray And Come Charles", song.Name);
        }

        [Test]
        [Category("Integration")]
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
