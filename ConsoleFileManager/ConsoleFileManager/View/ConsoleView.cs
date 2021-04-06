
using ConsoleFileManager.Controllers.Commands;
using System;

namespace ConsoleFileManager.View
{
    public class ConsoleView
    {
        //private Controller _controller;
        private Command[] _buttons;     //массив команд (индекс = номер команды)

        /// <summary>Установить команду в соответствии с номером.</summary>
        /// <param name="number">Номер команды.</param>
        /// <param name="com">Команда.</param>
        public void SetCommand(int number, Command com)
        {
            _buttons[number] = com;
        }

        /// <summary>Вызов комады.</summary>
        /// <param name="number">Номер команды.</param>
        /// <param name="param">Параметр команды (если есть).</param>
        private void PressButton(int number, string param = "")
        {
            _buttons[number].Execute(param);
        }

        /// <summary>Обработка нажатия клавиш.</summary>
        public void Explore()
        {
            bool exit = false;
            while (!exit)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.Tab:
                            PressButton(0); //ChangeActivePanel();
                            break;
                        case ConsoleKey.Enter:
                            PressButton(1); //ChangeDirectoryOrRunProcess();
                            break;
                        case ConsoleKey.F3:
                            PressButton(2); //ViewFile();
                            break;
                        case ConsoleKey.F4:
                            PressButton(3); //FindFile();
                            break;
                        case ConsoleKey.F5:
                            PressButton(4); //Copy();
                            break;
                        case ConsoleKey.F6:
                            PressButton(5); //Move();
                            break;
                        case ConsoleKey.F7:
                            PressButton(6); //CreateDirectory();
                            break;
                        case ConsoleKey.F8:
                            PressButton(7); //Rename();
                            break;
                        case ConsoleKey.F9:
                            PressButton(8); //Delete();
                            break;
                        case ConsoleKey.F10:
                            exit = true;
                            Console.ResetColor();
                            Console.Clear();
                            break;
                        case ConsoleKey.DownArrow:
                            goto case ConsoleKey.PageUp;
                        case ConsoleKey.UpArrow:
                            goto case ConsoleKey.PageUp;
                        case ConsoleKey.End:
                            goto case ConsoleKey.PageUp;
                        case ConsoleKey.Home:
                            goto case ConsoleKey.PageUp;
                        case ConsoleKey.PageDown:
                            goto case ConsoleKey.PageUp;
                        case ConsoleKey.PageUp:
                            PressButton(9); //KeyPress(userKey);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
