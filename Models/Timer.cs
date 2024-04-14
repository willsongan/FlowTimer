using ReactiveUI;

namespace FlowTimer.Models;

public class Timer : ReactiveObject
{
    private int? _hours;
    private int? _minutes;
    private int? _seconds;
    
    public int? Hours
    {
        get => _hours;
        set
        {
            this.RaiseAndSetIfChanged(ref _hours, value);
        }
    }

    public int? Minutes
    {
        get => _minutes;
        set
        {
            if (value > 59)
            {
                Hours++;
                this.RaiseAndSetIfChanged(ref _minutes, 0);
            }
            else if (value < 0)
            {
                if (Hours == 0) return;
                Hours--;
                this.RaiseAndSetIfChanged(ref _minutes, 59);
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _minutes, value);
            }
        }
    }

    public int? Seconds
    {
        get => _seconds;
        set
        {
            if (value > 59)
            {
                Minutes++;
                this.RaiseAndSetIfChanged(ref _seconds, 0);
            }
            else if (value < 0)
            {
                if (Hours == 0 && Minutes == 0) return;
                Minutes--;
                this.RaiseAndSetIfChanged(ref _seconds, 59);
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _seconds, value);
            }
        }
    }

    public bool HasRanOut => Hours <= 0 && Minutes <= 0 && Seconds <= 0;
}
