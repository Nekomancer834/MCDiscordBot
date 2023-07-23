

using MCDiscordBot.ServerClasses;
using System.Diagnostics;
using System.Text;

namespace MCDiscordBot;

public partial class MainPage : ContentPage
{
	private static StringBuilder stdout = new StringBuilder();
	private readonly Config _config;
	private readonly ServerManager _sm;

    public MainPage(Config config, ServerManager sm)
	{
		InitializeComponent();
		_config = config;
		_sm = sm;
        _sm.activeServer.ErrorDataReceived += OnStdOutUpdated;
        _sm.activeServer.Exited += OnServerExit;
    }

	private async void OnStartServerClicked(object sender, EventArgs e)
	{
		if (!_sm.serverStatus)
		{
			_sm.startServer();
            stdout.Clear();
			logbox.Text = stdout.ToString();
            statuscolor.TextColor = new Color(0, 255, 0);
            statuscolor.Text = "ONLINE";
			serverbutton.Text = "Stop Server";
        }
		else
		{
			_sm.stopServer();
            statuscolor.TextColor = new Color(255, 0, 0);
            statuscolor.Text = "OFFLINE";
            serverbutton.Text = "Start Server";
        }

		await Task.CompletedTask;
	}

	private void OnStdOutUpdated(object s, DataReceivedEventArgs e)
	{
        if (!String.IsNullOrEmpty(e.Data))
        {
            MainThread.BeginInvokeOnMainThread(async () => 
			{
                    await Task.Run(()=> stdout.Append($"{e.Data}\n"));
                    logbox.Text = stdout.ToString(); //TODO why is this Index out of range erroring
				
			});
        }
    }
	private void OnServerExit(object s, EventArgs e)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
            statuscolor.TextColor = new Color(255, 0, 0);
			statuscolor.Text = "OFFLINE";
			serverbutton.Text = "Start Server";
            _sm.serverStatus = !_sm.serverStatus;
			_sm.activeServer.CancelErrorRead();
            stdout.Append("\nServer Closed");
			logbox.Text = stdout.ToString();
		});
    }
}

