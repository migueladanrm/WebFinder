using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFinder
{
    public static class PageDownloader
    {
        public static async Task<IEnumerable<string>> DownloadPageAsync(string url)
        {
            var page = await HttpDownloader.DownloadAsync(url);
            return page.Split('\n').ToList();
        }

        public static async Task<IEnumerable<IEnumerable<string>>> DownloadPagesAsync(IEnumerable<string> urls)
        {
            var pages = new List<IEnumerable<string>>();

            foreach (string url in urls) {
                var tmp = await DownloadPageAsync(url);
                pages.Add(tmp);
            }

            return pages;
        }
    }
}