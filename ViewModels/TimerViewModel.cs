using System;
using Avalonia.Threading;
using FlowTimer.Models;
using ReactiveUI;

namespace FlowTimer.ViewModels;

public class TimerViewModel : ViewModelBase
{
    public TimerViewModel()
    {
        Timer = new()
        {
            Hours = 0,
            Minutes = 0,
            Seconds = 0
        };
        
        _dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1),DispatcherPriority.Normal,Tick);
        _dispatcherTimer.Start();
    }

    private DispatcherTimer _dispatcherTimer;
    private Timer _timer;
    public Timer Timer
    {
        get => _timer;
        private set => this.RaiseAndSetIfChanged(ref _timer, value);
    }

    private void Tick(object? sender, EventArgs e)
    {
        Timer.Seconds++;
    }
}