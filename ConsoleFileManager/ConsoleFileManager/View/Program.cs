using ConsoleFileManager.Controllers.Settings;
using ConsoleFileManager.Controls;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();

            SettingsClass settingsClass = new SettingsClass();
            settingsClass.Startup();
            string firstName = settingsClass.AppConfiguration["firstname"];
        }
    }
}
