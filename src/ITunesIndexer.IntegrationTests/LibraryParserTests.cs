using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace ITunesIndexer.IntegrationTests
{
    public class LibraryParserTests
    {
        [Test]
        [Category("Integration")]
        public void Parsing_xml_should_produce_valid_list()
        {
            string pathToItunesLibrary = ConfigSettings.PathToXml;

            IEnumerable<XElement> songs = new LibraryParser().ParseXml(pathToItunesLibrary);

            Assert.IsNotNull(songs);
            List<XElement> list = songs.ToList();
            Assert.IsNotEmpty(list);
        }

        private static string BuildXmlFile()
        {
            var xmlFile = Path.GetTempFileName();

            using (var fs = File.OpenWrite(xmlFile))
            {
                using (var xmlWriter = new XmlTextWriter(fs, Encoding.UTF8))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("plist");
                    xmlWriter.WriteStartElement("dict");
                    xmlWriter.WriteStartElement("dict");
                    xmlWriter.WriteStartElement("dict");
                    xmlWriter.WriteElementString("key", "artist");
                    xmlWriter.WriteElementString("string", "billy idol");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.Flush();
                    fs.Flush();
                }
            }
            return xmlFile;
        }
    }
}
