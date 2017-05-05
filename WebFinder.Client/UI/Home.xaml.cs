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
                //Log("No se ha implementado.");

                foreach (string searchTerm in e.SearchTerms)
                    Console.WriteLine(searchTerm);
            };
        }

        private void btnManageLibrary_Click(object sender, RoutedEventArgs e)
        {
            var ml = new ManageLibrary() {
                Owner = this
            };
            ml.ShowDialog();
        }


        //private string[] GetSearchTerms()
        //{
        //    string text = tbxSearchBox.Text;
        //    return text.Split(';');
        //}
    }
}
