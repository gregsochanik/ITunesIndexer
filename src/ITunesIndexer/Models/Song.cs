using System;

namespace ITunesIndexer.Models
{
    [Serializable]
    public class Song
    {
        public int Id { get; set; }				
        public string Album { get; set; }		
        public string Artist { get; set; }		
        public int BitRate { get; set; }		
        public string Comments { get; set; }	
        public string Composer { get; set; }	
        public string Genre { get; set; }		
        public string Kind { get; set; }		
        public string Location { get; set; }	
        public string Name { get; set; }		
        public int PlayCount { get; set; }		
        public int SampleRate { get; set; }		
        public Int64 Size { get; set; }			
        public Int64 TotalTime { get; set; }	
        public int TrackNumber { get; set; }	


    }
}
