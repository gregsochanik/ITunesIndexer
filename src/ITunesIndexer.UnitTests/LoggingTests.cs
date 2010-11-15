using log4net;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
    public class LoggingTests
    {
        [Test]
        public void Posting_file_should_log_info_if_success()
        {
            var log =  MockRepository.GenerateStub<ILog>();

            log.AssertWasCalled(x => x.Info("Post Added"));
        }

        [Test]
        public void Posting_file_should_log_error_if_exception()
        {
            var log = MockRepository.GenerateStub<ILog>();

            log.AssertWasCalled(x => x.Error("There was an error"));
        }
    }
}
