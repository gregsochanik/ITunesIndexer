using System.Collections.Generic;
using ITunesIndexer.Models;

namespace ITunesIndexer.ItunesXml
{
	public class ArtistLibraryConvertor : ILibraryConvertor<Song, Artist>
	{
		private readonly IImageFinder _imageFinder;

		public ArtistLibraryConvertor(IImageFinder imageFinder)
		{
			_imageFinder = imageFinder;
		}

		public IEnumerable<Artist> ConvertLibrary(IEnumerable<Song> toConvert)
		{
			var artists = new List<Artist>();
			string currentArtist = string.Empty;
			int currentId = 10001; // TODO: This will cause problems in the future - maybe need to reindex the whole lot? Or write local manifest of ids to check
			foreach (Song song in toConvert)
			{
				if (currentArtist == song.Artist)
					continue;

				var artist = new Artist
				             	{
				             		ArtistId = currentId,
				             		Image = _imageFinder.FindImage(song.Artist),
				             		Name = song.Artist
				             	};
				artists.Add(artist);
				currentArtist = song.Artist;
				currentId += 2;
			}
			return artists;
		}
	}
}