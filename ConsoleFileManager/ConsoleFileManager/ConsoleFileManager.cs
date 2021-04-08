﻿
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
            ConsoleView view = new ConsoleView();

            view.SetCommand(8, new DeleteCommand(controller));
            view.SetCommand(6, new CreateDirectoryCommand(controller));
            view.SetCommand(7, new RenameCommand(controller));
            view.SetCommand(5, new MoveCommand(controller));

            view.Explore();
        }
    }
}
