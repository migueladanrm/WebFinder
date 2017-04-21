﻿using System;
using System.IO;

namespace WebFinder
{
    /// <summary>
    /// Constantes de uso genérico del programa.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Directorio de datos principal.
        /// </summary>
        internal static string DataDir {
            get {
                string dir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\TextFinder";
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