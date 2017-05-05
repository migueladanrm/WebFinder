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
        public static List<SearchResult> RunSearch(string[] searchTerms)
        {
            var results = new List<SearchResult>();

            var pages = GetPages(PageLibraryManager.GetLinks());

            for (int i = 0; i < searchTerms.Length; i++) {
                foreach(var page in pages) {
                    results.Add(Search(page.Split('\n').ToList(), searchTerms[i]));
                }
            }

            return results;
        }

        public static SearchResult Search(IEnumerable<string> pagePayload, string searchTerm)
        {
            int matches = 0;
            foreach (string line in pagePayload) {
                if (line.Contains(searchTerm))
                    matches++;
            }

            return new SearchResult(null, null, matches, searchTerm);
        }

        //public List<SearchResult> Search(string searchTerm, List<string> pages)
        //{
        //    Parallel.ForEach(pages, (link) => {

        //    });
        //}

        public static IEnumerable<string> GetPages(IEnumerable<string> pages)
        {
#if prueba
            foreach (string page in pages)
                yield return HttpDownloader.DownloadPage(page);
#endif
            var tmp = new List<string>();
            foreach (string page in pages)
                tmp.Add(HttpDownloader.DownloadPage(page));

            return tmp;
        }
    }
}