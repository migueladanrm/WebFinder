using System;
using System.Collections.Generic;
using System.Linq;

namespace WebFinder
{
    public static class HtmlUtils
    {
        public static (string PageTitle, IEnumerable<string> PageBody) GetPageTarget(List<string> page)
        {
            string title = string.Empty;
            int bodyStart = 0;

            for (int i = 0; i < page.Count; i++) {
                if (page[i].Contains("<title>"))
                    title = page[i];

                if (page.Contains("<body"))
                    bodyStart = i;
            }

            page.RemoveRange(0, bodyStart);
            title = title.Replace("<title>", null).Replace("</title>", null);
            return (title, page);
        }
    }
}