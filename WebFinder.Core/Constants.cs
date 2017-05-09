using System;
using System.IO;

namespace WebFinder
{
    /// <summary>
    /// Constantes de uso genérico del programa.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Directorio de datos principal.
        /// </summary>
        public static string DataDir {
            get {
                string dir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\WebFinder";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <summary>
        /// Directorio de registros de sesiones de la aplicación.
        /// </summary>
        internal static string LogsDir {
            get {
                string dir = $@"{DataDir}\Logs";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }
    }
}