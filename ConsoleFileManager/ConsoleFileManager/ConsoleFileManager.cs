
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;
using ConsoleFileManager.View;
using System;

namespace ConsoleFileManager
{

    public class ConsoleFileManager
    {
        /// <summary>Перечисление всех команд.</summary>
        enum Commands
        {
            ChangeActivePanel,
            ChangeDirectoryOrRunProcess,
            ViewFile,
            FindFile,
            Copy,
            Move,
            CreateDirectory,
            Rename,
            Delete,
            exit
        }

        public static void Main()
        {
            Controller controller = new Controller();
            ConsoleView view = new ConsoleView();

            view.SetCommand(0, new DeleteCommand(controller));
            view.Explore();
        }
    }
}
