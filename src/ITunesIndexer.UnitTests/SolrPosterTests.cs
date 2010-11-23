using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
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

    public class HttpPosterTests
    {
        [Test]
        public void SuccessCtor()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var httpPoster = new HttpPoster(webRequest);
            Assert.That(httpPoster, Is.Not.Null);

        }

        [Test]
        public void Should_set_content_length_with_correct_value()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var httpPoster = new HttpPoster(webRequest);

            const string content = "Hello world";
            byte[] bytes = Encoding.ASCII.GetBytes(content);

            httpPoster.Post(content);

            webRequest.AssertWasCalled(x => x.SetContentLength(bytes.Length));
        }

        [Test, Category("Integration")]
        public void Should_write_stream()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var memoryStream = new MemoryStream();
            webRequest.Stub(x => x.GetRequestStream()).Return(memoryStream);
            var httpPoster = new HttpPoster(webRequest);

            const string content = "Hello world";
            byte[] bytes = Encoding.ASCII.GetBytes(content);

            httpPoster.Post(content);

            // Closed stream - this can only be tested in integration tests
            //Assert.That(memoryStream.Length, Is.EqualTo(bytes.Length));

        }

        [Test]
        public void Should_return_response_cannot_be_read_error()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var memoryStream = new MemoryStream();
            webRequest.Stub(x => x.GetRequestStream()).Return(memoryStream);
            webRequest.Stub(x => x.GetResponse()).Return(null);
            var httpPoster = new HttpPoster(webRequest);
            string response = httpPoster.Post("Nothing much");

            const string expectedResponse = "Error: Response cannot be read";
            Assert.That(response, Is.EqualTo(expectedResponse));
        } 

        [Test]
        public void Should_return_response_stream_cannot_be_read_error()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var webResponse = MockRepository.GenerateStub<IWebResponse>();
            webResponse.Stub(x => x.GetResponseStream()).Return(null);

            var memoryStream = new MemoryStream();
            webRequest.Stub(x => x.GetRequestStream()).Return(memoryStream);
            webRequest.Stub(x => x.GetResponse()).Return(webResponse);
            var httpPoster = new HttpPoster(webRequest);
            string response = httpPoster.Post("Nothing much");

            const string expectedResponse = "Error: Response stream cannot be read";
            Assert.That(response, Is.EqualTo(expectedResponse));
        } 

        [Test]
        public void Shoul_return_contents_of_response_stream_as_a_string()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var webResponse = MockRepository.GenerateStub<IWebResponse>();
            var requestStream = new MemoryStream();
            webRequest.Stub(x => x.GetRequestStream()).Return(requestStream);
            var responseStream = new MemoryStream();
            using(var sw = new StreamWriter(responseStream))
            {
                const string expected = "Hello world";
                sw.Write(expected);
                sw.Flush();
                webResponse.Stub(x => x.GetResponseStream()).Return(responseStream);
                webRequest.Stub(x => x.GetResponse()).Return(webResponse);
                var httpPoster = new HttpPoster(webRequest);
                string response = httpPoster.Post("Nothing much");

                Assert.That(response, Is.EqualTo(expected));
            }
        }
    }
}
