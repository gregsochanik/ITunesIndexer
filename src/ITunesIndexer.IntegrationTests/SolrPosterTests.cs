using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ITunesIndexer.Configuration;
using ITunesIndexer.Http;
using ITunesIndexer.ItunesXml;
using ITunesIndexer.Models;
using ITunesIndexer.Solr;
using NUnit.Framework;

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
		public void Should_batch_add_records_to_solr()
		{
			log4net.Config.XmlConfigurator.Configure();
			// work out a batch strategy
			const int batchNumber = 1000;

			// get list of songs
			string pathToItunesLibrary = ConfigSettings.PathToXml;

			IEnumerable<Song> songs = new LibraryBuilder<Song>().BuildLibrary(pathToItunesLibrary);
			songs = songs.Where(x => !string.IsNullOrEmpty(x.Artist) && !string.IsNullOrEmpty(x.Album));
			var indexer = new BatchedIndexer<Song>(new SolrCastleResolver<Song>()) {BatchBy = batchNumber};

			indexer.GenerateIndex(songs);
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
