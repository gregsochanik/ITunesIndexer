using System.Collections.Generic;
using ITunesIndexer.Models;

namespace ITunesIndexer.IntegrationTests
{
    public static class TestData
    {
        public static List<Song> BasicListOfSongs()
        {
            return new List<Song>
                       {
                           new Song { Album = "Only By The Night", Artist = "Kings of Leon", Name = "Be Somebody", Id=1001 },
                           new Song { Album = "Oracular Spectacular", Artist = "MGMT", Name = "Kids", Id=1002 },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Evil", Id=1003 },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Slow Hands", Id=1004 },
                       };
        }

        public static Song SingleSong()
        {
            return new Song {Album = "Antics", Artist = "Interpol", Name = "Evil", Id= 1001};
        }
    }
}
