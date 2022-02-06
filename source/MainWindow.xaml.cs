using Microsoft.UI.Xaml;
using ScreenTime.Activity;
using System;
using System.Text.Json;
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
        private ActivityTracker _activityTracker;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            this.InitializeComponent();

            var activityMonitor = new ActivityMonitor();
            _activityTracker = new ActivityTracker(activityMonitor);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += CheckActivity;
            _timer.Start();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        private void CheckActivity(object sender, object e)
        {
            var jsonText = JsonSerializer.Serialize(_activityTracker.Activities, new JsonSerializerOptions { IncludeFields = true });
            text.Text = jsonText;
        }
    }
}
