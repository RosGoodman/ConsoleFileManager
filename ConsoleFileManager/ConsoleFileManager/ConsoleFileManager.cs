
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;
using ConsoleFileManager.View;

namespace ConsoleFileManager
{
    /// <summary>Класс инициализатор программы.</summary>
    public class ConsoleFileManager
    {
        public static void Main()
        {
            Controller controller = new Controller();
            ConsoleView view = new ConsoleView(controller);

            view.SetCommand(1, new ChangeDirectoryOrRunProcessCommand(controller));
            view.SetCommand(2, new CopyCommand(controller));
            view.SetCommand(3, new MoveCommand(controller));
            view.SetCommand(4, new CreateDirectoryCommand(controller));
            view.SetCommand(5, new RenameCommand(controller));
            view.SetCommand(6, new DeleteCommand(controller));
            view.SetCommand(7, new ExitCommand(controller));
            view.SetCommand(8, new SelectTheLowerOneCommand(controller));
            view.SetCommand(9, new SelectTheTopOneCommand(controller));
            view.SetCommand(10, new SelectLastOnPageCommand(controller));
            view.SetCommand(11, new SelectFirstOnPageCommand(controller));
            view.SetCommand(12, new PageUpOrDownCommand(controller));

            view.Explore();
        }
    }
}
