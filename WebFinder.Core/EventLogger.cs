using System;
using System.IO;

namespace WebFinder
{
    /// <summary>
    /// Herramienta de registro de eventos de la aplicación.
    /// </summary>
    public static class EventLogger
    {
        private static string logFile;

        /// <summary>
        /// Registra un evento en consola y lo almacena en un archivo de registros de la sesión.
        /// </summary>
        /// <param name="msg">Registro de texto.</param>
        public static void Log(string msg) => Log(msg, "WebFinder");

        /// <summary>
        /// Registra un evento en consola y lo almacena en un archivo de registros de la sesión.
        /// </summary>
        /// <param name="msg">Registro de texto.</param>
        /// <param name="tag">Etiqueta o descripción para el evento.</param>
        public static void Log(string msg, string tag)
        {
            string line = $"{tag} > {msg} | {DateTime.Now.TimeOfDay}";
            Console.WriteLine(line);
            SaveLine(line);
        }

        /// <summary>
        /// Guarda un registro en el archivo de registros de la sesión actual.
        /// </summary>
        /// <param name="line">Registro a guardar.</param>
        private static void SaveLine(string line)
        {
            string addZero(int n)
            {
                if (n <= 9)
                    return $"0{n}";
                return n.ToString();
            }

            if (logFile == null) {
                var now = DateTime.Now;
                logFile = $@"{Constants.LogsDir}\{now.Year}{addZero(now.Month)}{addZero(now.Day)}_{addZero(now.Hour)}{addZero(now.Minute)}{addZero(now.Second)}.log";

                using (File.Create(logFile)) {
                    ;
                }
            }

            using (StreamWriter log = File.AppendText(logFile))
                log.WriteLine(line);
        }
    }
}