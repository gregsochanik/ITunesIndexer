using Sochanik.Framework.Configuration;

namespace ITunesIndexer
{
    public class ConfigSettings : BaseGlobalSettings
    {
        public static string PathToXml { get; set; }
        public static string SolrUrl { get; set; }

        static ConfigSettings()
        {
            new ConfigSettings().Init();
        }

        public override void BuildConfigFileSettings()
        {
            PathToXml = GetAppSetting("PathToXml");
            SolrUrl = GetAppSetting("SolrUrl");
        }

        public override void BuildNonConfigFileSettings()
        {}
    }
}
