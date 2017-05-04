using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;
using System.Collections.Generic;

namespace WebFinder.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DownloadPage()
        {
            var result = SearchEngine.GetPages(new List<string> { "http://es.wikipedia.org/wiki/Tierra" });

            foreach (string page in result) {
                WriteLine(page);
            }
        }
    }
}
