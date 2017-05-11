using System;

namespace WebFinder
{
    /// <summary>
    /// Resultado de búsqueda.
    /// </summary>
    public class SearchResult
    {
        private string url;
        private string pageTitle;
        private int matches;
        private string searchTerm;

        public SearchResult()
        {

        }

        public SearchResult(string url, string pageTitle, int matches, string searchTerm)
        {
            this.url = url;
            this.pageTitle = pageTitle;
            this.matches = matches;
            this.searchTerm = searchTerm;
        }

        public string SearchTerm => searchTerm;
        public string PageTitle => pageTitle;
        public string URL => url;
        public int Matches => matches;

        public override string ToString()
        {
            return $"{pageTitle} | {matches} | {searchTerm} | {url}";
        }
    }
}