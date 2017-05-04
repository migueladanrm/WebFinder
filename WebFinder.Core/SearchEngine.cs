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

        public static async Task<List<string>> getUtilPages(List<string> searchTerms) {

            var utilPages = new List<string>();
            if (searchTerms.Count > 0)
            {
                if (searchTerms.Count == 1) {
                    foreach (string link in PageLibraryManager.GetLinks())
                    {
                        var page = await HttpDownloader.DownloadPageAsync(link);
                        //if(ifUtil(page.Split('\n').ToList(), searchTerms[0]) > 0);
                        //utilPages.Add();
                    }

                }

                return utilPages;
            }
            return null;
        }

        public static int ifUtil(List<string> page, string term)
        {
            int cont = 0;
            foreach(string s in page)
            {
                if (s.Contains(term))
                    cont++;
            }
            return cont;
        }
    }
}