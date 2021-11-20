using Microsoft.Win32;
using T = System.Timers;

namespace Timer;

//https://stackoverflow.com/a/8480218
static class MidnightNotifier
{
    private static readonly T.Timer timer;

    static MidnightNotifier()
    {
        timer = new T.Timer(GetSleepTime());
        timer.Elapsed += (s, e) =>
        {
            OnDayChanged();
            timer.Interval = GetSleepTime();
        };
        timer.Start();

        SystemEvents.TimeChanged += OnSystemTimeChanged;
    }

    private static double GetSleepTime()
    {
        DateTime midnightTonight = DateTime.Today.AddDays(1);
        double differenceInMilliseconds = (midnightTonight - DateTime.Now).TotalMilliseconds;
        return differenceInMilliseconds;
    }

    private static void OnDayChanged()
    {
        DayChanged?.Invoke(null, EventArgs.Empty);
    }

    private static void OnSystemTimeChanged(object sender, EventArgs e)
    {
        timer.Interval = GetSleepTime();
    }

    public static event EventHandler<EventArgs> DayChanged;
}