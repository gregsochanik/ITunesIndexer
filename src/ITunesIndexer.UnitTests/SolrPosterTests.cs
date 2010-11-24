using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using ITunesIndexer.Models;
using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
    public class SolrPosterTests
    {
        [Test]
        public void SuccessCtor()
        {
            var xmlPoster = MockRepository.GenerateStub<IHttpPoster>();
            var solrPoster = new SolrPoster<Song>(xmlPoster);
            Assert.That(solrPoster, Is.Not.Null);
        }


        [Test]
        public void Posting_file_should_log_info_if_success()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var xmlPoster = MockRepository.GenerateStub<IHttpPoster>();
            xmlPoster.Stub(x => x.Post(Arg<string>.Is.Anything)).Return("Post Added");
            var solrPoster = new SolrPoster<Song>(log, xmlPoster);
            solrPoster.PostToSolr(new Song());
            log.AssertWasCalled(x => x.Info("Post Added"));
        }

        [Test]
        public void Posting_file_should_log_error_if_exception()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var xmlPoster = MockRepository.GenerateStub<IHttpPoster>();
            xmlPoster.Stub(x => x.Post(Arg<string>.Is.Anything)).Throw(new Exception());

            var solrPoster = new SolrPoster<Song>(log, xmlPoster);
            solrPoster.PostToSolr(new Song());
            log.AssertWasCalled(x => x.Error("There was an error"));
        }
    }
    public class WebResponseWrapperTests
    {
        [Test]
        public void Should_return_correct_response_stream()
        {
            const string expected = "Hello world";
            var webResponse = MockRepository.GenerateStub<WebResponse>();
            var responseStream = new MemoryStream();
            using (var sw = new StreamWriter(responseStream))
            {
                sw.Write(expected);
                sw.Flush();
                responseStream.Position = 0;
                webResponse.Stub(x => x.GetResponseStream()).Return(responseStream);

                var webResponseWrapper = new WebResponseWrapper(webResponse);

                using (var sr = new StreamReader(webResponseWrapper.GetResponseStream()))
                {
                    string actual = sr.ReadToEnd().Trim();

                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }
    }
}
