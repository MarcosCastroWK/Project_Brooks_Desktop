using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationSilc
{
    public class SilcConfigurationManager
    {
        private readonly IConfiguration _configuration;
        private readonly IConfiguration _enviromentConfiguration;
        private const string CONNECTION_NAME = "Silc";

        public SilcConfigurationManager()
        {
            var rrr = AppDomain.CurrentDomain.BaseDirectory;
            _configuration = GetConfigurationBuilder("");

            var enviromentName = _configuration.GetSection("enviroment").Value;
            _enviromentConfiguration = GetConfigurationBuilder(enviromentName);
        }

        private IConfiguration GetConfigurationBuilder(string name)
        {
            return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings{name}.json", optional: true, reloadOnChange: true)
            .Build();
        }

        public T GetConfiguration<T>() 
        {
            var typeName = typeof(T).Name;
            return _enviromentConfiguration.GetSection(typeName).Get<T>();
        }

        public string GetConnectionString()
        {
            return "Persist Security Info=false;server=SERVIDOR;password=yes;uid=root;database=ewvs;pwd=BR**ks729;Connect Timeout=360;pooling=false;"; //_enviromentConfiguration.GetConnectionString(CONNECTION_NAME);
        }
    }
}
