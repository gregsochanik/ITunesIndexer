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

				currentAlbum = song.Album;
				string finderSearch = string.Format("{0} {1}", song.Artist, currentAlbum);
				var artist = new Album
				             	{
				             		AlbumId = currentId,
									Image = _imageFinder.FindImage(finderSearch),
									Name = currentAlbum,
				             		Genre = song.Genre,
									TotalTime = toConvert.Where(x => x.Album == currentAlbum).Sum(x => x.TotalTime),
									Tracks = toConvert.Where(x => x.Album == currentAlbum).ToList()
				             	};
				albums.Add(artist);
				currentId += 2;
			}
			return albums;
		}
	}
}