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
using System.IO;
using Microsoft.Win32;

namespace WebFinder.UI
{
    /// <summary>
    /// Lógica de interacción para ManageLibrary.xaml
    /// </summary>
    public partial class ManageLibrary : Window
    {
        public ManageLibrary()
        {
            InitializeComponent();
        }

        private void btnSearchLibrary_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            //dlg.FileOk += Dlg_FileOk;
            if(dlg.ShowDialog().Value) {
                PageLibraryManager.SetLibraryFile(dlg.FileName);
                EventLogger.Log("Se ha configurado el archivo de enlaces.");
            }
        }

        private void Dlg_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
