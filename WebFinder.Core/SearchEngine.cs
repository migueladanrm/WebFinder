using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebFinder
{
    /// <summary>
    /// Motor de búsqueda.
    /// </summary>
    public static class SearchEngine
    {
        public static List<SearchResult> RunSearch(IEnumerable<HtmlPage> pages, IEnumerable<string> searchTerms, bool parallel)
        {
            var results = new List<SearchResult>();
            
            if (parallel) {
                Parallel.ForEach(pages, page => {
                    Parallel.ForEach(searchTerms, term => {
                        var result = SearchParallel(page, term);
                        results.Add(result);
                    });
                });
            } else {
                foreach (var page in pages) {
                    foreach (string term in searchTerms) {
                        var result = Search(page, term);

                        results.Add(result);
                    }
                }
            }

            return results;
        }

        private static SearchResult Search(HtmlPage page, string searchTerm)
        {
            int matches = 0;
            var searchTime = Stopwatch.StartNew();

            foreach(string line in page.HTML) {
                if (line.Contains(searchTerm))
                    matches++;
            }

            searchTime.Stop();

            return new SearchResult(page.URL, page.Title, matches, searchTerm, searchTime.Elapsed.TotalMilliseconds);
        }

        private static SearchResult SearchParallel(HtmlPage page, string searchTerm)
        {
            int matches = 0;
            var sw = Stopwatch.StartNew();

            Parallel.ForEach(page.HTML, line => {
                Parallel.ForEach(line.Split(' '), word => {
                    if (word.ToLower().Contains(searchTerm) || word.ToLower().Equals(searchTerm.ToLower()))
                        matches++;
                });
            });

            sw.Stop();

            return new SearchResult(page.URL, page.Title, matches, searchTerm, sw.Elapsed.TotalMilliseconds);
        }
    }
}