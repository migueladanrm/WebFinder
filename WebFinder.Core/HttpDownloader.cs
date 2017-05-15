using System.IO;
using System.Net;
using System.Threading.Tasks;
using static WebFinder.EventLogger;
using static System.Console;
namespace WebFinder
{
    /// <summary>
    /// Herramienta de descarga.
    /// </summary>
    public static class HttpDownloader
    {
        /// <summary>
        /// Ejecuta una descarga desde un enlace proporcionado de forma asíncrona.
        /// </summary>
        /// <param name="url">Enlace.</param>
        public static async Task<string> DownloadAsync(string url)
        {
            Log($"Inicializando descarga desde: '{url}'.");

            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                html = reader.ReadToEnd();
            }

            Log("Se ha completado la descarga.");
            
            return html;
        }

        public static string Download(string url)
        {
            Log($"Inicializando descarga desde: '{url}'.");

            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                html = reader.ReadToEnd();
            }

            Log("Se ha completado la descarga.");

            return html;
        }
    }
}