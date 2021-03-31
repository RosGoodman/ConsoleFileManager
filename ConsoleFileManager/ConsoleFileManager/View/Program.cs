using ConsoleFileManager.Controllers.Settings;
using ConsoleFileManager.Controls;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            Settings settings = new Settings();

            System.Console.WriteLine(settings.PropList[1].PropValue);
            System.Console.WriteLine(settings.PropList[0].PropValue);
            settings.ChangeProperty("LastPath", "C:\\ddd");
            System.Console.WriteLine(settings.PropList[1].PropValue);
            settings.SaveSettings();
            System.Console.WriteLine(settings.PropList[1].PropValue);
        }
    }
}
