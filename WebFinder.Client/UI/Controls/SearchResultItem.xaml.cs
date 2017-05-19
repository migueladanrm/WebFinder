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
using System.ComponentModel;
using System.Diagnostics;

namespace WebFinder.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para SearchResultItem.xaml
    /// </summary>
    public partial class SearchResultItem : UserControl
    {
        public SearchResultItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register("PageTitle", typeof(string), typeof(SearchResultItem), new PropertyMetadata("Lorem ipsum dolor"));
        public static readonly DependencyProperty PageUrlProperty = DependencyProperty.Register("PageUrl", typeof(string), typeof(SearchResultItem), new PropertyMetadata("www.lipsum.com"));
        public static readonly DependencyProperty MatchesFoundProperty = DependencyProperty.Register("MatchesFound", typeof(int), typeof(SearchResultItem), new PropertyMetadata(19));

        [Description("Título de la página web"), Category("Personalizado")]
        public string PageTitle {
            get => (string)GetValue(PageTitleProperty);
            set => SetValue(PageTitleProperty, value);
        }

        [Description("URL base de la página web"), Category("Personalizado")]
        public string PageUrl {
            get => (string)GetValue(PageUrlProperty);
            set {
                lblPageDomain.Text = value.Replace("http://", null).Replace("https://",null);
                SetValue(PageUrlProperty, value);
            }
        }

        [Description("Coincidencias encontradas"), Category("Personalizado")]
        public int MatchesFound {
            get => (int)GetValue(MatchesFoundProperty);
            set {
                lblMatches.Text = ((string)FindResource("lang.searchResultItem.matches")).Replace("{0}", value.ToString());
                SetValue(MatchesFoundProperty, value);
            }
        }

        private void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PageUrl != null)
                Process.Start(PageUrl);
        }
    }
}
