using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static WebFinder.EventLogger;

namespace WebFinder
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new UI.Splash().Show();

            Log("El programa ha iniciado correctamente.");
        }
    }
}
