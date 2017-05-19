using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace WebFinder
{
    public static class PageLibraryManager
    {
        private static string libraryFile = $@"{Constants.DataDir}\library.json";

        public static void SetLibraryFile(string path)
        {
            libraryFile = path;
        }

        public static IEnumerable<string> GetLinks()
        {
            if (libraryFile != null && File.Exists(libraryFile)) {
                var json = JArray.Parse(File.ReadAllText(libraryFile));
                var result = new List<string>();

                foreach (string item in json) {
                    result.Add(item);
                }

                return result;
            } else {
                File.WriteAllText(libraryFile, "[]");
                return new List<string>();
            }
        }

        public static void AddLink(string link)
        {
            var json = JArray.Parse(File.ReadAllText(libraryFile));
            json.Add(link);
            File.WriteAllText(libraryFile,json.ToString());
        }
    }
}