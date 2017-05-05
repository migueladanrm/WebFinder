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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebFinder.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        /// <summary>
        /// Se produce cuando el usuario solicita la ejecución de una búsqueda.
        /// </summary>
        public event EventHandler<OnSearchRequestEventHandler> OnSearchRequest;

        public SearchBox()
        {
            InitializeComponent();
        }
        

        /// <summary>
        /// Argumentos para eventos de solicitud de búsqueda.
        /// </summary>
        public class OnSearchRequestEventHandler : EventArgs
        {
            public OnSearchRequestEventHandler(List<string> searchTerms)
            {
                SearchTerms = searchTerms;
            }

            /// <summary>
            /// Términos de búsqueda.
            /// </summary>
            public List<string> SearchTerms { get; private set; }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string[] search = tbxSearchContent.Text.Split(';');

            OnSearchRequest?.Invoke(this, new OnSearchRequestEventHandler(search.ToList()));
        }
    }
}
