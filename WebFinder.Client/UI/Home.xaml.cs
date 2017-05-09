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

namespace WebFinder.UI
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

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
            Log("Obteniendo contenido...");

            var pages = await PageDownloader.DownloadPagesAsync(PageLibraryManager.GetLinks());

            Log("Obtención de contenido finalizada.");

            foreach(var page in pages) {
                foreach(string line in page) {
                    if (line.Contains("<title>")) {
                        Console.WriteLine(line);
                        break;
                    }
                }
            }
        }

        private void btnManageLibrary_Click(object sender, RoutedEventArgs e)
        {
            var ml = new ManageLibrary() {
                Owner = this
            };
            ml.ShowDialog();
        }

    }
}
