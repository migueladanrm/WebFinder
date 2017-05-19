using System;
using System.Collections.Generic;
using System.Linq;

namespace WebFinder
{
    public class HtmlPage
    {
        private string url;
        private string title;
        private IEnumerable<string> html;

        public HtmlPage(string url, IEnumerable<string> html)
        {
            this.url = url;
            var tmp = HtmlUtils.GetPageTarget(html.ToList());

            title = tmp.PageTitle.Replace("\n", null);
            this.html = tmp.PageBody;
        }

        public string URL => url;
        public string Title => title;
        public IEnumerable<string> HTML => html;

    }
}