using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ITunesIndexer.Configuration;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;

namespace ITunesIndexerService
{
    class Program
    {
        static void Main(string[] args)
        {
			// work out a batch strategy
        	const int batchNumber = 100;
        	
			// get list of songs
			string pathToItunesLibrary = ConfigSettings.PathToXml;

            IEnumerable<Song> songs = new LibraryBuilder<Song>().BuildLibrary(pathToItunesLibrary);

        	int counter = 0;
        	int numberOfSongs = songs.Count();
        	int numberOfBatches = numberOfSongs/batchNumber;

        	// for each batch
			for(int i=0; i< numberOfBatches; i++)
			{
				int start = counter;
				// get list of songs
				IEnumerable<Song> songBatch = songs.Skip(start).Take(batchNumber);
				// add batch to Solr
				Console.WriteLine("Adding {0} through to{1}", start, start + batchNumber);

				counter += batchNumber;
			}

        	Console.Read();
        }
    }
}
