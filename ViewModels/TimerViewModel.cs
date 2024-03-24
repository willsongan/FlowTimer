using System;
using System.Windows.Input;
using Avalonia.Threading;
using FlowTimer.Models;
using ReactiveUI;

namespace FlowTimer.ViewModels;

public class TimerViewModel : ViewModelBase
{
    public TimerViewModel()
    {
        WorkTimeSpan = new TimeSpan(0, 8, 0, 0);
        WorkTimeLeft = new Timer
        {
            Hours = 0,
            Minutes = 0,
            Seconds = 0
        };

        FocusTimeSpan = new TimeSpan(0, 0, 15, 0);
        FocusTimeLeft = new Timer()
        {
            Hours = 0,
            Minutes = 0,
            Seconds = 0
        };
        
        _dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1),DispatcherPriority.Normal,Tick);

        SetWorkHourCommand = ReactiveCommand.Create(SetWorkHour);
        StartFocusCommand = ReactiveCommand.Create(StartFocus);
    }

    private DispatcherTimer _dispatcherTimer;
    
    private Timer? _workTimeLeft;
    public Timer? WorkTimeLeft
    {
        get => _workTimeLeft;
        private set => this.RaiseAndSetIfChanged(ref _workTimeLeft, value);
    }

    private TimeSpan? _workTimeSpan;
    public TimeSpan? WorkTimeSpan
    {
        get => _workTimeSpan;
        set => this.RaiseAndSetIfChanged(ref _workTimeSpan, value);
    }

    private Timer? _focusTimeLeft;
    public Timer? FocusTimeLeft
    {
        get => _focusTimeLeft;
        set => this.RaiseAndSetIfChanged(ref _focusTimeLeft, value);
    }

    private TimeSpan? _focusTimeSpan;
    public TimeSpan? FocusTimeSpan
    {
        get => _focusTimeSpan;
        set => this.RaiseAndSetIfChanged(ref _focusTimeSpan, value);
    }

    public ICommand SetWorkHourCommand { get; }
    private void SetWorkHour()
    {
        if (WorkTimeSpan == null) return;
        
        var timeSpanValue = WorkTimeSpan.Value;
        if (WorkTimeLeft != null)
        {
            WorkTimeLeft.Hours = timeSpanValue.Hours;
            WorkTimeLeft.Minutes = timeSpanValue.Minutes;
            WorkTimeLeft.Seconds = timeSpanValue.Seconds;
        }
    }
    
    public ICommand StartFocusCommand { get; }
    private void StartFocus()
    {
       _dispatcherTimer.Start();

       //set focus timer
       if (FocusTimeSpan == null) return;
       
       var timeSpan = FocusTimeSpan.Value;
       if (FocusTimeLeft != null)
       {
           FocusTimeLeft.Hours = timeSpan.Hours;
           FocusTimeLeft.Minutes = timeSpan.Minutes;
           FocusTimeLeft.Seconds = timeSpan.Seconds;
       }
    }

    private void Tick(object? sender, EventArgs e)
    {
        if (FocusTimeLeft != null)
        {
            FocusTimeLeft.Seconds--;

            if (FocusTimeLeft.HasRanOut)
            {
                _dispatcherTimer.Stop();
            }
        }
    }
}