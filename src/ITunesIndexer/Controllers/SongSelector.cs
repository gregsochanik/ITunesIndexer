using System;
using System.Collections.Generic;
using System.Linq;
using ITunesIndexer.Configuration;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;

namespace ITunesIndexer.Controllers
{
    public class SongSelector : ISelector
    {
        private readonly ILibraryBuilder<Song> _libraryBuilder;
        private readonly IEnumerable<Song> _library;

        public SongSelector(ILibraryBuilder<Song> libraryBuilder)
        {
            _libraryBuilder = libraryBuilder;
            _library = _libraryBuilder.BuildLibrary(ConfigSettings.PathToXml);
        }

        public Song FindSingle(Func<Song, bool> predicate)
        {
            IEnumerable<Song> songList = FindMany(predicate);
            if (songList.Count() < 1)
                return null;

            return songList.First();
        }

        public IEnumerable<Song> FindMany(Func<Song, bool> predicate)
        {
            return _library.Where(predicate);
        }
    }

}
