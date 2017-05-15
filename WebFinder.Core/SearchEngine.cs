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
        public static IEnumerable<SearchResult> RunSearch(IEnumerable<IEnumerable<string>> pages, IEnumerable<string> searchTerms, bool parallel)
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

        private static SearchResult Search(IEnumerable<string> page, string searchTerm)
        {
            var target = HtmlUtils.GetPageTarget(page.ToList());

            int matches = 0;

            foreach(string line in target.PageBody) {
                if (line.Contains(searchTerm))
                    matches++;
            }

            return new SearchResult(null, target.PageTitle, matches, searchTerm);
        }

        private static SearchResult SearchParallel(IEnumerable<string> page, string searchTerm)
        {
            var target = HtmlUtils.GetPageTarget(page.ToList());

            int matches = 0;

            Parallel.ForEach(page, line => {
                if (line.Contains(searchTerm))
                    matches++;
            });

            return new SearchResult(null, target.PageTitle, matches, searchTerm);
        }

        //public static IEnumerable<IEnumerable<(string Title, string URL, int Matches, int SearchTerm)>> RunSearch()
        //{

        //}



        //public static List<SearchResult> RunSearch(string[] searchTerms)
        //{
        //    var results = new List<SearchResult>();

        //    var pages = GetPages(PageLibraryManager.GetLinks());

        //    for (int i = 0; i < searchTerms.Length; i++) {
        //        foreach(var page in pages) {
        //            results.Add(Search(page.Split('\n').ToList(), searchTerms[i]));
        //        }
        //    }

        //    return results;
        //}

        //public static SearchResult Search(IEnumerable<string> pagePayload, string searchTerm)
        //{
        //    int matches = 0;
        //    foreach (string line in pagePayload) {
        //        if (line.Contains(searchTerm))
        //            matches++;
        //    }

        //    return new SearchResult(null, null, matches, searchTerm);
        //}

        //public List<SearchResult> Search(string searchTerm, List<string> pages)
        //{
        //    Parallel.ForEach(pages, (link) => {

        //    });
        //}

        //        public static IEnumerable<IEnumerable<string>> GetPages(IEnumerable<string> pages)
        //        {
        //#if prueba
        //            foreach (string page in pages)
        //                yield return HttpDownloader.DownloadPage(page);
        //#endif
        //            var tmp = new List<string>();
        //            foreach (string page in pages)
        //                tmp.Add(HttpDownloader.Download(page));

        //            return tmp;
        //        }
    }
}