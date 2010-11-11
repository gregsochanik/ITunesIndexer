using Sochanik.Framework.Configuration;

namespace ITunesIndexer
{
    public class ConfigSettings : BaseGlobalSettings
    {
        public static string PathToXml { get; set; }

        static ConfigSettings()
        {
            new ConfigSettings().Init();
        }

        public override void BuildConfigFileSettings()
        {
            PathToXml = GetAppSetting("PathToXml");
        }

        public override void BuildNonConfigFileSettings()
        {}
    }
}
