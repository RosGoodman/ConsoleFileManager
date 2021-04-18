
using ConsoleFileManager.Models;
using System;
using System.Collections.Generic;

namespace ConsoleFileManager.View
{
    /// <summary>Класс описывающий вывод данных в консоль.</summary>
    internal class ViewPrint
    {
        /// <summary>Вывод строки с наименованием файла/папки.</summary>
        /// <param name="printingStr">Выводимая строка.</param>
        /// <param name="cursorPosY">Положение курсора по оси Y.</param>
        internal static void PrintFileOnPage(string printingStr, int cursorPosY, bool isSelected, bool isFolder)
        {
            if(isFolder) Console.ForegroundColor = ConsoleColor.Yellow;
            if (isSelected) Console.BackgroundColor = ConsoleColor.Gray;

            Console.SetCursorPosition(6, cursorPosY);
            Console.WriteLine(printingStr);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>Вывод в консоль запроса на подтверждение действия.</summary>
        internal static void ConfirmationPrint()
        {
            Console.WriteLine("вы уверены? Y/N");
        }

        /// <summary>Вывести инфо о клавишах управления.</summary>
        internal static void PrintButtonsInfo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\"Enter\" - CD/RUN \t \"F5\" - COPY \t \"F6\" - MOVE \t \"F7\" - CREATE \t \"F8\" - RENAME \t \"F9\" - DELETE \t \"F10\" - EXIT \n" +
                "\"DownArrow\" - MoveDown \t \"UpArrow\" - MoveUp \t \"END\" - LastOnPage \t \"HOME\" - FirstOnPage \t \"PageDown\" - NextPage \t \"PageUp\" - PrevPage");
            Console.ResetColor();
        }

        /// <summary>Вывести информацию о файле/папке.</summary>
        /// <param name="fileInfo">Список с инфо.</param>
        internal static void PrintFileInfo(List<string> fileInfo, int maxCountOnPage)
        {
            Console.SetCursorPosition(3, maxCountOnPage + 10);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("FileInfo:");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("FileName: {0}", fileInfo[0]);
            Console.WriteLine("FilePath: {0}", fileInfo[1]);
            Console.WriteLine("Size: {0} Kb", fileInfo[2]);
            Console.WriteLine("CreationTime: {0}", fileInfo[3]);

            Console.ResetColor();
        }

        internal static void PrintNumbPage(int numbPage)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("\t\tPage: {0}", numbPage);
        }

        /// <summary>Считать строку вводимых пользователем параметров.</summary>
        /// <returns>Параметр.</returns>
        internal static string ReadParamString()
        {
            Console.WriteLine();
            Console.WriteLine("Введите имя файла:");
            string commandParam = Console.ReadLine();
            return commandParam;
        }

        /// <summary>Вывод вспомогательного сообщения.</summary>
        internal static void HelpMessage(string msg)
        {
            Console.WriteLine();
            Console.WriteLine(msg);
        }
    }
}
