
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controllers.Services;
using ConsoleFileManager.Controls;
using ConsoleFileManager.View;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;

namespace ConsoleFileManager
{

    public class ConsoleFileManager
    {
        public static void Main()
        {
            WorkWithFiles.Availability(@"C:\ProgramData\Новая папка");

            //Controller controller = new Controller();
            //ConsoleView view = new ConsoleView();

            //view.SetCommand(0, new DeleteCommand(controller));
            //view.Explore();
        }
    }
}
