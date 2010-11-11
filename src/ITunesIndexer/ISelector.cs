using System;
using System.Collections.Generic;
using ITunesIndexer.Models;

namespace ITunesIndexer
{
    public interface ISelector
    {
        Song FindSingle(Func<Song, bool> predicate);
        IEnumerable<Song> FindMany(Func<Song, bool> predicate);
    }
}