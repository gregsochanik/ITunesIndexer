using SolrNet.Attributes;

namespace ITunesIndexer.Models
{
	public class Artist
	{
		[SolrUniqueKey("id")]
		public int ArtistId { get; set; }
		[SolrField("Name")]
		public string Name { get; set; }
		[SolrField("Image")]
		public string Image { get; set; }
	}
}
