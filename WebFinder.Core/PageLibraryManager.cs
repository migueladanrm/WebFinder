using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json.Linq;
using static WebFinder.DatabaseManager;
using static WebFinder.EventLogger;
using System.Collections.Generic;

namespace WebFinder
{
    public static class PageLibraryManager
    {
        private static string libraryFile = @"C:\Users\Miguel\Desktop\library.json";

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
            }
            return null;
        }

        public static bool ExistsDownloadedPage(string url)
        {
            return false;
        }

        public static string GetPayload(string link)
        {
            return null;
        }

        public static void AddPayload(string link, string payload)
        {

        }

        //public static List<string> GetLinks()
        //{
        //    var links = new List<string>();

        //    using (var db = GetDatabaseConnection()) {
        //        db.Open();
        //        using (var cmd = new SQLiteCommand("SELECT * FROM PageLibrary;", db))
        //        using (SQLiteDataReader reader = cmd.ExecuteReader()) {
        //            while (reader.Read())
        //                links.Add((string)reader["Link"]);
        //        }
        //    }

        //    return links;
        //}

        public static void Import(JArray lib)
        {
            Log("Se ha iniciado la importación de enlaces...");

            using (var db = GetDatabaseConnection()) {
                db.Open();
                foreach (string link in lib) {
                    using (var cmd = new SQLiteCommand("INSERT INTO PageLibrary (Link) VALUES(@link);", db)) {
                        cmd.Parameters.Add("@link", DbType.String).Value = link.Replace("http://", null);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Log("Ha finalizado la importación de enlaces.");
        }
    }
}