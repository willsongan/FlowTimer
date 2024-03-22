using System;
using Avalonia.Threading;
using ReactiveUI;

namespace FlowTimer.ViewModels;

public class TimerViewModel : ViewModelBase
{
    public TimerViewModel()
    {
        _timer = new DispatcherTimer(TimeSpan.FromSeconds(1),DispatcherPriority.Normal,Tick);
        _timer.Start();
    }

    private void Tick(object? sender, EventArgs e)
    {
        Counter++;
    }

    private DispatcherTimer _timer;

    private int _counter;
    public int Counter
    {
        get => _counter;
        set => this.RaiseAndSetIfChanged(ref _counter, value);
    }
}