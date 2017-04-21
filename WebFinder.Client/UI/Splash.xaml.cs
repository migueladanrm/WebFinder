using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;

namespace WebFinder.UI
{
    /// <summary>
    /// Lógica de interacción para Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();

            container.Visibility = Visibility.Hidden;

            ((Storyboard)FindResource("anim.onRun")).Completed += (sender, e) => {
                container.Visibility = Visibility.Visible;
                var sb = (Storyboard)FindResource("anim.controls");
                sb.Completed += (sender1, e1) => {
                    Thread.Sleep(200);
                    new Home().Show();
                    Close();
                };

                BeginStoryboard(sb);
            };
        }
    }
}