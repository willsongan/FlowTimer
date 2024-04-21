using FluentAvalonia.UI.Windowing;

namespace FlowTimer.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
}