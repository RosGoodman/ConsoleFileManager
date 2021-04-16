﻿
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
            Controller controller = new Controller();
            ConsoleView view = new ConsoleView(controller);

            view.SetCommand(1, new ChangeDirectoryOrRunProcessCommand(controller));
            view.SetCommand(8, new DeleteCommand(controller));
            view.SetCommand(6, new CreateDirectoryCommand(controller));
            view.SetCommand(7, new RenameCommand(controller));
            view.SetCommand(5, new MoveCommand(controller));
            view.SetCommand(10, new SelectTheLowerOneCommand(controller));
            view.SetCommand(11, new SelectTheTopOneCommand(controller));
            view.SetCommand(12, new SelectLastOnPage(controller));
            view.SetCommand(13, new SelectFirstOnPage(controller));

            view.Explore();
        }
    }
}
