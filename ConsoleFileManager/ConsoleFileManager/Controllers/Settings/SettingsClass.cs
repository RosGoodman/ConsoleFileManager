using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ConsoleFileManager.Controllers.Settings
{
    internal class SettingsClass
    {
        // свойство, которое будет хранить конфигурацию
        internal IConfiguration AppConfiguration { get; set; }

        /// <summary>Установка загруженных настроек.</summary>
        internal void Startup(string loadedSettings)
        {
            // строитель конфигурации
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"firstname", "Tom"},
                    {"age", "31"}
                });
            // создаем конфигурацию
            AppConfiguration = builder.Build();
        }
        
    }
}
