
using System.Timers;

namespace CryptoChat.Frontend.Services;

public class JobExecutedEventArgs : EventArgs {}


public class PeriodicExecutor : IDisposable
{
    public event EventHandler<JobExecutedEventArgs> JobExecuted;
    void OnJobExecuted()
    {
        JobExecuted?.Invoke(this, new JobExecutedEventArgs());
    }

    private System.Timers.Timer _timer;
    private bool _running;

    public void StartExecuting()
    {
        if (!_running)
        {
            // Initiate a Timer
            _timer= new System.Timers.Timer();
            _timer.Interval = 100;  // every 60 seconds
            _timer.Elapsed += HandleTimer;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            _running = true;
        }
    }
    void HandleTimer(object source, ElapsedEventArgs e)
    {
        // Execute required job

        // Notify any subscribers to the event
        OnJobExecuted();
    }
    public void Dispose()
    {
        if (_running)
        {
            // Clear up the timer
        }
    }
}