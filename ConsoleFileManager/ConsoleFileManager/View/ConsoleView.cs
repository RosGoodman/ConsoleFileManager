
using ConsoleFileManager.Controllers.Commands;
using System;
using System.Collections.Generic;

namespace ConsoleFileManager.View
{
    public class ConsoleView
    {
        //private Controller _controller;
        private Dictionary<int, Command> _buttons = new Dictionary<int, Command>();     //словарь команд (номер/команда)
        //private string _paramString;    //строка вводимых пользователем параметров.

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
                            PressButton(0); //вызов ChangeActivePanelCommand();
                            break;
                        case ConsoleKey.Enter:
                            PressButton(1); //ChangeDirectoryOrRunProcessCommand();
                            break;
                        case ConsoleKey.F3:
                            PressButton(2); //ViewFileCommand();
                            break;
                        case ConsoleKey.F4:
                            PressButton(3); //FindFileCommand();
                            break;
                        case ConsoleKey.F5:
                            PressButton(4); //CopyCommand();
                            break;
                        case ConsoleKey.F6:
                            PressButton(5); //MoveCommand();
                            break;
                        case ConsoleKey.F7:
                            PressButton(6, ReadParamString()); //CreateDirectoryCommand();
                            break;
                        case ConsoleKey.F8:
                            PressButton(7, ReadParamString()); //RenameCommand();
                            break;
                        case ConsoleKey.F9:
                            if(Confirmation()) PressButton(8, ReadParamString()); //DeleteCommand();
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

        /// <summary>Запрос на подтверждение действия.</summary>
        /// <returns>true/false - подтверждение/отмена.</returns>
        private static bool Confirmation()
        {
            Console.WriteLine("вы уверены? Y/N");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.N:
                            return false;
                        case ConsoleKey.Y:
                            return true;
                    }
                }
            }
        }

        /// <summary>Считать строку вводимых пользователем параметров.</summary>
        /// <returns>Параметр.</returns>
        private string ReadParamString()
        {
            //TODO: дописать метод. пока заглушка
            return "test2";
        }
    }
}
