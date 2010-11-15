using NUnit.Framework;

namespace ITunesIndexer.IntegrationTests
{
    public class LoggingTests
    {
        [Test]
        public void SuccessLogLine()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(LoggingTests));
            log.Info("HELLO WORLD!");
        }

        [Test]
        public void Posting_file_should_log_info_if_success()
        {
            Assert.Fail();
        }

        [Test]
        public void Posting_file_should_log_error_if_exception()
        {
            Assert.Fail();
        }
    }
}
