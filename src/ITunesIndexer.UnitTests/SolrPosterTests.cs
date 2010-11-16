using System;
using ITunesIndexer.Models;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
    public class SolrPosterTests
    {
        [Test]
        public void Posting_file_should_log_info_if_success()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var xmlPoster = MockRepository.GenerateStub<IXmlPoster>();
            var solrPoster = new SolrPoster<Song>(log, xmlPoster);
            solrPoster.PostToSolr(new Song());
            log.AssertWasCalled(x => x.Info("Post Added"));
        }

        [Test]
        public void Posting_file_should_log_error_if_exception()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var xmlPoster = MockRepository.GenerateStub<IXmlPoster>();
            xmlPoster.Stub(x => x.PostXml(Arg<string>.Is.Anything, Arg<Uri>.Is.Anything)).Throw(new Exception());

            var solrPoster = new SolrPoster<Song>(log, xmlPoster);
            solrPoster.PostToSolr(new Song());
            log.AssertWasCalled(x => x.Error("There was an error"));
        }
    }
}
