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
                           new Song { Album = "Only By The Night", Artist = "Kings of Leon", Name = "Be Somebody" },
                           new Song { Album = "Oracular Spectacular", Artist = "MGMT", Name = "Kids" },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Evil" },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Slow Hands" },
                       };
        }

        public static Song SingleSong()
        {
            return new Song {Album = "Antics", Artist = "Interpol", Name = "Evil"};
        }
    }
}
