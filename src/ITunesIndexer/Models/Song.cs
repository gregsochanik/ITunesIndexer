using System;
using SolrNet.Attributes;

namespace ITunesIndexer.Models
{
	[Serializable]
	public class Song
    {
		[SolrUniqueKey("id")]
        public int TrackID { get; set; }		
		[SolrField("Album")]
        public string Album { get; set; }
		[SolrField("Artist")]
        public string Artist { get; set; }
		[SolrField("BitRate")]
        public int BitRate { get; set; }
		[SolrField("Comments")]
        public string Comments { get; set; }
		[SolrField("Composer")]
        public string Composer { get; set; }
		[SolrField("Genre")]
		public string Genre { get; set; }
		[SolrField("Kind")]
        public string Kind { get; set; }
		[SolrField("Location")]
        public string Location { get; set; }
		[SolrField("Name")]
        public string Name { get; set; }
		[SolrField("PlayCount")]
        public int PlayCount { get; set; }
		[SolrField("SampleRate")]
        public int SampleRate { get; set; }
		[SolrField("Size")]
        public long Size { get; set; }
		[SolrField("TotalTime")]	
        public long TotalTime { get; set; }
		[SolrField("TrackNumber")]
        public int TrackNumber { get; set; }	
    }
}
