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

        public static async Task<IEnumerable<HtmlPage>> DownloadPagesAsync(IEnumerable<string> urls)
        {
            var pages = new List<HtmlPage>();

            //Parallel.ForEach(urls, async url => {
            //    var tmp = await DownloadPageAsync(url);
            //    pages.Add(tmp);
            //});

            foreach (string url in urls) {
                var tmp = await DownloadPageAsync(url);
                pages.Add(new HtmlPage(url, tmp));
            }

            return pages;
        }
    }
}