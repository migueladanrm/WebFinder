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
using System.Diagnostics;
using System.Timers;
//using System.Threading;
//using System.Windows.Threading;

namespace WebFinder.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para PerformanceViewerProcessorCore.xaml
    /// </summary>
    public partial class PerformanceViewerProcessorCore : UserControl
    {
        public PerformanceViewerProcessorCore()
        {
            InitializeComponent();
        }

        public PerformanceViewerProcessorCore(string description)
        {
            InitializeComponent();
            lblDescription.Content = description;
        }

        public void Update(double value)
        {
            pbar.Value = value;
            lblCurrentPercent.Content = $"{value} %";
        }

        //private void Start()
        //{
        //    //while (true) {
        //    //    Thread.Sleep(500);
        //    //    pbar.Value= Math.Round(pc.NextValue(), 2);
        //    //}

            
        //    var timer = new Timer(500) {
        //        AutoReset = true
        //    };
            
            
        //    timer.Elapsed += (sender, e) => {
        //        double p = Math.Round(pc.NextValue(), 2);

        //        Dispatcher.Invoke(() => {
        //        lblCurrentPercent.Content = $"{p} %";
        //        pbar.Value = p;

        //        });


        //        timer.Stop();
        //        timer.Start();
        //    };

        //    timer.Start();
        //}

    }
}
