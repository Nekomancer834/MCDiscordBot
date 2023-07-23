using MCDiscordBot.HelperClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDiscordBot.ServerClasses
{
    public class ServerManager
    {
        //DepInjection
        private readonly ConfigLoader _yaml;

        //Global Variables
        public bool serverStatus = false;
        //public bool errReadLineToggle = true; 
        public Process activeServer {get; set;}

        //Constructor - Creates new process for the server so that the ui can create event handlers before server startup
        public ServerManager(ConfigLoader yaml) 
        { 
            _yaml = yaml;

            activeServer = new Process();

        }

        //prepares the server process for start by feeding it relevant information about what it's running and then starts the server
        public void startServer()
        {
            if (!serverStatus)
            {
                activeServer.Close();
                activeServer.StartInfo.CreateNoWindow = true;
                activeServer.EnableRaisingEvents = true;

                activeServer.StartInfo.RedirectStandardOutput = false;
                activeServer.StartInfo.RedirectStandardError = true;
                activeServer.StartInfo.RedirectStandardInput = true;

                activeServer.StartInfo.UseShellExecute = false;

                activeServer.StartInfo.WorkingDirectory = _yaml.loadedConfig.Dir;
                activeServer.StartInfo.FileName = _yaml.loadedConfig.JavaDir;
                activeServer.StartInfo.Arguments = _yaml.loadedConfig.Args;
                activeServer.Start();
                activeServer.BeginErrorReadLine(); //TODO fix logic for this. sometimes server doesn't stop reading line before it tries to call it again
                
                serverStatus = !serverStatus;
            }
            
        }
        public void stopServer()
        {
            if(serverStatus)
            {
                activeServer.StandardInput.WriteLine("stop");
            }
            
        }
    }
}
