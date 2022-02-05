using Microsoft.UI.Xaml;
using System;
using System.Timers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ScreenTime
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private ActivityMonitor _monitor;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            this.InitializeComponent();

            _monitor = new ActivityMonitor();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += CheckActivity;
            _timer.Start();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        private void CheckActivity(object sender, object e)
        {
            text.Text = _monitor.IsActive.ToString();
            _monitor.ResetActive();
        }
    }
}
