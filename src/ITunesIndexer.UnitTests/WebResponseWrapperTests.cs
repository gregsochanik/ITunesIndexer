using System.IO;
using System.Net;
using ITunesIndexer.Http;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
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