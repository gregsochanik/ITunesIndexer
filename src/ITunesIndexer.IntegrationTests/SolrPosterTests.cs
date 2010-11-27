using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using ITunesIndexer.Configuration;
using ITunesIndexer.Http;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;
using ITunesIndexer.Solr;
using NUnit.Framework;
using SolrNet;

namespace ITunesIndexer.IntegrationTests
{
    public class SolrPosterTests
    {
        private WebRequest _webRequest;
        private Song _currentSong;

        [Test]
        public void Item_is_added_to_solr_instance()
        {
            Given_a_single_song();
            Given_an_xml_web_request_to_solr_instance();

            var wrapper = new WebRequestWrapper(_webRequest);
            var httpPoster = new HttpPoster(wrapper);
            var solrPoster = new SolrPoster<Song>(httpPoster);

            string response = solrPoster.PostToSolr(_currentSong);

            Assert.That(response.ToLower(), Contains.Substring("<response>"));

            var solrResponse = new SolrResponse(response);

            Assert.That(solrResponse, Is.Not.Null);
            Assert.That(solrResponse.Status, Is.EqualTo(0));
        }

		[Test]
		[Category("Spike")]
		public void Should_add_to_solr_using_solrnet()
		{
			Given_a_single_song();
			
			var solrInstance = new SolrCastleResolver<Song>().GetSolrOperationInstance();
			var response = solrInstance.Add(TestData.BasicListOfSongs());
			Assert.That(response.Status, Is.EqualTo(0));

			response = solrInstance.Commit();
			Assert.That(response.Status, Is.EqualTo(0));

			ISolrQueryResults<Song> solrQueryResults = solrInstance.Query("Artist:Kings");

			Assert.That(solrQueryResults.Count, Is.GreaterThan(0));

			response = solrInstance.Delete(TestData.BasicListOfSongs());
			Assert.That(response.Status, Is.EqualTo(0));

			response = solrInstance.Commit();
			Assert.That(response.Status, Is.EqualTo(0));

			solrQueryResults = solrInstance.Query("Artist:Kings");
			Assert.That(solrQueryResults.Count, Is.EqualTo(0));
		}

		[Test]
		[Category("Spike")]
		public void Should_batch_add_records_to_solr()
		{
			// work out a batch strategy
			const int batchNumber = 10;

			// get list of songs
			string pathToItunesLibrary = ConfigSettings.PathToXml;

			IEnumerable<Song> songs = new LibraryBuilder<Song>().BuildLibrary(pathToItunesLibrary);

			int counter = 0;
			int numberOfSongs = songs.Count();
			int numberOfBatches = numberOfSongs / batchNumber;

			var solrInstance = new SolrCastleResolver<Song>().GetSolrOperationInstance();

			// for each batch
			for (int i = 0; i < numberOfBatches; i++)
			{
				int start = counter;
				// get list of songs
				IEnumerable<Song> songBatch = songs.Skip(start).Take(batchNumber);
				// add batch to Solr
				Console.WriteLine("Adding {0} through to {1}", start, start + songBatch.Count()-1);
				var response = solrInstance.Add(songBatch);
				Assert.That(response.Status, Is.EqualTo(0));

				// Commit
				Console.WriteLine("Committing batch");
				response = solrInstance.Commit();
				Assert.That(response.Status, Is.EqualTo(0));
				counter += batchNumber;
			}
			Console.WriteLine("Optimizing index");
			solrInstance.Optimize();
			Console.Read();
		}



        private void Given_a_single_song()
        {
            _currentSong = TestData.SingleSong();
        }

        private void Given_an_xml_web_request_to_solr_instance()
        {
            _webRequest = WebRequest.Create(new Uri(ConfigSettings.SolrUrl + @"/update"));
            _webRequest.ContentType = "text/xml";
            _webRequest.Method = "POST";
        }
    }

    
}
