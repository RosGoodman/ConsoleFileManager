
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;
using ConsoleFileManager.View;

namespace ConsoleFileManager
{

    public class ConsoleFileManager
    {
        public static void Main()
        {
            Controller controller = new Controller();
            ConsoleView view = new ConsoleView();

            view.SetCommand(8, new DeleteCommand(controller));
            view.SetCommand(6, new CreateDirectoryCommand(controller));
            view.Explore();
        }
    }
}
