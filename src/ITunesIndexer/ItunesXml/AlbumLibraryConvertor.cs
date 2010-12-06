using System.Collections.Generic;
using System.Linq;
using ITunesIndexer.Models;

namespace ITunesIndexer.ItunesXml
{
	public class AlbumLibraryConvertor : ILibraryConvertor<Song, Album>
	{
		private readonly IImageFinder _imageFinder;

		public AlbumLibraryConvertor(IImageFinder imageFinder)
		{
			_imageFinder = imageFinder;
		}

		public IEnumerable<Album> ConvertLibrary(IEnumerable<Song> toConvert)
		{
			var albums = new List<Album>();
			string currentAlbum = string.Empty;
			int currentId = 10001; // TODO: This will cause problems in the future - maybe need to reindex the whole lot? Or write local manifest of ids to check
			foreach (Song song in toConvert)
			{
				if (currentAlbum == song.Album)
					continue;

				string albumName = currentAlbum;
				var artist = new Album
				             	{
				             		AlbumId = currentId,
				             		Image = _imageFinder.FindImage(albumName),
				             		Name = albumName,
				             		Genre = song.Genre,
				             		TotalTime = toConvert.Where(x => x.Album == albumName).Sum(x => x.TotalTime),
				             		Tracks = toConvert.Where(x => x.Album == albumName).ToList()
				             	};
				albums.Add(artist);
				currentAlbum = song.Artist;
				currentId += 2;
			}
			return albums;
		}
	}
}