using System.Xml;
using System.Xml.Linq;
using ITunesIndexer.ItunesXml;
using NUnit.Framework;

namespace ITunesIndexer.UnitTests
{
    public class XNodeExtensionsTests
    {
        [Test]
        public void SuccessToXmlDocument()
        {
            var xNode = new XElement(XName.Get("test"));
            XmlDocument navigable = xNode.ToXmlDocument();
            Assert.IsNotNull(navigable);
            Assert.AreEqual("<test />", navigable.OuterXml);
        }
    }
}