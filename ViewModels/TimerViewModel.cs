using System;
using System.Windows.Input;
using Avalonia.Threading;
using FlowTimer.Models;
using FluentAvalonia.UI.Controls;
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

        PlaybackState = PlaybackStateEnum.IsDefault;
        PlaybackIcon = Symbol.Play;
        
        _dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1),DispatcherPriority.Normal,Tick);

        SetWorkTimeCommand = ReactiveCommand.Create(SetWorkTimer);
        SetFocusTimeCommand = ReactiveCommand.Create(SetFocusTimer);
        PlaybackCommand = ReactiveCommand.Create(Playback);

        IncrementTimeCommand = ReactiveCommand.Create(IncrementTime,
            this.WhenAnyValue(x => x.PlaybackState, (state) => state == PlaybackStateEnum.IsPlaying));

        TestCommand = ReactiveCommand.Create(Test);
    }

    public ICommand TestCommand { get; }
    private void Test()
    {
        
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

    public ICommand SetWorkTimeCommand { get; }
    private void SetWorkTimer()
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

    public ICommand SetFocusTimeCommand { get; }
    private void SetFocusTimer()
    {
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

    public enum PlaybackStateEnum
    {
        IsDefault,
        IsPlaying,
        IsPausing
    }

    private PlaybackStateEnum _playbackState;
    public PlaybackStateEnum PlaybackState
    {
        get => _playbackState;
        set => this.RaiseAndSetIfChanged(ref _playbackState, value);
    }
    private Symbol _playbackIcon;
    public Symbol PlaybackIcon
    {
        get => _playbackIcon;
        set => this.RaiseAndSetIfChanged(ref _playbackIcon, value);
    }

    public ICommand PlaybackCommand { get; }
    private void Playback()
    {
        switch (PlaybackState)
        {
            case PlaybackStateEnum.IsDefault:
                _dispatcherTimer.Start();
                PlaybackIcon = Symbol.Pause;
                PlaybackState = PlaybackStateEnum.IsPlaying;
                break;
            case PlaybackStateEnum.IsPlaying:
                _dispatcherTimer.Stop();
                PlaybackIcon = Symbol.Play;
                PlaybackState = PlaybackStateEnum.IsPausing;
                break;
            case PlaybackStateEnum.IsPausing:
                _dispatcherTimer.Start();
                PlaybackIcon = Symbol.Pause;
                PlaybackState = PlaybackStateEnum.IsPlaying;
                break;
        }
    }

    public ICommand IncrementTimeCommand { get; }
    private void IncrementTime()
    {
        if (FocusTimeLeft != null) FocusTimeLeft.Minutes++;
    }

    private void Tick(object? sender, EventArgs e)
    {
        if (FocusTimeLeft != null && WorkTimeLeft != null)
        {
            FocusTimeLeft.Seconds--;
            WorkTimeLeft.Seconds--;

            if (FocusTimeLeft.HasRanOut || WorkTimeLeft.HasRanOut)
            {
                _dispatcherTimer.Stop();
                PlaybackIcon = Symbol.Play;
                PlaybackState = PlaybackStateEnum.IsDefault;
            }
        }
    }
}