using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using ITunesIndexer.Models;
using Sochanik.Framework.Xml;

namespace ITunesIndexer
{
    public class LibraryParser : ILibraryParser
    {
        public IEnumerable<XElement> ParseXml(string pathToXml)
        {
            // transfer
            XDocument loaded = XDocument.Load(pathToXml); // TODO This could be pulled out to allow Stubbed XDocuments to be passed in for testing

            Func<XElement, XElement> keySelector = key => new XElement(((string)key).Replace(" ", ""), (string)(XElement)key.NextNode);

            Func<XElement, XElement> songSelector = song => new XElement("song", song.Descendants("key").Select(keySelector));

            var rawsongs = loaded.Descendants("plist")
                                .Descendants("dict")
                                .Descendants("dict")
                                .Descendants("dict").Select(songSelector);

            IEnumerable<XElement> songs = from song in rawsongs where song.Element("Location") != null select song;

            return songs;
        }

        public static IEnumerable<Song> GetLibraryAsSongs(string pathToXml)
        {
            IEnumerable<XElement> songs = new LibraryParser().ParseXml(pathToXml);
            var songList = new List<Song>();
            foreach (XElement xElement in songs)
            {
                Song song = SerializationHelper<Song>.Deserialize(xElement.ToNavigable());
                songList.Add(song);
            }
            return songList;
        }
    }
}
