using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using static WebFinder.EventLogger;

namespace WebFinder
{
    public static class SettingsManager
    {
        private static readonly string SETTINGS_FILE = $@"{Constants.DataDir}\settings.json";

        public const string STT_SEARCH_PARALLEL = "searchParallel";


        public static void SetSetting(string targetSetting, object value)
        {
            JObject json = File.Exists(SETTINGS_FILE) ? JObject.Parse(File.ReadAllText(SETTINGS_FILE)) : GetDefaultSettingFile();
            json[targetSetting] = new JValue(value);

            File.WriteAllText(SETTINGS_FILE, json.ToString());

            Log($"Se ha modificado el ajuste '{targetSetting}'.");
        }

        private static JObject GetDefaultSettingFile() => new JObject {
            { "searchParallel", false }
        };

        public static dynamic GetSetting(string setting)
        {

            if (!File.Exists(SETTINGS_FILE)) {
                File.WriteAllText(SETTINGS_FILE, GetDefaultSettingFile().ToString());
            }

            var json = JObject.Parse(File.ReadAllText(SETTINGS_FILE));

            switch (setting) {
                case STT_SEARCH_PARALLEL:
                    return (bool)json[STT_SEARCH_PARALLEL];
            }

            return null;
        }
    }
}
