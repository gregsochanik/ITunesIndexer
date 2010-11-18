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
            var httpPoster = new HttpPoster();
            var solrPoster = new SolrPoster<Song>(httpPoster);
            string response = solrPoster.PostToSolr(song);

            Assert.That(response.ToLower(), Contains.Substring("<response>"));

            var solrResponse = new SolrResponse(response);

            Assert.That(solrResponse, Is.Not.Null);
            Assert.That(solrResponse.Status, Is.GreaterThan(0));
        }
    }
}
