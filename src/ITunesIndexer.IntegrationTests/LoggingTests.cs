using System.IO;
using log4net.Appender;
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
            
            IAppender[] apps = log.Logger.Repository.GetAppenders();
            
            var appender = apps[1] as RollingFileAppender;
            
            Assert.That(File.Exists(appender.File));
        }
    }
}
