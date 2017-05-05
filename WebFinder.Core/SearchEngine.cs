using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebFinder
{
    /// <summary>
    /// Motor de búsqueda.
    /// </summary>
    public static class SearchEngine
    {

        public static SearchResult Search(IEnumerable<string> pagePayload, string searchTerm)
        {
            int matches = 0;
            foreach(string line in pagePayload) {
                if (line.Contains(searchTerm))
                    matches++;
            }

            return new SearchResult();
        }

        //public List<SearchResult> Search(string searchTerm, List<string> pages)
        //{
        //    Parallel.ForEach(pages, (link) => {

        //    });
        //}

        public static IEnumerable<string> GetPages(IEnumerable<string> pages)
        {
            foreach (string page in pages)
                yield return HttpDownloader.DownloadPage(page);
        }
    }
}