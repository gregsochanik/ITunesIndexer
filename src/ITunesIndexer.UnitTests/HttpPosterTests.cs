using System;
using System.IO;
using System.Text;
using ITunesIndexer.Http;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
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

        [Test]
        public void Should_return_response_cannot_be_read_error()
        {
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var memoryStream = new MemoryStream();
            webRequest.Stub(x => x.GetRequestStream()).Return(memoryStream);
            webRequest.Stub(x => x.GetResponse()).Return(null);
            var httpPoster = new HttpPoster(webRequest);

            const string expectedResponse = "Error: Response cannot be read";
            Assert.Throws(Is.InstanceOf(typeof(Exception)), 
                () => httpPoster.Post("Nothing much"), 
                expectedResponse);
            
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

            const string expectedResponse = "Error: Response stream cannot be read";
            Assert.Throws(Is.InstanceOf(typeof (Exception)), () => httpPoster.Post("Nothing much"), expectedResponse);
        } 

        [Test]
        public void Shoul_return_contents_of_response_stream_as_a_string()
        {
            const string expected = "Hello world";
            var webRequest = MockRepository.GenerateStub<IWebRequest>();
            var webResponse = MockRepository.GenerateStub<IWebResponse>();
            var requestStream = new MemoryStream();
            webRequest.Stub(x => x.GetRequestStream()).Return(requestStream);
            var responseStream = new MemoryStream();
            
            using(var sw = new StreamWriter(responseStream))
            {
                sw.Write(expected);
                sw.Flush();
                responseStream.Position = 0;
                webResponse.Stub(x => x.GetResponseStream()).Return(responseStream);
                webRequest.Stub(x => x.GetResponse()).Return(webResponse);

                var httpPoster = new HttpPoster(webRequest);
                string response = httpPoster.Post("");

                Assert.That(response, Is.EqualTo(expected));
            }
        }
    }
}