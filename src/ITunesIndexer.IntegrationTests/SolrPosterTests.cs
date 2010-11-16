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
            solrPoster.PostToSolr(song);
        }
    }
    
}
