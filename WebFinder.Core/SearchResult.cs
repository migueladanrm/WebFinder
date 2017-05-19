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
        private double searchTime;

        public SearchResult()
        {

        }

        public SearchResult(string url, string pageTitle, int matches, string searchTerm, double searchTime)
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

        public double SearchTime => searchTime;

        public override string ToString()
        {
            return $"{pageTitle} | {matches} | {searchTerm} | {url}";
        }
    }
}