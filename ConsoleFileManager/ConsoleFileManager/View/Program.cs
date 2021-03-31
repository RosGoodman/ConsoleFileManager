using ConsoleFileManager.Controllers.Settings;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //Controller controller = new Controller();
            Settings settings = new Settings();

            System.Console.WriteLine(settings.PropList[1].propValue);
            System.Console.WriteLine(settings.PropList[0].propValue);
            settings.ChangeProperty("LastPath", "C:\\ddd");
            System.Console.WriteLine(settings.PropList[0].propValue);
            settings.SaveSettings();
            System.Console.WriteLine(settings.PropList[1].propValue);
        }
    }
}
