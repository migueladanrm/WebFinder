using System;
using System.Data;
using System.Data.SQLite;

namespace WebFinder
{
    public static class DataManager
    {
        private static readonly string CONNECTION_STRING = $@"Data Source={Constants.DataDir}\LocalDB.db;Version=3;Pooling=True;";

        public static SQLiteConnection GetDatabaseConnection() => new SQLiteConnection(CONNECTION_STRING);
    }
}