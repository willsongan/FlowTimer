using ReactiveUI;

namespace FlowTimer.Models;

public class Timer : ReactiveObject
{
    private int _hours;
    private int _minutes;
    private int _seconds;
    
    public int Hours
    {
        get => _hours;
        set
        {
            this.RaiseAndSetIfChanged(ref _hours, value);
            if (_hours >= 24)
            {
                this.RaiseAndSetIfChanged(ref _hours, 0);
            }
        }
    }

    public int Minutes
    {
        get => _minutes;
        set
        {
            this.RaiseAndSetIfChanged(ref _minutes, value);
            if (_minutes > 59)
            {
                _hours++;
                this.RaiseAndSetIfChanged(ref _minutes, 0);
            }
        }
    }

    public int Seconds
    {
        get => _seconds;
        set
        {
            this.RaiseAndSetIfChanged(ref _seconds, value);
            if (_seconds > 59)
            {
                Minutes++;
                this.RaiseAndSetIfChanged(ref _seconds, 0);
            }
        }
    }
}