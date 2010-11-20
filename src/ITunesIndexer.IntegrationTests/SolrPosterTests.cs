using System;
using System.Net;
using ITunesIndexer.Models;
using NUnit.Framework;

namespace ITunesIndexer.IntegrationTests
{
    public class SolrPosterTests
    {
        [Test]
        public void Item_is_added_to_solr_instance()
        {
            Song song = TestData.SingleSong();
            WebRequest request = WebRequest.Create(new Uri(ConfigSettings.SolrUrl + @"/update"));
            request.ContentType = "text/xml";
            request.Method = "POST";
            var wrapper = new WebRequestWrapper(request);
            var httpPoster = new HttpPoster(wrapper);
            var solrPoster = new SolrPoster<Song>(httpPoster);
            string response = solrPoster.PostToSolr(song);

            Assert.That(response.ToLower(), Contains.Substring("<response>"));

            var solrResponse = new SolrResponse(response);

            Assert.That(solrResponse, Is.Not.Null);
            Assert.That(solrResponse.Status, Is.EqualTo(0));
        }
    }
}
