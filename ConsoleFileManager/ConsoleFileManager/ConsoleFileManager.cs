
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;
using ConsoleFileManager.View;

namespace ConsoleFileManager
{
    public class ConsoleFileManager
    {
        public static void StartProgram()
        {
            Controller controller = new Controller();
            ConsoleView view = new ConsoleView();
            view.SetCommand(0, new DeleteCommand(controller));
            view.Start();
        }
    }
}
