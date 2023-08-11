using System;
namespace Jokes.Common
{
    public class ConfigManager
    {
        private readonly IConfiguration _configuration;

        public ConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetStringValue(string key)
        {
            string keyValue = _configuration["AppSettings:" + key];
            return keyValue;
        }

        public string JokeURL { get { return GetStringValue("JokeURL"); } }

        public string SearchJokesLimit { get { return GetStringValue("SearchJokesLimit"); } }
    }
}

