using MCDiscordBot.ServerClasses;

namespace MCDiscordBot;

public partial class App : Application
{
    private readonly ServerManager _sm;
    public App(ServerManager sm)
	{
		InitializeComponent();
        _sm = sm;
        MainPage = new AppShell();
	}
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 800;
        const int newHeight = 600;

        window.MinimumWidth = window.MaximumWidth = window.Width = newWidth;
        window.MinimumHeight = window.MaximumHeight = window.Height = newHeight;

        window.Destroying += (sender, eventArgs) =>
        {
            _sm.stopServer();
        };
        return window;
    }

}
