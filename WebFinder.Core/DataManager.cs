using System.Data.SQLite;
using System.IO;

namespace WebFinder
{
    /// <summary>
    /// Gestor de base de datos local.
    /// </summary>
    public static class DataManager
    {
        private static readonly string FILE_PATH = $@"{Constants.DataDir}\LocalDB.db";
        private static readonly string CONNECTION_STRING = $"Data Source={FILE_PATH};Version=3;Pooling=True;";
        private static readonly string[] dbArchitecture = new string[] {
            "CREATE TABLE PageLibrary (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Link TEXT NOT NULL, Payload BLOB, LastVisit TEXT);"
        };

        /// <summary>
        /// Obtiene una instancia de conexión de la base de datos local.
        /// </summary>
        /// <returns>Conexión SQLite.</returns>
        public static SQLiteConnection GetDatabaseConnection() => DatabaseIsReady() ? new SQLiteConnection(CONNECTION_STRING) : null;

        /// <summary>
        /// Comprueba si la base de datos se encuentra preparada, si es necesario ejecuta una configuración de la misma.
        /// </summary>
        /// <returns>Estado de preparación de la base de datos.</returns>
        private static bool DatabaseIsReady()
        {
            try {
                if (File.Exists(FILE_PATH)) {
                    if ((new FileInfo(FILE_PATH).Length) > 0)
                        return true;
                }
                return SetupDatabase();
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Configura la base de datos local.
        /// </summary>
        /// <returns>Resultado de la operación.</returns>
        private static bool SetupDatabase()
        {
            if (File.Exists(FILE_PATH))
                File.Delete(FILE_PATH);


            SQLiteConnection.CreateFile(FILE_PATH);
            using(var db = new SQLiteConnection(CONNECTION_STRING)) {
                db.Open();
                foreach(string sql in dbArchitecture) {
                    using (var cmd = new SQLiteCommand(sql, db)) {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }
    }
}