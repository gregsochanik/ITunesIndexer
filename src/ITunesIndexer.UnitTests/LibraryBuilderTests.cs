using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ITunesIndexer.ItunesXml;
using NUnit.Framework;
using Rhino.Mocks;

namespace ITunesIndexer.UnitTests
{
    public class LibraryBuilderTests
    {
        [Test]
        public void Can_build_valid_library_from_xml_file()
        {
            var xElements = new List<XElement>{new XElement("plist", new XElement("dlist", new XElement("artist", "billy idol")))};
            var libraryParser = MockRepository.GenerateStub<ILibraryParser>();
            libraryParser.Stub(x => x.ParseXml(Arg<string>.Is.Anything)).Return(xElements);
            
            var lb = new LibraryBuilder<XElement>(libraryParser);
            IEnumerable<XElement> buildLibrary = lb.BuildLibrary("");

            Assert.That(buildLibrary, Is.Not.Null);
            Assert.That(buildLibrary.Count(), Is.GreaterThanOrEqualTo(1));
        }

        

        [Test]
        public void Can_build_valid_library_from_xml()
        {
            var xElements = new List<XElement> { new XElement("plist", new XElement("dlist", new XElement("artist", "billy idol"))) };
            var lb = new LibraryBuilder<XElement>();
            IEnumerable<XElement> buildLibrary = lb.BuildLibrary(xElements);

            Assert.That(buildLibrary, Is.Not.Null);
            Assert.That(buildLibrary.Count(), Is.GreaterThanOrEqualTo(1));
        }
    }
}