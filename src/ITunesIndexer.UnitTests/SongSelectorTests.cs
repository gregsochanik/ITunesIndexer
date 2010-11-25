using System.Collections.Generic;
using System.Linq;
using ITunesIndexer.Controllers;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
    public class SongSelectorTests
    {
        private ILibraryBuilder<Song> _libraryBuilderStub;
        
        [SetUp]
        public void Setup()
        {
            _libraryBuilderStub = MockRepository.GenerateStub<ILibraryBuilder<Song>>();
        }


        [Test]
        public void Should_return_1_song()
        {
            const string expectedSong = "Be Somebody";
            
            Given_a_library_builder_with_multiple_songs();

            var selector = new SongSelector(_libraryBuilderStub);
            Song song = selector.FindSingle(x => x.Name == expectedSong);

            Assert.IsNotNull(song);
            Assert.AreEqual(expectedSong, song.Name);
        }

        private void Given_a_library_builder_with_multiple_songs()
        {
            var stubbedSongs = TestData();
            _libraryBuilderStub.Stub(x => x.BuildLibrary(Arg<string>.Is.Anything)).Return(stubbedSongs);
        }

        [Test]
        public void Should_return_many_songs()
        {
            const string expected = "Interpol";

            Given_a_library_builder_with_multiple_songs();

            var selector = new SongSelector(_libraryBuilderStub);
            IEnumerable<Song> songs = selector.FindMany(x => x.Artist == "Interpol");

            Assert.IsNotEmpty(songs.ToList());
            
            Song first = songs.First();
            
            Assert.IsNotNull(first);
            Assert.AreEqual(expected, first.Artist);
        }

        [Test]
        public void Should_return_null_if_no_songs_found()
        {
            Given_a_library_builder_with_no_songs();

            var selector = new SongSelector(_libraryBuilderStub);

            Song single = selector.FindSingle(x => x.Artist=="Never found");

            Assert.IsNull(single);
        }

        private void Given_a_library_builder_with_no_songs()
        {
            var stubbedSongs = new List<Song>();
            _libraryBuilderStub.Stub(x => x.BuildLibrary(Arg<string>.Is.Anything)).Return(stubbedSongs);
        }

        

        public static List<Song> TestData()
        {
            return new List<Song>
                       {
                           new Song { Album = "Only By The Night", Artist = "Kings of Leon", Name = "Be Somebody" },
                           new Song { Album = "Oracular Spectacular", Artist = "MGMT", Name = "Kids" },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Evil" },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Slow Hands" },
                       };
        }
    }
}