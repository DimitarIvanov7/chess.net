using System.Timers;
using Timer = System.Timers.Timer;

namespace WebApplication3.Domain.Features.ChessLogic
{
    public class CountdownTimer
    {
        private int Duration;
        public int CurrentTime { get; private set; }
        private int? IntervalId;

        public CountdownTimer(int duration)
        {
            Duration = duration;
            CurrentTime = duration;
            IntervalId = null;
        }

        private void UpdateDisplay()
        {
            // Update the display with the current time (you can customize this as needed)
        }

        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int secs = seconds % 60;
            return $"{minutes}:{(secs < 10 ? "0" : "")}{secs}";
        }

        public void Start()
        {
            if (IntervalId != null) return; // Prevent multiple intervals

            IntervalId = TimerHelper.SetInterval(() =>
            {
                CurrentTime--;
                UpdateDisplay();

                if (CurrentTime <= 0)
                {
                    Stop();
                }
            }, 1000);
        }

        public void Stop()
        {
            if (IntervalId != null)
            {
                TimerHelper.ClearInterval((int)IntervalId);
                IntervalId = null;
            }
        }

        public void Reset()
        {
            Stop();
            CurrentTime = Duration;
            UpdateDisplay();
        }

        public void SetDuration(int newDuration)
        {
            Duration = newDuration;
            Reset();
        }
    }

    public static class TimerHelper
    {
        private static readonly Timer timer = new Timer();

        public static int SetInterval(Action action, int interval)
        {
            timer.Interval = interval;
            timer.Elapsed += (sender, args) => action.Invoke();
            timer.Start();
            return timer.GetHashCode(); // Return a unique identifier for the interval
        }

        public static void ClearInterval(int id)
        {
            timer.Stop();
            timer.Elapsed -= (sender, args) => { }; // Remove the event handler
        }
    }
}
