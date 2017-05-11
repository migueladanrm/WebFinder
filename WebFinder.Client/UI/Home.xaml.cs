using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WebFinder.EventLogger;
using System.Windows.Media.Animation;
using System.Threading;
using System.Timers;
using System.Windows.Threading;
using static System.Console;
using System.Diagnostics;
using WebFinder.UI.Controls;

namespace WebFinder.UI
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private const int HINT_DEFAULT = 0x1;
        private const int HINT_DOWNLOADING_PAGES = 0x2;
        private const int HINT_HIDE = 0x3;

        public Home()
        {
            InitializeComponent();

            SetupHint(HINT_DEFAULT);

            searchBox.OnSearchRequest += (sender, e) => {
                if (e.SearchTerms != null)
                    PrepareSearch(e.SearchTerms);
                else
                    BeginStoryboard((Storyboard)FindResource("anim.messageBar.show"));
            };

            // animación de ocultar después de 1s.
            ((Storyboard)FindResource("anim.messageBar.show")).Completed += (sender, e) => {
                DispatcherTimer tmr = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
                tmr.Tick += (sender2, e2) => {
                    BeginStoryboard((Storyboard)FindResource("anim.messageBar.hide"));
                    tmr.Stop();
                };
                tmr.Start();
            };

            searchBox.Focus();

            messageBar.Height = 0;
        }

        private async void PrepareSearch(IEnumerable<string> searchTerms)
        {
            SetupHint(HINT_DOWNLOADING_PAGES);

            Log("Obteniendo contenido...");

            var sw = Stopwatch.StartNew();

            var pages = await PageDownloader.DownloadPagesAsync(PageLibraryManager.GetLinks());

            Log($"Obtención de contenido finalizada en {sw.Elapsed.TotalMilliseconds} ms.");
            sw.Stop();

            //foreach (var page in pages) {
            //    foreach (string line in page) {
            //        if (line.Contains("<title>")) {
            //            WriteLine(line);
            //            break;
            //        }
            //    }
            //}

            SetupHint(HINT_HIDE);




            var results = SearchEngine.RunSearch(pages, searchTerms, false).ToList();

            LoadResults(results);

            //foreach(var result in results) {
            //    WriteLine(result.ToString());
            //}
        }

        private void btnManageLibrary_Click(object sender, RoutedEventArgs e)
        {
            var ml = new ManageLibrary() {
                Owner = this
            };
            ml.ShowDialog();
        }

        private void SetupHint(int mode)
        {
            string icon = string.Empty;
            string text = string.Empty;

            switch (mode) {
                default:
                    return;
                case HINT_DEFAULT:
                    icon = "vector.magnify";
                    text = "lang.home.hint.default";
                    break;
                case HINT_DOWNLOADING_PAGES:
                    icon = "vector.loop";
                    text = "lang.home.hint.downloadingData";
                    break;
                case HINT_HIDE:
                    lblHints.Visibility = Visibility.Hidden;
                    return;
            }

            lblHintsIcon.Data = (Geometry)FindResource(icon);
            lblHintsText.Text = (string)FindResource(text);

            /*
            if (mode == HINT_DOWNLOADING_PAGES) {
                var sb = (Storyboard)FindResource("anim.hint.rotateIcon");
                sb.Completed += (sender, e) => {
                    BeginStoryboard(sb);
                };
                BeginStoryboard(sb);
            } else {
                ((Storyboard)FindResource("anim.hint.rotateIcon")).Stop();
                lblHintsIcon.RenderTransform = new RotateTransform(0);
            }
            */

        }

        private void LoadResults(List<SearchResult> results)
        {
            foreach (var result in results) {
                var item = new SearchResultItem() {
                    PageTitle = result.PageTitle,
                    PageUrl = result.URL,
                    MatchesFound = result.Matches
                };
                stkResults.Children.Add(item);
            }
        }

    }
}
