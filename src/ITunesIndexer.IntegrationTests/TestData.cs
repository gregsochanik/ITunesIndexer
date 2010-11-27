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
                           new Song { Album = "Only By The Night", Artist = "Kings of Leon", Name = "Be Somebody", TrackID=1001 },
                           new Song { Album = "Oracular Spectacular", Artist = "MGMT", Name = "Kids", TrackID=1002 },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Evil", TrackID=1003 },
                           new Song { Album = "Antics", Artist = "Interpol", Name = "Slow Hands", TrackID=1004 },
                       };
        }

        public static Song SingleSong()
        {
            return new Song {Album = "Antics", Artist = "Interpol", Name = "Evil", TrackID= 1001, BitRate=320};
        }
    }
}
