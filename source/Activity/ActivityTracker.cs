using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ScreenTime.Activity
{
    internal class Activity
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public TimeSpan Duration;
    }

    internal class ActivityTracker
    {
        private ActivityMonitor _activityMonitor;
        private Timer _timer;

        private DateTime _startTime;
        private bool _wasActive;

        internal List<Activity> Activities { private set; get; }

        internal ActivityTracker(ActivityMonitor activityMonitor)
        {
            Activities = new List<Activity>();

            _wasActive = false;
            _activityMonitor = activityMonitor;

            _timer = new Timer(1000);
            _timer.Elapsed += CheckActivity;
            _timer.Start();
        }

        private void CheckActivity(object sender, object e)
        {
            bool isActive = _activityMonitor.IsActive;

            if (isActive && !_wasActive)
            {
                _startTime = DateTime.Now;
            }

            if (!isActive && _wasActive)
            {
                var endTime = DateTime.Now;

                Activities.Add(new Activity
                {
                    StartTime = _startTime,
                    EndTime = endTime,
                    Duration = endTime - _startTime,
                });
            }

            _wasActive = isActive;
            _activityMonitor.ResetActive();
        }
    }
}
