using MCDiscordBot.HelperClasses;
using System.Diagnostics;
using System.Security.Principal;

namespace MCDiscordBot;

public partial class Config : ContentPage
{
    private readonly ConfigLoader _mbc;
    //private readonly MainPage _mp;
    public string TestInfo = "Hello from the config page!";
	public Config(ConfigLoader mbc)
	{

		InitializeComponent();
        _mbc = mbc;
        //_mp = mp;
        TokenField.Text = _mbc.loadedConfig.Token;
        ServerField.Text = _mbc.loadedConfig.Dir;
        ArgsField.Text = _mbc.loadedConfig.Args;
        JavaDirField.Text = _mbc.loadedConfig.JavaDir;

    }
    
    private void showtokenbutton_Clicked(object sender, EventArgs e)
    {
        
		TokenField.IsPassword = !TokenField.IsPassword;
        if(TokenField.IsPassword)
        {
            showtokenbutton.BackgroundColor = new Color(255, 255, 255);
        }
        else
        {
            showtokenbutton.BackgroundColor = new Color(127, 127, 127);
        }
    }

    private void saveconfig_Clicked(object sender, EventArgs e)
    {
        _mbc.loadedConfig.Token = TokenField.Text;
        _mbc.loadedConfig.Dir = ServerField.Text;
        _mbc.loadedConfig.Args = ArgsField.Text;
        _mbc.loadedConfig.JavaDir = JavaDirField.Text;
        _mbc.saveConfig();
        Feedback.Text = "Config Saved";
    }

    private void defaultconfig_Clicked(object sender, EventArgs e)
    {
        _mbc.createDefaultConfig();
        TokenField.Text = _mbc.loadedConfig.Token;
        ServerField.Text = _mbc.loadedConfig.Dir;
        ArgsField.Text = _mbc.loadedConfig.Args;
        JavaDirField.Text = _mbc.loadedConfig.JavaDir;
        Feedback.Text = "Default Loaded";
    }

    private void loadconfig_Clicked(object sender, EventArgs e)
    {
        _mbc.loadConfig();
        TokenField.Text = _mbc.loadedConfig.Token;
        ServerField.Text = _mbc.loadedConfig.Dir;
        ArgsField.Text = _mbc.loadedConfig.Args;
        JavaDirField.Text = _mbc.loadedConfig.JavaDir;
        Feedback.Text = "Config Loaded";
    }

    private void configField_Focused(object sender, FocusEventArgs e)
    {
        Feedback.Text = "";
    }
}