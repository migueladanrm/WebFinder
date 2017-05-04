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

        public string SearchTerm => searchTerm;
        public string PageTitle => PageTitle;
        public string URL => url;
        public int Matches => matches;
    }
}