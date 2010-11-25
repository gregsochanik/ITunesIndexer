using System.Xml;
using System.Xml.Linq;

namespace ITunesIndexer.ItunesXml
{
    public static class XNodeExtensions
    {
        public static XmlDocument ToXmlDocument(this XNode n)
        {
            using (XmlReader xmlReader = n.CreateReader())
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }
    }
}