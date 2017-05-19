using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows.Controls;

namespace WebFinder.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para PerformanceViewer.xaml
    /// </summary>
    public partial class PerformanceViewer : UserControl
    {
        private List<PerformanceCounter> cores = null;
        public PerformanceViewer()
        {
            InitializeComponent();

            cores = new List<PerformanceCounter> {
                new PerformanceCounter("Processor", "% Processor Time", "_Total")
            };
            for (int i = 0; i < Environment.ProcessorCount; i++) {
                cores.Add(new PerformanceCounter("Processor", "% Processor Time", $"{i}"));
            }

            for (int i = 0; i <= Environment.ProcessorCount; i++)
                stkItems.Children.Add(new PerformanceViewerProcessorCore(i == 0 ? "General" : $"C. {i - 1}"));

            UpdateData();
        }


        void UpdateData()
        {
            var timer = new Timer(500) {
                AutoReset = true
            };
            
            timer.Elapsed += (sender, e) => {

                for (int i = 0; i < cores.Count; i++) {
                    Dispatcher.Invoke(() => {
                        ((PerformanceViewerProcessorCore)stkItems.Children[i]).Update(Math.Round(cores[i].NextValue(), 2));
                    });
                }


                //Dispatcher.Invoke(() => {

                //    lblCurrentPercent.Content = $"{p} %";
                //    pbar.Value = p;

                //});

            };

            timer.Start();
        }
        
    }
}