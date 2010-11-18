using System.Xml;

namespace ITunesIndexer.IntegrationTests
{
    public class SolrResponse
    {
        public int Status { get; set; }
        public int QTime { get; set; }

        public SolrResponse(string response) 
            : this(FromStringToXml(response))
        {}

        public SolrResponse(XmlNode xmlResponse)
        {
            if(xmlResponse==null)
                return;

            XmlNode statusNode = xmlResponse.SelectSingleNode("//int[@name='status']");
            XmlNode qTimeNode = xmlResponse.SelectSingleNode("//int[@name='QTime']");

            SetStatus(statusNode);
            SetQTime(qTimeNode);
        }

        private void SetQTime(XmlNode qTimeNode)
        {
            if (qTimeNode == null)
                return;

            int qTime;
            int.TryParse(qTimeNode.InnerText, out qTime);
            QTime = qTime;
        }

        private void SetStatus(XmlNode statusNode)
        {
            if (statusNode == null)
                return;

            int status;
            int.TryParse(statusNode.InnerText, out status);
            Status = status;
        }

        private static XmlDocument FromStringToXml(string response)
        {
            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response);
            return xmlResponse;
        }
    }
}