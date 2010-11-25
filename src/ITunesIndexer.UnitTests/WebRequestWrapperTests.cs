using System.IO;
using System.Net;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
    public class WebRequestWrapperTests
    {
        [Test]
        public void Should_return_correct_request_stream()
        {
            const string expected = "Hello world";
            var webRequest = MockRepository.GenerateStub<WebRequest>();
            var responseStream = new MemoryStream();
            using (var sw = new StreamWriter(responseStream))
            {
                sw.Write(expected);
                sw.Flush();
                responseStream.Position = 0;
                webRequest.Stub(x => x.GetRequestStream()).Return(responseStream);

                var webResponseWrapper = new WebRequestWrapper(webRequest);

                using (var sr = new StreamReader(webResponseWrapper.GetRequestStream()))
                {
                    string actual = sr.ReadToEnd().Trim();

                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }

        [Test]
        [Ignore("Come back to this")]
        public void Should_return_correct_response()
        {
            var webRequest = MockRepository.GenerateStub<WebRequest>();
            var webResponse = MockRepository.GenerateStub<WebResponse>();
            webRequest.Stub(x => x.GetResponse()).Return(webResponse);

            var webRequestWrapper = new WebRequestWrapper(webRequest);

            Assert.That(webRequestWrapper.GetResponse(), Is.SameAs(webResponse));
        }

        [Test]
        public void Should_return_correct_content_length()
        {
            var webRequest = MockRepository.GenerateStub<WebRequest>();
            var webResponseWrapper = new WebRequestWrapper(webRequest);

            const int expected = 2000;
            webResponseWrapper.SetContentLength(expected);

            Assert.That(webResponseWrapper.ContentLength, Is.EqualTo(expected));
        }
    }
}