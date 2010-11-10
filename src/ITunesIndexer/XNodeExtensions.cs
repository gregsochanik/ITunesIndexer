using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ITunesIndexer
{
    public static class XNodeExtensions
    {
        private class Navigable : IXPathNavigable
        {
            private readonly XNode _node;

            public Navigable(XNode n)
            {
                _node = n;
            }
            public XPathNavigator CreateNavigator()
            {
                return (_node.CreateNavigator());
            }
        }

        public static IXPathNavigable ToNavigable(this XNode n)
        {
            return (new Navigable(n));
        }

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