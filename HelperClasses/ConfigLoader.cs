using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace MCDiscordBot.HelperClasses
{
    public class ConfigLoader
    {
        public string configLocation = @"E:/DEMOFOLDER/_config.yml";
        public ConfigFile loadedConfig { get; set; }
        public string serializedConfig { get; set; }

        public StringReader config;
        public ConfigLoader() 
        {
            //load a config on startup
            loadConfig();
        }
        public void loadConfig()
        {
            
            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

            //if config doesn't exist in the specified location, create the file and fill it with the default config
            if (!File.Exists(configLocation) || File.ReadAllText(configLocation) == "")
            {
                createDefaultConfig();
                saveConfig();
            }
            //read the contents of the config and place them in the loadedConfig
            config = new StringReader(File.ReadAllText(configLocation));
            loadedConfig = deserializer.Deserialize<ConfigFile>(config);
        }
        public bool saveConfig()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            //Convert the loadedConfig into a string and write that back into the config file
            serializedConfig = serializer.Serialize(loadedConfig);
            try
            {
                File.WriteAllText(configLocation, serializedConfig);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public void createDefaultConfig()
        {
            //overwrite the loadedConfig with a new config that contains default values
            loadedConfig = new ConfigFile()
            {
                Token = null,
                Dir = null,
                Args = null,
                JavaDir = null,
                Test = "A person who thinks all the time has nothing to think about except thoughts"

            };
        }
    }

    //The class that holds the loaded config values
    public class ConfigFile
    {
        public string Token { get; set; }
        public string Dir { get; set; }
        public string Args { get; set; }
        public string JavaDir { get; set; }
        public string Test { get; set; }
    }
}
