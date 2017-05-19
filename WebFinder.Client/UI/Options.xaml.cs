using System.Windows;

namespace WebFinder.UI
{
    /// <summary>
    /// Lógica de interacción para Settings.xaml
    /// </summary>
    public partial class Options : Window
    {
        bool loadMode = true;

        public Options()
        {
            InitializeComponent();

            LoadSettings();
        }

        private void LoadSettings()
        {
            cbxUseParallelism.IsChecked = (bool)SettingsManager.GetSetting(SettingsManager.STT_SEARCH_PARALLEL);
            loadMode = false;
        }

        private void cbxUseParallelism_Click(object sender, RoutedEventArgs e)
        {
            SettingsManager.SetSetting(SettingsManager.STT_SEARCH_PARALLEL, cbxUseParallelism.IsChecked.Value);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void btnAddLink_Click(object sender, RoutedEventArgs e)
        {
            PageLibraryManager.AddLink(tbxLink.Text);
            tbxLink.Text = null;
        }
    }
}
