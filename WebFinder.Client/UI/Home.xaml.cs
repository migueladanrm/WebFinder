using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WebFinder.UI.Controls;
using static WebFinder.EventLogger;

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

        private IEnumerable<HtmlPage> pages = null;
        private List<string> lastSearchTerms = null;
        private List<SearchResult> lastSearchResults = null;
        //private List<double> lastSearchElapsedTime = null;
        private double lastSearchElapsedTime = 0;

        public Home()
        {
            InitializeComponent();

            GetPagesOnStart();

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

            containerSearchOverview.Visibility = Visibility.Collapsed;
            containerTargetSearch.Visibility = Visibility.Collapsed;
        }

        private async void GetPagesOnStart()
        {
            var tmp = await PageDownloader.DownloadPagesAsync(PageLibraryManager.GetLinks());
            pages = tmp;
            Log("Se ha completado la descarga inicial de páginas.");
        }

        private void PrepareSearch(IEnumerable<string> searchTerms)
        {
            SetupHint(HINT_DOWNLOADING_PAGES);

            Log("Obteniendo contenido...");

            var sw = Stopwatch.StartNew();

            Log($"Obtención de contenido finalizada en {sw.Elapsed.TotalMilliseconds} ms.");
            sw.Stop();

            SetupHint(HINT_HIDE);

            sw = Stopwatch.StartNew();

            var results = SearchEngine.RunSearch(pages, searchTerms, SettingsManager.GetSetting(SettingsManager.STT_SEARCH_PARALLEL));

            sw.Stop();
            lastSearchElapsedTime = sw.Elapsed.TotalMilliseconds;

            Log($"Búsqueda completada en {lastSearchElapsedTime} ms.");

            lastSearchTerms = (List<string>)searchTerms;
            lastSearchResults = results;

            LoadSearchedTerms();

            if (lastSearchTerms.Count > 1)
                containerTargetSearch.Visibility = Visibility.Visible;
            containerSearchOverview.Visibility = Visibility.Visible;

            LoadResults(lastSearchTerms[0]);
        }

        private void LoadSearchedTerms()
        {
            cbxTargetTerm.Items.Clear();
            foreach (var item in lastSearchTerms) {
                cbxTargetTerm.Items.Add(item);
            }

            cbxTargetTerm.SelectedIndex = 0;
        }

        private void btnManageLibrary_Click(object sender, RoutedEventArgs e)
        {
            new Options() {
                Owner = this
            }.ShowDialog();
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
        }

        private void LoadResults(string targetTerm)
        {
            stkResults.Children.Clear();

            lblSearchOverview.Content = ((string)FindResource("lang.searchResultOverview")).Replace("{0}",
                            (from n in lastSearchResults where n.SearchTerm.Equals(targetTerm) select n).Count().ToString())
                            .Replace("{1}", targetTerm).Replace("{2}", lastSearchElapsedTime.ToString());
            
            foreach (var result in lastSearchResults) {
                if (result.Matches > 0 && result.SearchTerm.Equals(targetTerm)) {
                    var item = new SearchResultItem() {
                        PageTitle = result.PageTitle,
                        PageUrl = result.URL,
                        MatchesFound = result.Matches
                    };
                    stkResults.Children.Add(item);
                }
            }
        }

        private void cbxTargetTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTargetTerm.SelectedIndex > -1)
                LoadResults(lastSearchTerms[cbxTargetTerm.SelectedIndex]);
        }
    }
}