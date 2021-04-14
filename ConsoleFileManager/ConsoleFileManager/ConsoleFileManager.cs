
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;
using ConsoleFileManager.View;
using System;
using System.Threading.Tasks;

namespace ConsoleFileManager
{
    /// <summary>Класс инициализатор программы.</summary>
    public class ConsoleFileManager
    {
        public static void Main()
        {
            Task t = Task.Run(() => {
                while (true)
                {
                    if (Console.WindowWidth < 50 || Console.WindowHeight < 50)
                    {
                        Console.SetWindowSize(50, 50);
                    }
                }
            });

            Controller controller = new Controller();
            ConsoleView view = new ConsoleView(controller);

            view.SetCommand(8, new DeleteCommand(controller));
            view.SetCommand(6, new CreateDirectoryCommand(controller));
            view.SetCommand(7, new RenameCommand(controller));
            view.SetCommand(5, new MoveCommand(controller));
            view.SetCommand(10, new SelectTheLowerOneCommand(controller));
            view.SetCommand(11, new SelectTheTopOneCommand(controller));

            view.Explore();
        }
    }
}
