
using ConsoleFileManager.Models;
using System;
using System.Collections.Generic;

namespace ConsoleFileManager.View
{
    /// <summary>Класс описывающий вывод данных в консоль.</summary>
    internal class ViewPrint
    {
        /// <summary>Выделить строку в списке.</summary>
        private static void VisualSelectingFile(FileModel selectedFile, List<FileModel> pageList)
        {
            Console.Clear();

            for (int i = 0; i < pageList.Count; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new String(' ', 50));

                if (pageList[i].IsFolder) Console.ForegroundColor = ConsoleColor.Yellow;
                if (pageList[i] == selectedFile) Console.BackgroundColor = ConsoleColor.Gray;

                Console.SetCursorPosition(0, i);

                for (int j = 0; j < pageList[i].DeepthLvl; j++)
                {
                    Console.Write("    ");
                }

                Console.WriteLine(pageList[i].ToString());

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        /// <summary>Вывод строки с наименованием файла/папки.</summary>
        /// <param name="printingStr">Выводимая строка.</param>
        /// <param name="cursorPosY">Положение курсора по оси Y.</param>
        internal static void PrintFileOnPage(string printingStr, int cursorPosY, bool isSelected, bool isFolder)
        {
            if(isFolder) Console.ForegroundColor = ConsoleColor.Yellow;
            if (isSelected) Console.BackgroundColor = ConsoleColor.Gray;

            Console.SetCursorPosition(6, cursorPosY);
            Console.WriteLine(printingStr);

            Console.ResetColor();
        }

        /// <summary>Вывод в консоль запроса на подтверждение действия.</summary>
        internal static void ConfirmationPrint()
        {
            Console.WriteLine("вы уверены? Y/N");

        }
    }
}
