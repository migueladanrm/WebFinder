using System;
using System.Net;
using System.IO;
using static WebFinder.EventLogger;

namespace WebFinder
{
    /// <summary>
    /// Herramienta de descarga.
    /// </summary>
    public static class HttpDownloader
    {
        /// <summary>
        /// Ejecuta una descarga desde un enlace proporcionado.
        /// </summary>
        /// <param name="url">Enlace.</param>
        public static string DownloadPage(string url)
        {
            Log($"Inicializando descarga desde: '{url}'.");

            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                html = reader.ReadToEnd();
            }

            Log("Se ha completado la descarga.");

            return html;
        }
    }
}