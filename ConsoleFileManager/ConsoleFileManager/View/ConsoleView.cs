
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;

namespace ConsoleFileManager.View
{
    public class ConsoleView
    {
        private Controller _controller;
        private Command[] _buttons;

        enum buttons
        {
            del
        }

        public void Start()
        {
            Explore();
        }

        public void SetCommand(int number, Command com)
        {
            _buttons[number] = com;
        }

        private void PressButton(int number, string param)
        {
            _buttons[number].Execute(param);
        }

        //public void Explore()
        //{
        //    bool exit = false;
        //    while (!exit)
        //    {
        //        if (Console.KeyAvailable)
        //        {
        //            this.ClearMessage();

        //            ConsoleKeyInfo userKey = Console.ReadKey(true);
        //            switch (userKey.Key)
        //            {
        //                case ConsoleKey.Tab:
        //                    this.ChangeActivePanel();
        //                    break;
        //                case ConsoleKey.Enter:
        //                    this.ChangeDirectoryOrRunProcess();
        //                    break;
        //                case ConsoleKey.F3:
        //                    this.ViewFile();
        //                    break;
        //                case ConsoleKey.F4:
        //                    this.FindFile();
        //                    break;
        //                case ConsoleKey.F5:
        //                    this.Copy();
        //                    break;
        //                case ConsoleKey.F6:
        //                    this.Move();
        //                    break;
        //                case ConsoleKey.F7:
        //                    this.CreateDirectory();
        //                    break;
        //                case ConsoleKey.F8:
        //                    this.Rename();
        //                    break;
        //                case ConsoleKey.F9:
        //                    this.Delete();
        //                    break;
        //                case ConsoleKey.F10:
        //                    exit = true;
        //                    Console.ResetColor();
        //                    Console.Clear();
        //                    break;
        //                case ConsoleKey.DownArrow:
        //                    goto case ConsoleKey.PageUp;
        //                case ConsoleKey.UpArrow:
        //                    goto case ConsoleKey.PageUp;
        //                case ConsoleKey.End:
        //                    goto case ConsoleKey.PageUp;
        //                case ConsoleKey.Home:
        //                    goto case ConsoleKey.PageUp;
        //                case ConsoleKey.PageDown:
        //                    goto case ConsoleKey.PageUp;
        //                case ConsoleKey.PageUp:
        //                    this.KeyPress(userKey);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}
    }
}
