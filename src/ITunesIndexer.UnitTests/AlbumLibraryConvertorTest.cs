using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
	public class AlbumLibraryConvertorTest
	{
		private IEnumerable<Song> _currentSongs;
		private IImageFinder _imageFinder;
		private IEnumerable<Album> _currentAlbums;

		[Test]
		public void Should_create_album_from_group_of_songs()
		{
			Given_a_list_of_songs_containing_an_album();
			Given_a_fake_imagefinder();
			When_converting_the_songs_to_albums();
			Then_the_number_of_albums().Should_be(2);
			Then_the_number_of_tracks_on_album(_currentAlbums.ElementAt(0)).Should_be(3);
			Then_the_number_of_tracks_on_album(_currentAlbums.ElementAt(1)).Should_be(2);
		}

		[Test]
		public void Should_fire_imagefinder_with_correct_params()
		{
			Given_a_list_of_songs_containing_an_album();
			Given_a_fake_imagefinder();
			When_converting_the_songs_to_albums();
			Song currentSong = _currentSongs.ElementAt(0);
			string toFind = string.Format("{0} {1}", currentSong.Artist, currentSong.Album);
			_imageFinder.AssertWasCalled(x=>x.FindImage(toFind));
			_imageFinder.AssertWasCalled(x => x.FindImage(Arg<string>.Is.Anything), o => o.Repeat.Times(_currentAlbums.Count()));
		}

		[Test]
		public void Should_contain_all_correct_values()
		{
			
		}

		public int Then_the_number_of_tracks_on_album(Album album)
		{
			return album.Tracks.Count();
		}

		public int Then_the_number_of_albums()
		{
			return _currentAlbums.Count();
		}

		public void Given_a_list_of_songs_containing_an_album()
		{
			var songA = new Song
			{
				Name = "Song A",
				Album = "Test Album A",
				Artist = "Test Artist A"
			};

			var songB = new Song
			{
				Name = "Song B",
				Album = "Test Album A",
				Artist = "Test Artist A"
			};

			var songC = new Song
			{
				Name = "Song C",
				Album = "Test Album A",
				Artist = "Test Artist A"
			};
			var songD = new Song
			{
				Name = "Song D",
				Album = "Test Album B",
				Artist = "Test Artist B"
			};
			var songE = new Song
			{
				Name = "Song E",
				Album = "Test Album B",
				Artist = "Test Artist B"
			};

			_currentSongs =  new List<Song>
			{
				songA,songB,songC,songD,songE
			};
		}

		private void When_converting_the_songs_to_albums()
		{
			var convertor = new AlbumLibraryConvertor(_imageFinder);

			_currentAlbums = convertor.ConvertLibrary(_currentSongs);
		}

		private void Given_a_fake_imagefinder()
		{
			_imageFinder = MockRepository.GenerateStub<IImageFinder>();
		}
	}
}
