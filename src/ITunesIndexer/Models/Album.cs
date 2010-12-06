using System.Collections.Generic;
using SolrNet.Attributes;

namespace ITunesIndexer.Models
{
	public class Album
	{
		[SolrUniqueKey("id")]
		public int AlbumId { get; set; }
		[SolrField("Name")]
		public string Name { get; set; }
		[SolrField("Tracks")]
		public List<Song> Tracks { get; set; }
		[SolrField("Genre")]
		public string Genre { get; set; }
		[SolrField("TotalTime")]
		public long TotalTime { get; set; }
		[SolrField("Image")]
		public string Image { get; set; }
	}
}
